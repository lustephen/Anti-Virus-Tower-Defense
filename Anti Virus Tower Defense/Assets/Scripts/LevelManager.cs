using System;
using System.Collections;
using System.Collections.Generic;   //dictionary 
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField]    //Can acces from inspector
    private GameObject[] tilePrefabs;

	[SerializeField]
	private GameObject pauseMenu;

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
    private enum TileType { EMPTY, PATH, WAYPOINT, SPAWNPOINT };
    public static Vector3 spawnPoint;
    public static List<Vector3> waypoints = new List<Vector3>();

	// Use this for initialization
	void Start () {
        //Execute Map Creation
        waypoints.Clear();
        Debug.Log(waypoints.Count);
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
        findWaypoints(mapData, worldStart);
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)   //Places Tiles Accordingly to Level.txt
    {
        int tileIndex = int.Parse(tileType);    //Pass tiletype:string to tileIndex:int
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();   //New Object Tile
        TileType type = (TileType)tileIndex;

        Vector3 position = new Vector3(
            worldStart.x + (TileSize * x),
            worldStart.y - (TileSize * y),
            0);
        //Places tile According Right/Left, Top/Down
        switch (type)
        {
            case TileType.EMPTY:
            case TileType.PATH:
                newTile.Setup(new Point(x, y), position);
                break;
            case TileType.WAYPOINT:
                newTile.Setup(new Point(x, y), position, true);
                break;
            case TileType.SPAWNPOINT:
                // center spawn point
                spawnPoint = new Vector3(position.x + (TileSize / 2), position.y - (TileSize / 2), 0);
                newTile.Setup(new Point(x, y), position, false, true);
                break;
            default:
                Debug.LogError("UNKNOWN TILE TYPE: " + tileIndex.ToString());
                break;
        }
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));
        Tiles.Add(new Point(x, y), newTile);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');     //Splits text document when reading "-"
    }

	private void HandleEscape()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			showPauseMenu ();
		}
	}

	public void showPauseMenu()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
	}

    /* Lord forgive me for this hacky code I am about to write. It pains me to do this. */
    private void findWaypoints(string[] mapData, Vector3 worldStart)
    {
        // copy data into 2d array
        TileType[,] map = new TileType[mapData.Length, mapData[0].Length];
        int y = 0;
        int startx, starty;
        startx = starty = 0;
        foreach (string row in mapData)
        {
            int x = 0;
            foreach (char c in row)
            {
                // we're gonna cheat and get the spawn point while we are copying the map
                int tileIndex = int.Parse(c.ToString());
                TileType type = (TileType)tileIndex;
                if (type == TileType.SPAWNPOINT)
                {
                    startx = x;
                    starty = y;
                }
                map[y, x] = type;
                x++;
            }
            y++;
        }

        // use depth first search to find waypoints in order along path.
        Stack stack = new Stack();
        List<int> first = new List<int> { starty, startx };
        stack.Push(first);
        List<List<int>> visited = new List<List<int>>();
        visited.Add(first);
        while (stack.Count != 0)
        {
            List<int> s = (List<int>)stack.Peek();
            int ycoord = s[0];
            int xcoord = s[1];
            stack.Pop();

            if (!point_in(s, visited))
            {
                if (map[ycoord, xcoord] == TileType.WAYPOINT|| map[ycoord, xcoord] == TileType.SPAWNPOINT)
                {
                    // add waypoint
                    Vector3 position = new Vector3(worldStart.x + (TileSize * xcoord) ,
                        worldStart.y - (TileSize * ycoord), 0);
                    Vector3 centered = new Vector3(position.x + (TileSize / 2),
                        position.y - (TileSize / 2), 0);

                    waypoints.Add(centered);
                }
                visited.Add(s);
            }

            // Find next path by check tiles on side of current tile
            if (xcoord > 0)
            {
                if (map[ycoord, xcoord - 1] == TileType.PATH || map[ycoord, xcoord - 1] == TileType.WAYPOINT)
                {
                    List<int> next = new List<int> { ycoord, xcoord - 1 };
                    if (!point_in(next, visited))
                    {
                        stack.Push(next);
                    }
                }
            }
            if (xcoord < mapData[0].Length)
            { 
                if(map[ycoord, xcoord + 1] == TileType.PATH || map[ycoord, xcoord + 1] == TileType.WAYPOINT)
                {
                    List<int> next = new List<int> { ycoord, xcoord + 1 };
                    if(!point_in(next, visited))
                    {
                        stack.Push(next);
                    }
                }
            }
            if (ycoord > 0)
            {
                if (map[ycoord - 1, xcoord] == TileType.PATH || map[ycoord - 1, xcoord] == TileType.WAYPOINT)
                {
                    List<int> next = new List<int> { ycoord - 1, xcoord };
                    if (!point_in(next, visited))
                    {
                        stack.Push(next);
                    }
                }
            }
            if (ycoord < mapData.Length)
            { 
                if (map[ycoord+1, xcoord] == TileType.PATH || map[ycoord+1, xcoord] == TileType.WAYPOINT)
                {
                    List<int> next = new List<int> { ycoord+1, xcoord };
                    if (!point_in(next, visited))
                    {
                        stack.Push(next);
                    }
                }
            }
        }
    }

    bool point_in(List<int> s, List<List<int>> visited)
    {
        foreach (List<int> prev in visited)
        {
            if (s[0] == prev[0] && s[1] == prev[1])
                return true;
        }
        return false;
    }
}
