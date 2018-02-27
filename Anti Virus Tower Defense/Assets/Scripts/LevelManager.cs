using System;
using System.Collections;
using System.Collections.Generic;   //dictionary 
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField]    //Can acces from inspector
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
       get
        {
            return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

	// Use this for initialization
	void Start () {
        //Execute Map Creation
        CreateLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();    //Allcate memory for tile grid dictionary

        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length; //Length of each element in mapData
        int mapY = mapData.Length;  //Length of mapData    


        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); //Coordinate of Top Left Corner of Camera/Screen
        for (int y = 0; y < mapY; y++) //Y 
        {
            char[] newTiles = mapData[y].ToCharArray();


            for (int x = 0; x < mapX; x++) //X 
            {
               PlaceTile(newTiles[x].ToString(), x, y, worldStart);    //Places Tiles Accordingly to Level.txt
         
            }
        }

        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;      //Finding Max Tile (Bottom Right) through Dictionary
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)   //Places Tiles Accordingly to Level.txt
    {
        int tileIndex = int.Parse(tileType);    //Pass tiletype:string to tileIndex:int
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();   //New Object Tile

        //Places tile According Right/Left, Top/Down

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));
        

        Tiles.Add(new Point(x, y), newTile);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');     //Splits text document when reading "-"
    }


   
}
