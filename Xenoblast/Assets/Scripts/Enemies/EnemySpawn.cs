using UnityEngine;
//  TEMPORARY ENEMY SPAWNER
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
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
        Vector2 spawnPosition = transform.position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
