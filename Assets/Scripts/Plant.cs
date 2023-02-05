using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plant : MonoBehaviour
{
    private Transform positionTarget;
    Vector3 velocity = Vector3.zero;
    public float maxSpeed = 1;
    public float smoothTime = 0.1f;
    public float wiggleFrequency;
    public float wiggleAmplitude;

    public State currentState;
    private float waitTime;
    private Vector3 startPosition;
    public float wanderRadius;

    public enum State
    {
        moveToTarget,
        wait,
    }

    void Start()
    {
        positionTarget = transform.Find("Position Target").transform;
        positionTarget.parent = null;
        startPosition = transform.position;

        waitTime = 1;
        currentState = State.wait;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.moveToTarget:
                transform.position = Vector3.SmoothDamp(transform.position, positionTarget.position, ref velocity,
                    smoothTime, maxSpeed);
                AddWiggle();

                if (Vector3.Distance(transform.position, positionTarget.position) < 0.1f)
                {
                    waitTime = Random.Range(5, 15f);
                    currentState = State.wait;
                }

                break;
            case State.wait:
                AddWiggle();
                waitTime -= Time.deltaTime;

                if (waitTime <= 0)
                {
                    NewTargetLocation();
                    currentState = State.moveToTarget;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void AddWiggle()
    {
        Vector3 randomMovement = new Vector3(
            (Mathf.PerlinNoise(Time.time * wiggleFrequency, 0) * 2) - 1,
            (Mathf.PerlinNoise(0, Time.time * wiggleFrequency) * 2) - 1,
            0);
        transform.position += randomMovement * (Time.deltaTime * wiggleAmplitude);
    }

    void NewTargetLocation()
    {
        positionTarget.position = startPosition + ((Vector3) Random.insideUnitCircle * wanderRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPosition, wanderRadius);
    }
}