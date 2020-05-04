using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject path;
    public List<Enemy> enemies;
    
    /* Spawn Timing */
    public float startSpawnRate = 5f;
    public float minSpawnRate = 2f;
    public float decSpawnRatePerWave = 0.5f;
    
    public float spawnRate;
    public float spawnTimer;

    /* Waves */
    public int waveNumber = 1;

    public int startNumEnemies = 5;
    public int maxNumEnemies = 100;
    public int numEnemiesPerWave = 5;

    public int numEnemies;
    public int numSpawned = 0;
    public int numDied = 0;

    /* Peace time */
    public float delayTime = 10f;
    public float delayTimer;
    public bool delay = true;
    
    /* Misc */
    System.Random random;

    void Start()
    {
        random = new System.Random();
        spawnRate = startSpawnRate;
        numEnemies = startNumEnemies;
        delayTimer = delayTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(!delay) {

            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0 && numSpawned < numEnemies) {
                SpawnEnemy();
                numSpawned++;
                spawnTimer = GetTimeUntilNext();
            }

            if(numSpawned >= numEnemies && numSpawned == numDied) {
                delay = true;
                StartWave();
            }

        } else {
            delayTimer -= Time.deltaTime;
            if(delayTimer <= 0f) {
                delayTimer = delayTime;
                delay = false;
            }
        }


    }

    public void StartWave() {
        numSpawned = 0;
        numDied = 0;
        waveNumber++;
        numEnemies = Mathf.Min(maxNumEnemies, startNumEnemies + (waveNumber-1) * numEnemiesPerWave);
        spawnRate = Mathf.Max(minSpawnRate, startSpawnRate - (waveNumber-1) * decSpawnRatePerWave);
    }

    float GetTimeUntilNext() {
        return ((float)random.NextDouble() + 0.5f) * spawnRate;
    }

    void SpawnEnemy() {
        /* pick enemy */
        float choose = (float)random.NextDouble() * enemies.Count;
        int index = Mathf.FloorToInt(Mathf.Clamp(choose + (waveNumber-1)/2f - 4f, 0f, enemies.Count - 1));

        Enemy enemy = Instantiate(enemies[index], transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.path = path;
    }
}
