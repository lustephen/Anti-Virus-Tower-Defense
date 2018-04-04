using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public static int towerHealth = 100;
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
    }
}
