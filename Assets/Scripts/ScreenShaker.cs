using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public float hitshakeDuration = 1;
    public float hitshakeAmp = 1;
    Health playerHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    private void OnDisable()
    {
        playerHealth.onDamage.RemoveListener(hitShake);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            playerHealth = PlayerSingleton.instance.GetComponent<Health>();
            playerHealth.onDamage.AddListener(hitShake);
        }
        if (Input.GetKey(KeyCode.L))
        {
            StopCoroutine(HitShake());
            StartCoroutine(HitShake());
        }
        else
        {

        }
    }
    void hitShake(float amount)
    {
        StopCoroutine(HitShake());
        StartCoroutine(HitShake());
    }
    IEnumerator HitShake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = hitshakeAmp;
        yield return new WaitForSeconds(hitshakeDuration);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
