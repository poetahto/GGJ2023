using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Transform positionTarget;
    Vector2 velocity = Vector2.zero;
    public float maxSpeed = 1;
    public float smoothTime = 0.1f;
    public float wiggleFrequency;
    public float wiggleAmplitude;

    enum State
    {
        moveToTarget,
        wait,
    }

void Start()
    {
        positionTarget = transform.Find("Position Target").transform;
        positionTarget.parent = null;
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            while (Vector2.Distance(transform.position, positionTarget.position) > 0.1f)
            {
                transform.position = Vector2.SmoothDamp(transform.position, positionTarget.position, ref velocity,
                    smoothTime, maxSpeed);

                Vector3 randomMovement = new Vector3(
                    (Mathf.PerlinNoise(Time.time * wiggleFrequency, 0) * 2) - 1,
                    (Mathf.PerlinNoise(0, Time.time * wiggleFrequency) * 2) - 1,
                    0);
                transform.position += randomMovement * (Time.deltaTime * wiggleAmplitude);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 5f));

            positionTarget.position = Random.insideUnitCircle * 2;


            yield return null;
        }
    }
}