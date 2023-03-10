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
        _scaleTarget = spriteRoot.transform.localScale;
        chainAnimations = GetComponentsInChildren<ChainAnimation>();
    }

    private Vector3 _scaleTarget;
    public float rotateTime = 10f;
    public bool useRotationLerping = true;
    
    void Update()
    {
        float maxSpeed = playerMovement.groundedMovement.speed;
        float t = Mathf.InverseLerp(-maxSpeed, maxSpeed, rb.velocity.x);
        float tilt = Mathf.Lerp(maxTilt, -maxTilt, t);
        spriteRoot.localEulerAngles = new Vector3(0, 0, tilt);
        if (Mathf.Abs(tilt) > 0.1f)
        {
            if (useRotationLerping)
            {
                _scaleTarget = new Vector3(tilt > 0 ? -1 : 1, 1, 1);
            }
            else
            {
                spriteRoot.localScale = new Vector3(tilt > 0 ? -1 : 1, 1, 1);
            }
            // _scaleTarget = tilt > 0 ? -1 : 1;
        }

        if (useRotationLerping)
        {
            spriteRoot.localScale = Vector3.Lerp(spriteRoot.localScale, _scaleTarget, rotateTime * Time.deltaTime);
        }
        
        foreach (ChainAnimation chainAnimation in chainAnimations)
        {
            foreach (ChainAnimation.AnimSettings animationSetting in chainAnimation.animationSettings)
            {
                float w = Mathf.InverseLerp(0, maxSpeed, rb.velocity.magnitude);
                animationSetting.MultAmplitude(w);
            }
        }
    }
}