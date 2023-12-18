using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;

    private readonly float rangeX = 7f;
    private readonly float rangeZ = 10f;
    private int numberOfWave = 1;

    void Start()
    {
        SpawnEnemyWave(numberOfWave);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            numberOfWave++;
            SpawnEnemyWave(numberOfWave);
            SpawnPowerup();
        }
    }

    private void SpawnEnemyWave(int numOfEnemy)
    {
        for (int i = 0; i < numOfEnemy; i++)
        {
            var spawnPosition = GenerateSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        }

    }

    private void SpawnPowerup()
    {
        for (int i = 0; i < numberOfWave; i++)
        {
            var spawnPosition = GenerateSpawnPosition();
            Instantiate(powerupPrefab, spawnPosition, powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        var randomX = Random.Range(-rangeX, rangeX);
        var randomZ = Random.Range(-rangeZ, rangeZ);
        return new Vector3(randomX, 0.15F, randomZ);
    }
}
