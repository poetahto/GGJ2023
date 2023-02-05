using System;
using System.Collections;
using System.Collections.Generic;
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
    public Transform orbitAround;

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
        orbitHeight = Random.Range(-0.1f, 0.1f);
        orbitSinAmplitude = Random.Range(0.0f, 0.1f);
        orbitSinSpeed = Random.Range(0.5f, 10f);
        orbitSinOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        if (orbitAround != null)
        {
            transform.localPosition = new Vector3(
                orbitRadius * Mathf.Cos((reverseSpin ? -1 : 1) * Time.time * orbitSpeed + orbitOffset),
                orbitHeight + Mathf.Sin(Time.time * orbitSinSpeed + orbitSinOffset) *
                orbitSinAmplitude,
                orbitRadius * Mathf.Sin((reverseSpin ? -1 : 1) * Time.time * orbitSpeed + orbitOffset));
        }
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