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
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
    IEnumerator fade(Color start,Color end)
    {
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
