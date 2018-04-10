using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get; private set; }
    public bool canPlaceTower = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
	}

    public void Setup(Point gridPos, Vector3 worldPos, bool canPlaceTower)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        this.canPlaceTower = canPlaceTower;
    }
}
