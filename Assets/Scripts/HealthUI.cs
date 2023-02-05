using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;
    public Slider HealthBar;
    public Health playerHealth;
    public float lowestHeight;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }

    void Awake()
    {
        if (instance == null)
        {
            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public float healthAnimSpeed = 15f;
    public float offsetStart;
    public float offsetEnd;
    private float _targetHealthPercentage = 1;
    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        float t = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        _targetHealthPercentage = Mathf.Lerp(_targetHealthPercentage, t, healthAnimSpeed * Time.deltaTime);
        HealthBar.value = _targetHealthPercentage;
    }
}
