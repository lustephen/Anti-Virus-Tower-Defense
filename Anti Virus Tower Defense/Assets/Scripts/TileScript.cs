using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get; private set; }
    bool waypoint = false;
    bool spawnPoint = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Point gridPos, Vector3 worldPos, bool waypoint = false, bool spawnPoint = false)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        if (waypoint)
        {
            this.waypoint = true;
        }
    }
}
