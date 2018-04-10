using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public static int towerHealth = 100;
    public static bool wave_over = false;
    EnemyManager enemyManagerScript;
	// Use this for initialization
	void Start () {
        enemyManagerScript = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        enemyManagerScript.Init();
	}
	
	// Update is called once per frame
	void Update () {

        if (towerHealth <= 0)
        {
            Debug.Log("CPU Destroyed! Game Over!");
            // Transition to Game Over State
        }
        if (enemyManagerScript.waveOver())
        {
            enemyManagerScript.nextWave();
            Debug.Log("Wave Over. Starting Next Wave.");
        }
    }
}
