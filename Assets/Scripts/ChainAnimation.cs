using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChainAnimation : MonoBehaviour
{
    private SpriteSkin spriteSkin;

    public bool ignoreFirstBone;
    private float[] startRotations;

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
            var rotation = startRotations[i] + ((Mathf.PerlinNoise(Time.time, 0) * 2) - 1) * 45f;
            bone.localEulerAngles = new Vector3(0, 0, rotation);
        }
    }
}