using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChainAnimation : MonoBehaviour
{
    private SpriteSkin spriteSkin;

    public bool ignoreFirstBone;
    private float[] startRotations;
    public animSettings[] animationSettings;

    [Serializable]
    public struct animSettings
    {
        public float speed;
        public float amplitude;
        public float perlinInterval;
        public float perlinOffset;
    }

    void Start()
    {
        spriteSkin = GetComponent<SpriteSkin>();
        startRotations = new float[spriteSkin.boneTransforms.Length];
        for (int i = 0; i < spriteSkin.boneTransforms.Length; i++)
        {
            startRotations[i] = spriteSkin.boneTransforms[i].localEulerAngles.z;
        }
    }

    void Update()
    {
        for (int i = ignoreFirstBone ? 1 : 0; i < spriteSkin.boneTransforms.Length; i++)
        {
            var bone = spriteSkin.boneTransforms[i];
            bone.localEulerAngles = new Vector3(0, 0, startRotations[i]);
        }

        foreach (animSettings animSet in animationSettings)
        {
            for (int i = ignoreFirstBone ? 1 : 0; i < spriteSkin.boneTransforms.Length; i++)
            {
                var bone = spriteSkin.boneTransforms[i];
                var rotation =
                    ((Mathf.PerlinNoise((Time.time * animSet.speed) + (i * animSet.perlinInterval),
                        animSet.perlinOffset) * 2) - 1) * animSet.amplitude;
                bone.localEulerAngles += new Vector3(0, 0, rotation);
            }
        }
    }
}