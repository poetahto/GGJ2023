using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int maxEnemiesPerWave;
    public int maxEnemyCount;
    [Range(0,1)]
    public float PlayerHunterRatio;
    public List<Transform> enemySpawnPositions;
    List<Transform> checkedSpawnPositions;
    bool spawning = false;
    public float spawnCheckRadius;
    public LayerMask enemyBlockers;
    public Slider timer;
    public GameObject sporePrefab;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }
    
    // Start is called before the first frame update
    void Awake()
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
            Destroy(gameObject);
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
        //StartCoroutine(combatDuration());
    }
    public void beginCombat()
    {
        StartCoroutine(combatDuration());

    }
    void resetPlant()
    {
        Destroy(plantInstance);
        plantInstance = Instantiate(plantPrefab, plantSpawn.position, Quaternion.identity);
        plantInstance.SetActive(true);
    }
    IEnumerator combatDuration()
    {
        timer.value = 0;
        timer.gameObject.SetActive(true);
        spawning = true;
        StartCoroutine(SpawnEnemies());
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            timer.value = normalizedTime;
            yield return null;
        }
        TransitionManager.instance.SetIntensity(0);
        print("ending combat encounter");
        TransitionManager.instance.encounterComplete = true;
        FindObjectOfType<DoorBehaviour>().OpenDoor();
        StopCoroutine(SpawnEnemies());
        timer.gameObject.SetActive(false);
        killAllEnemies();
        spawning = false;
        Instantiate(sporePrefab, plantInstance.transform.position, Quaternion.identity);
        Destroy(plantInstance);
    }
    IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(waveInterval);
            for (int i = 0; i < maxEnemiesPerWave;i++) {
                SpawnEnemy();
            }
        }
    }
    float getLivingEnemyCount()
    {
        int total = 0;
        foreach(GameObject enemy in enemies)
        {
            if (enemy&&enemy.active)
            {
                total++;
            }
        }
        return total;
    }
    void SpawnEnemy()
    {
        if (!spawning)
            return;
        if (getLivingEnemyCount() >= maxEnemyCount)
        {
            return;
        }
        //pick random spawn position and place enemy
        Vector3 spawnPos=Vector3.zero;
        while (enemySpawnPositions.Count != 0)
        {
            Transform t=enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)];
            Vector3 potentialSpawnPos = t.position;
            print(potentialSpawnPos);
            checkedSpawnPositions.Add(t);
            enemySpawnPositions.Remove(t);
            RaycastHit hit;
            if (Physics.OverlapSphere(potentialSpawnPos, spawnCheckRadius, enemyBlockers).Length != 0)
            {
                print("spawn blocked");
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
    public void killAllEnemies()
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
        if(plantInstance)
        plantInstance.SetActive(false);
        
        TransitionManager.instance.SetIntensity(0);
        timer.gameObject.SetActive(false);
    }
}
