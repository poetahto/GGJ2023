using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            player = Instantiate(player);
            player.transform.position=PlayerSpawnPoint.position;
            DontDestroyOnLoad(player);
            StartCoroutine(fade(Color.black, Color.clear));
            StartNextEncounter();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D col)
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
        StartNextEncounter();
    }
    void StartNextEncounter()
    {
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
            ChoiceManager.instance.Cleanup();
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
