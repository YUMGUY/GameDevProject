using System; // contains [Serializable]
using System.Collections;
using UnityEngine;
using TMPro; // for interfacing with the system's subobjects (timer, wave number display, ect)

public class WaveUISystem : MonoBehaviour
{
    [Serializable]
    private class WaveEnemy
    {
        public GameObject enemyToSpawn;
        public int numToSpawn;
    }

    // A group within a wave
    // Used in conjunction with the specific angle spawning mechanic
    [Serializable]
    private class WaveGroup
    {
        public WaveEnemy[] enemiesInGroup;
        public float spawnRadius;
        public float delayForEachSpawn;

        // angles to spawn the enemy group in
        public float angleBegin;
        public float angleEnd;
    }


    [Serializable]
    private class Wave
    {
        public int waveTime;
        public string waveName;
        public string waveDescription;
        public WaveGroup[] waveGroups;
    }

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI waveCounter;

    [SerializeField] Map map;
    [SerializeField] float waveSpawnMult;
    [SerializeField] Wave[] waveList;
    public GameObject playerLocation; // TO BE DELETED

    // Seems that enemies start at order within sorting layer, so start here
    private int layerOrderIndex = 3; 

    // Start is called before the first frame update
    void Start()
    {
        timer.GetComponent<Stopwatch>().startStopwatch();
        waveCounter.GetComponent<WaveCounter>().setWave(0);
    }

    void Update()
    {
        // Check next time and see if it's the same as the next wave event
        // get current wave
        int currentWaveNumber = waveCounter.GetComponent<WaveCounter>().getWave();

        // make sure we don't go out of scope
        if (currentWaveNumber < waveList.Length)
        {
            Wave currentWave = waveList[currentWaveNumber];
            if (currentWave.waveTime <= timer.GetComponent<Stopwatch>().getCurrentSecond())
            {
                Debug.Log("Start wave!: " + currentWave.waveName);
                waveCounter.GetComponent<WaveCounter>().setWave(currentWaveNumber + 1);

                // Spawn the wave
                StartCoroutine(SpawnWave(currentWave));
                
            }
            // display waveSprites
        }
    }

    IEnumerator SpawnWave(Wave currentWave)
    {
        // for each group in wave
            // for each enemy in group
        // Instantiate each enemy within the current wave
        foreach (WaveGroup currWaveGroup in currentWave.waveGroups)
        {
            foreach (WaveEnemy currWaveEnemy in currWaveGroup.enemiesInGroup)
            {
                int spawnCount = (int)Math.Round(currWaveEnemy.numToSpawn * Math.Max(0f, waveSpawnMult * map.getCurrentZone().difficultyMultiplier));
                for (int enemyCounter = 0; enemyCounter < spawnCount; enemyCounter++)
                {
                    GameObject enemySpawned = Instantiate(currWaveEnemy.enemyToSpawn);

                    // find random enemy position
                    Vector3 spawnPos = RandomEnemyPosition(currWaveGroup.spawnRadius, 
                                                           currWaveGroup.angleBegin,
                                                           currWaveGroup.angleEnd,
                                                           1.0f);
                    enemySpawned.transform.position = playerLocation.transform.position + spawnPos;
                    enemySpawned.GetComponent<EnemyOrbSystem>().map = map;

                    // Edit the sprite renderer's order in layer attribute
                    // so eney sprites do not overlap
                    SpriteRenderer enemySprite = enemySpawned.GetComponent<SpriteRenderer>();
                    if(enemySprite == null) { Debug.LogError("Enemy spawned has no sprite renderer!"); }

                    enemySprite.sortingOrder = layerOrderIndex;
                    ++layerOrderIndex;

                    yield return new WaitForSeconds(currWaveGroup.delayForEachSpawn);
                }
            }
        }
    }

    void getNextWave()
    {
        waveCounter.GetComponent<WaveCounter>().getWave();
    }
    public Vector3 RandomEnemyPosition(float radius, float angleBegin, float angleEnd, float offset)
    {
        float angleBeginRad = angleBegin * Mathf.Deg2Rad;
        float angleEndRad = angleEnd * Mathf.Deg2Rad;

        float randomAngle = UnityEngine.Random.Range(angleBeginRad, angleEndRad);
        float xPos = radius * Mathf.Cos(randomAngle);
        float yPos = radius * Mathf.Sin(randomAngle);
        return new Vector3(xPos,yPos, 0);
    }

    // Checks that all params are filled and valid

}