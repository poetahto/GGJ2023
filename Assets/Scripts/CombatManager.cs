using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public float duration;
    public GameObject plantPrefab;
    GameObject plantInstance;
    public Transform plantSpawn;
    // Start is called before the first frame update
    void Start()
    {
        plantInstance = Instantiate(plantPrefab);
        plantInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartCombatEncounter()
    {
        print("started combat encounter");
        resetPlant();
        StartCoroutine(combatDuration());
    }
    void resetPlant()
    {
        plantInstance.transform.position = plantSpawn.position;
        plantInstance.SetActive(true);
    }
    IEnumerator combatDuration()
    {
        yield return new WaitForSeconds(duration);
        print("ending combat encounter");
        
    }
    public void Cleanup()
    {
        plantInstance.SetActive(false);
    }
}
