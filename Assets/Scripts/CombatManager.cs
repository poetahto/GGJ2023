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
    [Range(0,1)]
    public float PlayerHunterRatio;
    public List<Transform> enemySpawnPositions;
    List<Transform> checkedSpawnPositions;
    bool spawning = false;
    public float spawnCheckRadius;
    public LayerMask enemyBlockers;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            plantInstance = Instantiate(plantPrefab,transform);
            plantInstance.SetActive(false);
            checkedSpawnPositions = new List<Transform>();
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
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        if (!spawning)
            return;
        //pick random spawn position and place enemy
        Vector3 spawnPos=Vector3.zero;
        while (enemySpawnPositions.Count != 0)
        {
            Transform t=enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)];
            Vector3 potentialSpawnPos = t.position;
            checkedSpawnPositions.Add(t);
            enemySpawnPositions.Remove(t);
            if (Physics2D.CircleCast(spawnPos, spawnCheckRadius,Vector2.down,0,enemyBlockers))
            {
                ;
            }
            else
            {
                spawnPos = potentialSpawnPos;
                break;
            }
        }
        foreach(Transform t in checkedSpawnPositions)
        {
            enemySpawnPositions.Add(t);
        }
        checkedSpawnPositions.Clear();
        if (spawnPos == Vector3.zero)
            return;
        if(Random.Range(0f,1f)<PlayerHunterRatio)
            enemies.Add(Instantiate(playerHunterPrefab,spawnPos,Quaternion.identity));
        else
            enemies.Add(Instantiate(plantHunterPrefab, spawnPos, Quaternion.identity));
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
