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
        StartCoroutine(fadeOut());
    }
    IEnumerator fadeOut()
    {
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            ScreenCover.color = Color.Lerp(Color.clear, Color.black, normalizedTime);
            yield return null;
        }
        ScreenCover.color = Color.black;
        sceneSetup();
    }
    void sceneSetup()
    {
        StartCoroutine(fadeIn());
        loading = false;
        player.transform.position = SpawnPoint.position;
    }
    IEnumerator fadeIn()
    {
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            ScreenCover.color = Color.Lerp(Color.black, Color.clear, normalizedTime);
            yield return null;
        }
        ScreenCover.color = Color.clear;
    }
}
