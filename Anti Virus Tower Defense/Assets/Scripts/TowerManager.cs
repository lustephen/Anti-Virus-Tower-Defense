using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerManager : MonoBehaviour {

    public List<Object> enemyPrefabs;  // The available towers to be placed.
    public int selectedTower = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            int layerMask = 1 << 8; // Mask the 8th layer (Tiles Layer) so we don't have hits on tower colliders.
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 12, layerMask);
            if (hit)
            { 
                if (hit.collider.gameObject.tag != "Tower" && hit.collider.gameObject.GetComponent<TileScript>().canPlaceTower)
                {
                    Vector3 tilePosition = hit.collider.transform.position;
                    hit.collider.gameObject.GetComponent<TileScript>().canPlaceTower = false;
                    placeTower(enemyPrefabs[selectedTower], tilePosition);
                }
                else
                {
                    print("Tower already at that location");
                    print(hit.collider.name);
                }
            }
        }
	}

    private void placeTower(Object towerPrefab, Vector3 position)
    {
        var tower = PrefabUtility.InstantiatePrefab(towerPrefab) as GameObject;
        tower.GetComponent<BasicTower>().Init(position);
        tower.layer = 1;
    }
}
