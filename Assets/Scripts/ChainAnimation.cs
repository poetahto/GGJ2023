using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChainAnimation : MonoBehaviour
{
    public bool ignoreFirstBone;
    private float[] startRotations;
    private List<Transform> bones = new List<Transform>();
    public animSettings[] animationSettings;

    public enum AnimationType
    {
        Perlin,
        Sin,
    }

    [Serializable]
    public struct animSettings
    {
        public AnimationType animationType;
        public float speed;
        public float amplitude;
        public float interval;
        public float offset;
    }

    void Start()
    {
        var t = transform;
        bones.Add(t);
        while (t.childCount > 0)
        {
            t = t.GetChild(0);
            bones.Add(t);
        }


        startRotations = new float[bones.Count];
        for (int i = 0; i < bones.Count; i++)
        {
            startRotations[i] = bones[i].localEulerAngles.z;
        }
    }

    float rotation;
    
    void Update()
    {
        for (int i = ignoreFirstBone ? 1 : 0; i < bones.Count; i++)
        {
            var bone = bones[i];
            bone.localEulerAngles = new Vector3(0, 0, startRotations[i]);
        }

        foreach (animSettings animSet in animationSettings)
        {
            for (int i = ignoreFirstBone ? 1 : 0; i < bones.Count; i++)
            {
                var bone = bones[i];

                switch (animSet.animationType)
                {
                    case AnimationType.Perlin:
                        rotation =
                            ((Mathf.PerlinNoise((Time.time * animSet.speed) + (i * animSet.interval),
                                animSet.offset) * 2) - 1) * animSet.amplitude;
                        break;
                    case AnimationType.Sin:
                        rotation = Mathf.Sin(((Time.time + animSet.offset) * animSet.speed) + (i * animSet.interval)) *
                                   animSet.amplitude;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                bone.localEulerAngles += new Vector3(0, 0, rotation);
            }
        }
    }
}