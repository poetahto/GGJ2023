using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransitionManager : MonoBehaviour
{

    public Transform SpawnPoint;
    public Image ScreenCover;
    public float fadeTime;
    public bool loading = false;
    public Transform player;
    public ChoiceManager choiceManager;
    public CombatManager combatManager;
    public bool combatRoom = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fade(Color.black, Color.clear));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D()
    {
        if(!loading)
            TransitionToNextLevel();
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
        StartCoroutine(fade(Color.black,Color.clear));
        loading = false;
        player.transform.position = SpawnPoint.position;
        if (!combatRoom)
        {
            combatManager.Cleanup();
            choiceManager.SpawnNewPickups();
            combatRoom = !combatRoom;
        }
        else
        {
            choiceManager.Cleanup();
            combatManager.StartCombatEncounter();
            combatRoom = !combatRoom;
        }
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
