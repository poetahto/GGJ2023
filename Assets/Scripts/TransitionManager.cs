using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    // Start is called before the first frame update

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
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        print(ChoiceManager.instance);
        loading = false;
        player.transform.position = PlayerSpawnPoint.position;
        if (!combatRoom)
        {
            CombatManager.instance.Cleanup();
            ChoiceManager.instance.SpawnNewPickups();
            combatRoom = !combatRoom;
        }
        else
        {
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
