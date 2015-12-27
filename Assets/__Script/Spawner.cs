using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public Wave[] waves;
    public Enemy enemy;
    private LivingEntity playerEntiy;
    private Transform player;
    private Wave currentWave;
    private int currentWaveNumber;
    private int enemiesRemaingToSpawn;
    private int enemiesRemaingAlive;
    private float nextSpawnTime;
    private MapGenerator mapGenerator;
    private float campCheckInterval = 2f;
    private float campThresholdDistance = 1.5f;
    private float nextCampCheckTime;
    private Vector3 oldCampPosition;
    private bool isCamping = false;
    private bool isDiabled = false;

    void Start() {
        playerEntiy = FindObjectOfType<Player>();
        player = playerEntiy.transform;
        playerEntiy.OnDeath += OnPlayerDeath;

        nextCampCheckTime = Time.time + campCheckInterval;
        oldCampPosition = player.position;

        mapGenerator = FindObjectOfType<MapGenerator>();
        NextWave();
    }

    void Update() {
        if (!isDiabled) {
            if (Time.time > nextCampCheckTime) {
                nextCampCheckTime = Time.time + campCheckInterval;
                isCamping = (Vector3.Distance(player.position, oldCampPosition) < campThresholdDistance);
                oldCampPosition = player.position;
            }

            if (enemiesRemaingToSpawn > 0 && Time.time > nextSpawnTime) {
                enemiesRemaingToSpawn--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;

                StartCoroutine(SpawnEnemy());
            } 
        }
    }

    IEnumerator SpawnEnemy() {
        float spawnDelay = 1f;
        float tileFlashSpeed = 4f;

        Transform randomTile = mapGenerator.GetRandomOpenTile();
        if (isCamping) {
            randomTile = mapGenerator.GetTileFromPosition(player.position);
        }
        Material tileMat = randomTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0;
        while (spawnTimer < spawnDelay) {
            tileMat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1f));
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        Enemy spawnedEnemy = Instantiate(enemy, randomTile.position + Vector3.up, Quaternion.identity) as Enemy;
        spawnedEnemy.OnDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath() {
        enemiesRemaingAlive--;
        if(enemiesRemaingAlive == 0) {
            NextWave();
        }
    }

    private void OnPlayerDeath() {
        isDiabled = true;
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
