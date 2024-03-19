using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject prefab;
    [Range(0, 1)]
    public float spawnChance;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private Transform player;
    [SerializeField] private int initialEnemiesPerWave = 10;
    [SerializeField] private int enemiesIncreasePerWave = 5;
    [SerializeField] private float minSpawnRadius = 5f;
    [SerializeField] private float maxSpawnRadius = 15f;
    [SerializeField] private float minDistanceBetweenEnemies = 2f;
    [SerializeField] private float spawnDelayBetweenEnemies = 1f;
    [SerializeField] private float timeBetweenWaves = 30f;
    [SerializeField] private float waveDelayDecrease = 1f;
    [SerializeField] private float spawnRate = 1f;

    private int currentWave = 1;
    private int enemiesToSpawn;
    private int enemiesSpawned;
    private float nextSpawnTime;
    private float nextWaveTime;

    private Transform cachedPlayerTransform;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned in EnemySpawner.");
            enabled = false;
            return;
        }

        enemiesToSpawn = initialEnemiesPerWave;
        enemiesSpawned = 0;
        nextSpawnTime = Time.time;
        nextWaveTime = Time.time + timeBetweenWaves;

        cachedPlayerTransform = player;
    }

    private void Update()
    {
        if (Time.time >= nextWaveTime)
        {
            currentWave++;
            enemiesToSpawn += enemiesIncreasePerWave;
            enemiesSpawned = 0;
            nextWaveTime = Time.time + timeBetweenWaves;
            timeBetweenWaves -= waveDelayDecrease;
        }

        if (enemiesSpawned < enemiesToSpawn && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            enemiesSpawned++;
            nextSpawnTime = Time.time + (1 / spawnRate);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            float randomChance = Random.value;
            if (randomChance <= enemyTypes[i].spawnChance)
            {
                GameObject enemyPrefab = enemyTypes[i].prefab;

                Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
                Vector3 spawnPosition = cachedPlayerTransform.position + new Vector3(randomDirection.x, 0, randomDirection.y);

                Collider[] colliders = Physics.OverlapSphere(spawnPosition, minDistanceBetweenEnemies);
                if (colliders.Length == 0)
                {
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    break; 
                }
            }
        }
    }
}
