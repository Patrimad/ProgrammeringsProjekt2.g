using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public int spawnAmount = 10;
    public GameObject enemyPrefab;
    public float spawnCoolDown = 30f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), 0f, spawnCoolDown);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            GameObject randomSpawnPoint = spawnPoints[randomIndex];

            Instantiate(enemyPrefab, randomSpawnPoint.transform.position + new Vector3(0, 1, 0), Quaternion.identity);


        }
    }
}