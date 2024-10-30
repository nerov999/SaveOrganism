using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f; 
    public float spawnRadius = 5f; 
    public int minEnemies = 1; 
    public int maxEnemies = 3; 
    private float nextSpawnTime; 

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemies()
    {
        int numEnemies = UnityEngine.Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            float randomX = UnityEngine.Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius);
            float randomZ = UnityEngine.Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius);
            Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
