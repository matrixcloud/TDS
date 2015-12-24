using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public Wave[] waves;
    public Enemy enemy;
    private Wave currentWave;
    private int currentWaveNumber;
    private int enemiesRemaingToSpawn;
    private int enemiesRemaingAlive;
    private float nextSpawnTime;


    void Start() {
        NextWave();
    }

    void Update() {
        if(enemiesRemaingToSpawn > 0 && Time.time > nextSpawnTime) {
            enemiesRemaingToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnDeath;
        }
    }

    private void OnDeath() {
        Debug.Log("Enemy died");
        enemiesRemaingAlive--;
        if(enemiesRemaingAlive == 0) {
            NextWave();
        }
    }

    private void NextWave() {
        currentWaveNumber++;
        if (currentWaveNumber <= waves.Length) {
            Debug.Log("Wave: " + currentWaveNumber);
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemaingToSpawn = currentWave.enemyCount;
            enemiesRemaingAlive = enemiesRemaingToSpawn; 
        }
    }

    [System.Serializable]
    public class Wave {
        public int enemyCount;
        public float spawnInterval;
    }
}
