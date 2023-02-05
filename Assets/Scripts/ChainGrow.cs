using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChainGrow : MonoBehaviour
{
    private List<Transform> bones = new List<Transform>();

    public bool ignoreFirstBone;
    public AnimationCurve animationCurve;
    public float timeOffset;
    private float t = 0;
    public bool growing = false;

    void Start()
    {
        var t = transform;
        bones.Add(t);
        while (t.childCount > 0)
        {
            t = t.GetChild(0);
            bones.Add(t);
        }

        for (int i = ignoreFirstBone ? 1 : 0; i < bones.Count; i++)
        {
            var bone = bones[i];
            float scale = animationCurve.Evaluate(0);
            bone.localScale = new Vector3(scale, scale, 1);
        }
    }

    void Update()
    {
        if(!growing) return;
        
        for (int i = ignoreFirstBone ? 1 : 0; i < bones.Count; i++)
        {
            var bone = bones[i];
            t += Time.deltaTime;
            float scale = animationCurve.Evaluate(t - (timeOffset * i));
            bone.localScale = new Vector3(scale, scale, 1);

            if (t > animationCurve.keys[animationCurve.length - 1].time)
                Destroy(this);
        }
    }
}