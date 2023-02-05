using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public Transform PlayerSpawnPoint;
    public Image ScreenCover;
    public float fadeTime;
    public bool loading = false;
    public GameObject player;
    public bool combatRoom = false;
    public bool encounterComplete;

    public EventReference selectionMusic;
    // Start is called before the first frame update

    private EventInstance _selectionMusic;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = FindObjectOfType<PlayerMovement>().gameObject;
            
            player.transform.position=PlayerSpawnPoint.position;
            DontDestroyOnLoad(player);
            StartCoroutine(fade(Color.black, Color.clear));
            StartCoroutine(StartNextEncounter());
            DontDestroyOnLoad(gameObject);
            _selectionMusic = RuntimeManager.CreateInstance(selectionMusic);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(PlayerDeathRestartCoroutine());
    }

    private IEnumerator PlayerDeathRestartCoroutine()
    {
        foreach (var resettable in player.GetComponents<IResettable>())
        {
            resettable.ResetObject();
        }
        yield return fade(Color.clear, Color.white);

        CombatManager.instance.killAllEnemies();
        CombatManager.instance.StopAllCoroutines();
        CombatManager.instance.Cleanup();
        combatRoom = false;
        encounterComplete = true;
        
        player.gameObject.SetActive(true);
        
        yield return sceneSetup();
        yield return fade(Color.white, Color.clear);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if ((!loading) && encounterComplete)
                TransitionToNextLevel();
        }
    }
    void TransitionToNextLevel()
    {
        loading = true;
        
        StartCoroutine(fade(Color.clear,Color.black));
        
        StartCoroutine(sceneSetup());
    }
    IEnumerator sceneSetup()
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(fade(Color.black,Color.clear));
        StartCoroutine(StartNextEncounter());
    }
    IEnumerator StartNextEncounter()
    {
        yield return new WaitForSeconds(0.01f);
        loading = false;
        player.transform.position = PlayerSpawnPoint.position;
        if (!combatRoom)
        {
            CombatManager.instance.Cleanup();
            ChoiceManager.instance.SpawnNewPickups();
            combatRoom = !combatRoom;
            _selectionMusic.start();
        }
        else
        {
            // _selectionMusic.stop(STOP_MODE.ALLOWFADEOUT);
            CombatManager.instance.StartCombatEncounter();
            combatRoom = !combatRoom;
        }
        encounterComplete = false;
    }
    IEnumerator fade(Color start,Color end)
    {
        ScreenCover.color = start;
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            ScreenCover.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        ScreenCover.color = end;
    }
}
