using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    private float spawnTimer;
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

        if (currentScore >= 0 && currentScore <= 25)
        {
            return enemyPrefabs[0];
        }
        else if (currentScore >= 26 && currentScore <= 50)
        {
            return enemyPrefabs[Random.Range(0, 1)];
        }
        else if (currentScore >= 51 && currentScore <= 75)
        {
            return enemyPrefabs[Random.Range(0, 2)];
        }
        else
        {
            return enemyPrefabs[Random.Range(0, 3)];
        }

    }
}
