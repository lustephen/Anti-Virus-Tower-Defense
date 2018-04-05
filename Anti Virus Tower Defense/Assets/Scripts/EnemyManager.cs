using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyManager : MonoBehaviour {

    public List<Object> enemyPrefabs;
    public float spawnTimer = 5.0f;

    private List<GameObject> enemyList = new List<GameObject>();
    private Vector3 spawnPoint;
    private float currentTick = 0.0f;

	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {

        if (currentTick >= spawnTimer)
        {
            spawnEnemy(enemyPrefabs[Random.Range(0, enemyPrefabs.ToArray().Length)], LevelManager.spawnPoint);
            currentTick = 0.0f;
        }
        currentTick += Time.deltaTime;
	}

    public void spawnEnemy(Object enemy_obj, Vector3 spawnPoint)
    {
        var enemy = PrefabUtility.InstantiatePrefab(enemy_obj) as GameObject;
        enemy.transform.position = spawnPoint;
        enemy.layer = 1;
        enemyList.Add(enemy);
    }

    //nothing to load currently, thus commented out for now
    /*void loadEnemyWave(string filename)
    {
        TextAsset waveFile = Resources.Load(filename) as TextAsset;
    }*/
}
