using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sine : MonoBehaviour
{
    Vector3 startPos;
    Vector3 startRot;
    Vector3 startScale;

    public Vector3 posAmp;
    public Vector3 rotAmp;
    public Vector3 scaleAmp;
    public float speed = 1;
    public float offset = 0;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        startScale = transform.localScale;
    }

    void Update()
    {
        transform.localPosition = startPos + Mathf.Sin(Time.time * speed + offset) * posAmp;
        transform.localEulerAngles = startRot + Mathf.Sin(Time.time * speed + offset) * rotAmp;
        transform.localScale = startScale + Mathf.Sin(Time.time * speed + offset) * scaleAmp;
    }
}