using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

/* 
 * Wave contains information about the order in which enemies need to be spawned.
 */
public class Wave
{
    private Queue<Object> wave_order;
    private float spawn_timer;
    public int totalEnemies;  // used to detect if we are ready to go to next wave.

    public Wave(float spawn_timer, string spawn_data, List<Object> enemyPrefabs)
    {
        this.spawn_timer = spawn_timer;
        this.wave_order = new Queue<Object>();
        createFromSpawnData(spawn_data, enemyPrefabs);
        totalEnemies = wave_order.Count;
    }

    /*
     * Parses a single wave line and pushes the enemies to the wave_order queue.
     */
    private void createFromSpawnData(string spawn_data, List<Object> enemyPrefabs)
    {
        string[] enemy_info = spawn_data.Split(' '); // [ID-NUM, ID-NUM, ...]
        foreach (string entry in enemy_info)
        {
            string[] split = entry.Split('-');
            if (split.Length != 2)
            {
                Debug.LogError("Wave Error. Incorrect EnemyWave format: " + entry);
                throw new System.ArgumentException("Incorrect EnemyWave format");
            }
            int enemyType = int.Parse(split[0]);
            if (enemyType > enemyPrefabs.ToArray().Length-1 || enemyType < 0)
            {
                Debug.LogError("Wave Error. No enemy of that type indexable at " + enemyType);
                throw new System.ArgumentException("Uknown Enemy Type");
            }
            Object enemy = enemyPrefabs[enemyType];
            int spawnNumber = int.Parse(split[1]);
            for (int i = 0; i < spawnNumber; i++)
            {
                pushEnemy(enemy);
            }
        }
    }

    public void pushEnemy(Object obj)
    {
        wave_order.Enqueue(obj);
    }

    public Object getNextEnemy()
    {
        Object enemy = wave_order.Dequeue();
        return enemy;
    }

    public bool isFinished()
    {
        return wave_order.Count == 0;
    }
}

/*
 * EnemyManager is responsible for spawning Enemies according to the wave file.
 */
public class EnemyManager : MonoBehaviour {

    public List<Object> enemyPrefabs; // The available enemies to be spawned.
    public float spawnTimer = 5.0f;   // How long between spawns.

    private List<Wave> waves = new List<Wave>();
    private Vector3 spawnPoint;
    private float currentTick = 0.0f; // count for spawnTimer.
    private int currentWaveIndex;
    private int enemiesRemaining;

	void Start () {
	}

    public void Init()
    {
        waves = loadEnemyWaves("Waves");
        enemiesRemaining = waves[currentWaveIndex].totalEnemies;
        currentWaveIndex = 0;
    }

    // Update is called once per frame
    void Update () {
        if (waves.Count != 0 && !waves[currentWaveIndex].isFinished() && currentWaveIndex < waves.ToArray().Length)
        {
            spawnWave(currentWaveIndex);
        }
	}

    void spawnWave(int waveNumber)
    {
        if (currentTick >= spawnTimer)
        {
            spawnEnemy(waves[waveNumber].getNextEnemy(), LevelManager.spawnPoint);
            currentTick = 0.0f;
        }
        currentTick += Time.deltaTime;
    }

    public bool waveOver()
    {
        return enemiesRemaining == 0;
    }

    public void destroyEnemy()
    {
        enemiesRemaining--;
    }

    public void nextWave()
    {
        currentWaveIndex++;
        enemiesRemaining = waves[currentWaveIndex].totalEnemies;
    }

    /*
     * Creates the given enemy at the given spawnPoint.
     */
    public void spawnEnemy(Object enemy_obj, Vector3 spawnPoint)
    {
        var enemy = PrefabUtility.InstantiatePrefab(enemy_obj) as GameObject;
        enemy.transform.position = spawnPoint;
        enemy.layer = 1;
    }

    /* 
     * Loads the wave data. Individual waves are in format:
     * UNIT UNIT UNIT
     * ;
     * UNIT UNIT UNIT
     * ;
     * Where UNIT is of form ID-NUMBER where ID is an index into enemyPrefabs
     * and NUMBER is the amount to be spawned. The ';' symbol indicates the
     * end of a wave.
     */
    List<Wave> loadEnemyWaves(string filename)
    {
        TextAsset waveFile = Resources.Load(filename) as TextAsset;
        string waveText = waveFile.text;
        string[] waves_unfiltered = waveText.Split(';');
        List<string> waves_filtered = new List<string>();
        foreach (string str in waves_unfiltered)
        {
            string filtered = filterWaveData(str);
            waves_filtered.Add(filtered);
        }

        List<Wave> waves = new List<Wave>();
        foreach (string spawn_data in waves_filtered)
        {
            Wave wave = new Wave(this.spawnTimer, spawn_data, enemyPrefabs);
            waves.Add(wave);
        }
        return waves;
    }

    string filterWaveData(string unfiltered_wave)
    {
        var sb = new StringBuilder(unfiltered_wave.Length);

        foreach (char i in unfiltered_wave)
        {
            if (i != '\n' && i != '\r' && i != '\t' && i != '\0')
            {
                sb.Append(i);
            }
        }
        return sb.ToString();
    }
}
