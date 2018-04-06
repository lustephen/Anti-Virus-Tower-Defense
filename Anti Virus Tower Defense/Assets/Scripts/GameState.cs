using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public static int towerHealth = 100;
    public static bool wave_over = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (towerHealth <= 0)
        {
            Debug.Log("CPU Destroyed! Game Over!");
            // Transition to Game Over State
        }
        GameObject enemyManager = GameObject.Find("EnemyManager");
        EnemyManager enemyManagerScript = enemyManager.GetComponent<EnemyManager>();
        if (enemyManagerScript.waveOver())
        {
            enemyManagerScript.nextWave();
            Debug.Log("Wave Over. Starting Next Wave.");
        }
    }
}
