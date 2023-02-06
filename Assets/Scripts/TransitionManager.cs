using DefaultNamespace;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public TextMeshProUGUI FloorDisplay;
    public int floorCount;
    public EventReference selectionMusic;
    bool quoteDisplayed = false;
    public TextMeshProUGUI quoteText;
    public List<string> lowQuotes;
    public List<string> highQuotes;
    public List<string> lowDeathQuotes;
    public List<string> highDeathQuotes;

    public EventReference doorSfx;
    // Start is called before the first frame update

    private EventInstance _selectionMusic;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }

    public void SetIntensity(float value)
    {
        _selectionMusic.setParameterByName("Battle_Intensity", value);
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
            _selectionMusic.start();
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
        foreach (var resettable in player.GetComponentsInChildren<IResettable>())
        {
            resettable.ResetObject();
        }
        yield return fade(Color.clear, Color.white);

        CombatManager.instance.killAllEnemies();
        CombatManager.instance.StopAllCoroutines();
        CombatManager.instance.Cleanup();
        combatRoom = false;
        encounterComplete = true;
        floorCount = 0;
        foreach (Rune rune in FindObjectsOfType<Rune>())
        {
            Destroy(rune.gameObject);
        }
        FloorDisplay.text = "Floor " + floorCount.ToString();
        player.gameObject.SetActive(true);
        ChangeDeathQuoteText();
        StartCoroutine(CycleQuote(Color.clear, Color.black));
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        StartCoroutine(CycleQuote(Color.black, Color.clear));
        SceneManager.LoadScene("PickupRoom");
        floorCount++;
        FloorDisplay.text = "Floor " + floorCount.ToString();
        StartCoroutine(StartNextEncounter());
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
        RuntimeManager.PlayOneShot(doorSfx);
        StartCoroutine(sceneSetup());
    }
    IEnumerator sceneSetup()
    {
        yield return new WaitForSeconds(fadeTime);
        floorCount++;
        FloorDisplay.text = "Floor " + floorCount.ToString();
        if (combatRoom)
        {
            SetIntensity(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        else
        {
            SetIntensity(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-  1);
            ChangeQuoteText();
            StartCoroutine(CycleQuote(Color.clear, Color.white));
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
            StartCoroutine(CycleQuote(Color.white, Color.clear));
        }
        
        StartCoroutine(fade(Color.black,Color.clear));
        StartCoroutine(StartNextEncounter());
    }
    void ChangeQuoteText()
    {
        if(floorCount<5)
            quoteText.text = lowQuotes[Random.Range(0, lowQuotes.Count)];
        else
        {
            quoteText.text = highQuotes[Random.Range(0, highQuotes.Count)];
        }
    }
    void ChangeDeathQuoteText()
    {
        if (floorCount < 5)
            quoteText.text = lowDeathQuotes[Random.Range(0, lowDeathQuotes.Count)];
        else
        {
            quoteText.text = highDeathQuotes[Random.Range(0, highDeathQuotes.Count)];
        }
    }
    IEnumerator CycleQuote(Color start, Color end)
    {
        quoteText.color = start;
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            quoteText.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        quoteText.color = end;
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
