using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChainGrow : MonoBehaviour
{
    private SpriteSkin spriteSkin;

    public bool ignoreFirstBone;
    public AnimationCurve animationCurve;
    public float timeOffset;
    private float t = 0;

    void Start()
    {
        spriteSkin = GetComponent<SpriteSkin>();

        for (int i = ignoreFirstBone ? 1 : 0; i < spriteSkin.boneTransforms.Length; i++)
        {
            var bone = spriteSkin.boneTransforms[i];
            float scale = animationCurve.Evaluate(0);
            bone.localScale += new Vector3(scale, scale, 1);
        }
    }

    void Update()
    {
        for (int i = ignoreFirstBone ? 1 : 0; i < spriteSkin.boneTransforms.Length; i++)
        {
            var bone = spriteSkin.boneTransforms[i];
            t += Time.deltaTime;
            float scale = animationCurve.Evaluate(t - (timeOffset * i));
            bone.localScale = new Vector3(scale, scale, 1);
        }
    }
}