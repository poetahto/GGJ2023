using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;
    public Image healthLine;
    public Health playerHealth;
    public float lowestHeight;
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

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        float healthPercentage = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        healthLine.rectTransform.position = new Vector3(healthLine.rectTransform.position.x,257+lowestHeight-lowestHeight*healthPercentage, healthLine.rectTransform.position.z);
    }
}
