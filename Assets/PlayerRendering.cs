using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRendering : MonoBehaviour
{
    public Transform spriteRoot;
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    public float maxTilt;
    private ChainAnimation[] chainAnimations;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        chainAnimations = GetComponentsInChildren<ChainAnimation>();
    }

    void Update()
    {
        float maxSpeed = playerMovement.groundedMovement.speed;
        float t = Mathf.InverseLerp(-maxSpeed, maxSpeed, rb.velocity.x);
        float tilt = Mathf.Lerp(maxTilt, -maxTilt, t);
        spriteRoot.localEulerAngles = new Vector3(0, 0, tilt);
        spriteRoot.localScale = new Vector3(tilt > 0 ? -1 : 1, 1, 1);
        foreach (ChainAnimation chainAnimation in chainAnimations)
        {
            foreach (ChainAnimation.AnimSettings animationSetting in chainAnimation.animationSettings)
            {
                animationSetting.MultAmplitude(Mathf.Abs(tilt) / maxTilt);
            }
        }
    }
}