using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class Plant : MonoBehaviour
{
    private Transform positionTarget;
    Vector3 velocity = Vector3.zero;
    public float maxSpeed = 1;
    public float smoothTime = 0.1f;
    public float wiggleFrequency;
    public float wiggleAmplitude;

    public State currentState = State.beforePlant;
    private float waitTime;
    private Vector3 startPosition;
    public float wanderRadius;

    public Transform[] heartStems;
    public Transform[] hearts;
    private int currentHeartCount;
    public AnimationCurve heartBeat;
    public EventReference movementLoop;
    public float movementLoopVolumeSmoothing = 5f;
    
    public ChainGrow[] chainGrowers;

    private float _targetVolume = 0;

    public float firstWaitTime = 15;
    bool grown = false;
    public enum State
    {
        beforePlant,
        waitStill,
        moveToTarget,
        wait,
    }

    private EventInstance _movementLoop;

    void Start()
    {
        chainGrowers = GetComponentsInChildren<ChainGrow>();
        _movementLoop = RuntimeManager.CreateInstance(movementLoop);
        _movementLoop.start();
        _movementLoop.setVolume(0);
        positionTarget = transform.Find("Position Target").transform;
        positionTarget.parent = null;
        DontDestroyOnLoad(positionTarget);
        startPosition = transform.position;

        currentHeartCount = heartStems.Length;
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDamage.AddListener(DropHeart);
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDamage.RemoveListener(DropHeart);
        _movementLoop.setVolume(0);
        _targetVolume = 0;
    }

    private void Update()
    {
        if (!grown && (currentState == State.wait || currentState == State.moveToTarget))
        {
            grown = true;
            FindObjectOfType<CombatManager>().beginCombat();
        }
        for (int i = 0; i < currentHeartCount; i++)
        {
            float scale = heartBeat.Evaluate(Time.time + (i * 0.1f));
            hearts[i].localScale = new Vector3(scale, scale, 1);
        }
        
        _movementLoop.getPlaybackState(out var pbs);
        
        switch (currentState)
        {
            case State.moveToTarget:

                _targetVolume = Mathf.Lerp(_targetVolume, 1, movementLoopVolumeSmoothing * Time.deltaTime);

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

                _targetVolume = Mathf.Lerp(_targetVolume, 0, movementLoopVolumeSmoothing * Time.deltaTime);
                
                AddWiggle();
                waitTime -= Time.deltaTime;

                if (waitTime <= 0)
                {
                    NewTargetLocation();
                    currentState = State.moveToTarget;
                }

                break;
            case State.waitStill:

                _targetVolume = Mathf.Lerp(_targetVolume, 0, movementLoopVolumeSmoothing * Time.deltaTime);
                
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

        _movementLoop.setVolume(_targetVolume);
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
        Transform heart = heartStems[currentHeartCount];
        heart.parent = null;
        var rb = heart.gameObject.AddComponent<Rigidbody>();
        rb.drag = 0.5f;
        heart.gameObject.GetComponent<DeadHeart>().startDeath = true;
    }

    [EasyButtons.Button]
    public void Grow()
    {
        waitTime = firstWaitTime;
        currentState = State.waitStill;
        foreach (var chainGrow in chainGrowers)
        {
            chainGrow.growing = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("start growth");
            Grow();
        }
    }
}