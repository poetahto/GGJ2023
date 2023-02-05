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

    public Transform[] hearts;
    private int currentHeartCount;

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

        currentHeartCount = hearts.Length;

        waitTime = 1;
        currentState = State.wait;
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDamage.AddListener(DropHeart);
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDamage.RemoveListener(DropHeart);
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
            0,
            (Mathf.PerlinNoise(0, Time.time * wiggleFrequency) * 2) - 1);
        transform.position += randomMovement * (Time.deltaTime * wiggleAmplitude);
    }

    void NewTargetLocation()
    {
        Vector2 circle = Random.insideUnitCircle;
        positionTarget.position = startPosition + (new Vector3(circle.x, 0, circle.y) * wanderRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPosition, wanderRadius);
    }

    [EasyButtons.Button]
    public void DropHeart(HealthDamageEvent data)
    {
        if (currentHeartCount <= 0) return;
        currentHeartCount--;
        Transform heart = hearts[currentHeartCount];
        heart.parent = null;
        var rb = heart.gameObject.AddComponent<Rigidbody>();
        rb.drag = 0.5f;
        heart.gameObject.GetComponent<DeadHeart>().startDeath = true;
    }
}