using System;
using System.Collections;
using System.Collections.Generic;
using Collectables;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rune : MonoBehaviour
{
    public Sprite[] runes;
    public Sprite[] glows;

    public int runeIndex;

    [HideInInspector] public SpriteRenderer rune;
    [HideInInspector] public SpriteRenderer glow;

    public bool reverseSpin;

    [HideInInspector] public float orbitRadius;
    [HideInInspector] public float orbitSpeed;
    [HideInInspector] public float orbitOffset;
    [HideInInspector] public float orbitHeight;
    [HideInInspector] public float orbitSinAmplitude;
    [HideInInspector] public float orbitSinSpeed;
    [HideInInspector] public float orbitSinOffset;
    private Transform orbitAround;
    private Vector3 velocity;
    private Vector3 scaleVelocity;

    public float attachSpeed = 1;
    public float attachSmoothTime = 1;

    private void OnValidate()
    {
        rune = transform.GetChild(0).GetComponent<SpriteRenderer>();
        glow = rune.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SetRuneIndex(runeIndex);
    }

    private void Start()
    {
        rune = transform.GetChild(0).GetComponent<SpriteRenderer>();
        glow = rune.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SetRuneIndex(runeIndex);

        if (orbitAround != null)
            transform.parent = orbitAround;

        orbitRadius = Random.Range(0.5f, 0.8f);
        orbitSpeed = Random.Range(1f, 3f);
        orbitOffset = Random.Range(0f, Mathf.PI * 2f);
        orbitHeight = Random.Range(0.3f, 0.6f);
        orbitSinAmplitude = Random.Range(0.0f, 0.1f);
        orbitSinSpeed = Random.Range(0.5f, 10f);
        orbitSinOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        if (orbitAround != null)
        {
            Vector3 targetOrbit = new Vector3(
                orbitRadius * Mathf.Cos((reverseSpin ? -1 : 1) * Time.time * orbitSpeed + orbitOffset),
                orbitHeight + Mathf.Sin(Time.time * orbitSinSpeed + orbitSinOffset) *
                orbitSinAmplitude,
                orbitRadius * Mathf.Sin((reverseSpin ? -1 : 1) * Time.time * orbitSpeed + orbitOffset));

            transform.localPosition = Vector3.SmoothDamp(
                transform.localPosition, targetOrbit, ref velocity, attachSmoothTime, attachSpeed);

            rune.transform.localScale = Vector3.SmoothDamp(
                rune.transform.localScale, Vector3.one, ref scaleVelocity, attachSmoothTime, attachSpeed);
        }
    }

    [EasyButtons.Button]
    public void AttachRune(Transform t)
    {
        orbitAround = t;
        transform.parent = t;
    }
    
    public void AttachRune(ItemCollectEvent e)
    {
        orbitAround = e.collector.transform;
        transform.parent = e.collector.transform;
    }

    public void SetRuneIndex(int index)
    {
        if (index >= runes.Length)
            throw new Exception("Rune index out of range! (index: " + index + ", length: " + runes.Length + ")");

        runeIndex = index;
        rune.sprite = runes[runeIndex];
        glow.sprite = glows[runeIndex];
    }
}