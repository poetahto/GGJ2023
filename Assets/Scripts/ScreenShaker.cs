using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public float hitshakeDuration = 1;
    public float hitshakeAmp = 1;
    public float shootshakeDuration = 0.1f;
    public float shootshakeAmp = 0.5f;
    Health playerHealth;
    bool shaking = false;
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
            PlayerSingleton.instance.GetComponent<BulletSpawner>().shaker = this;
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
    public void ShootShake()
    {
        if (shaking)
            return;
        StopCoroutine(shootShake());
        StartCoroutine(shootShake());
    }
    IEnumerator shootShake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shootshakeAmp;
        yield return new WaitForSeconds(shootshakeDuration);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    void hitShake(HealthDamageEvent data)
    {

        StopCoroutine(HitShake());
        StartCoroutine(HitShake());
    }
    IEnumerator HitShake()
    {
        shaking = true;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = hitshakeAmp;
        yield return new WaitForSeconds(hitshakeDuration);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        shaking = false;
    }
}
