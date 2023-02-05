using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHeart : MonoBehaviour
{
    private float t = 0;
    public AnimationCurve curve;
    public bool startDeath;

    void Update()
    {
        if(!startDeath) return;

        t += Time.deltaTime;
        transform.localScale = new Vector3(curve.Evaluate(t), curve.Evaluate(t), 1);
        if (t > curve.keys[curve.length - 1].time)
            Destroy(gameObject);
    }
}