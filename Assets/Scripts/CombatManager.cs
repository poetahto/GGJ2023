using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public float duration;
    public GameObject plantPrefab;
    GameObject plantInstance;
    public Transform plantSpawn;
    public GameObject plantHunterPrefab;
    public GameObject playerHunterPrefab;
    public List<GameObject> enemies;
    public float waveInterval;
    public int PlantHuntersPerWave;
    public int PlayerHuntersPerWave;
    public List<Transform> enemySpawnPositions;
    bool spawning = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            plantInstance = Instantiate(plantPrefab,transform);
            plantInstance.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

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
        spawning = true;
        StartCoroutine(SpawnEnemies());
        yield return new WaitForSeconds(duration);
        print("ending combat encounter");
        TransitionManager.instance.encounterComplete = true;
        StopCoroutine(SpawnEnemies());
        killAllEnemies();
        spawning = false;
    }
    IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(waveInterval);
            SpawnWave();
        }
    }
    void SpawnWave()
    {
        if (!spawning)
            return;
        print("wave");
        for(int i = 0; i < PlantHuntersPerWave; i++)
        {
            SpawnEnemy(playerHunterPrefab);
        }
        for (int i = 0; i < PlayerHuntersPerWave; i++)
        {
            SpawnEnemy(plantHunterPrefab);
        }
    }
    void SpawnEnemy(GameObject enemyPrefab)
    {
        //pick random spawn position and place enemy
        Vector3 spawnPos = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)].position;
        enemies.Add(Instantiate(enemyPrefab,spawnPos,Quaternion.identity));
    }
    void killAllEnemies()
    {
        foreach(GameObject enemy in enemies)
        {   
            if(enemy!=null)
                enemy.GetComponent<Health>().Damage(10000);
        }
        enemies.Clear();
    }
    public void Cleanup()
    {
        plantInstance.SetActive(false);
    }
}
