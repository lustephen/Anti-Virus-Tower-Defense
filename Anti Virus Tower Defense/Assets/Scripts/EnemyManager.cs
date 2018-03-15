using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] enemyPrefabs;
    public float spawnTimer = 5.0f;

    private List<GameObject> enemyList = new List<GameObject>();
    private float currentTick = 0.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTick >= spawnTimer)
        {
            spawnEnemy();
            currentTick = 0.0f;
        }
        currentTick += Time.deltaTime;
	}

    void spawnEnemy()
    {
        Debug.Log("Spawning new enemy!");
        Vector3 spawnPoint = LevelManager.spawnPoint;
        GameObject enemy = Instantiate(enemyPrefabs[0], spawnPoint, Quaternion.identity);
        enemy.layer = 1;
        enemyList.Add(enemy);
    }
}
