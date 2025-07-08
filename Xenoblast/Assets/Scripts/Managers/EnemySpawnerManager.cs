using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    private float spawnTimer;
    private int lastScoreTracker = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval; // Reset timer
        }

        DecrementSpawnInterval();
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No spawn points or enemy prefabs assigned!");
            return;
        }

        // Selects a random spawner on the map to spawn the enemy
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject enemy = SelectEnemy();

        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }

    private GameObject SelectEnemy()
    {

        int currentScore = GameManager.gameManagerInstance.score;
        float probability = Random.value;

        if (currentScore >= 0 && currentScore <= 15)
        {
            return enemyPrefabs[0];
        }
        else if (currentScore >= 16 && currentScore <= 50)
        {
            // 40% of spawning SpiderEnemy, 60% of spawning DefaultEnemy
            if (probability <= 0.40f)
            {
                return enemyPrefabs[1];
            }
            else
            {
                return enemyPrefabs[0];
            }
        }
        else if (currentScore >= 51 && currentScore <= 75)
        {
            return enemyPrefabs[Random.Range(0, 3)];
        }
        else
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }

    }

    private void DecrementSpawnInterval()
    {
        int currentScore = GameManager.gameManagerInstance.score;
        if (spawnInterval <= 1f)
        {
            return;
        }

        // Spawn interval decreases by 1 second after ever 25 score
        if (currentScore != 0 && currentScore % 25 == 0 && currentScore != lastScoreTracker)
        {
            spawnInterval -= 0.5f;
            lastScoreTracker = currentScore;
            Debug.Log("Spawn interval decreased to: " + spawnInterval);
        }
    }
}
