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
    private enum TileType       { EMPTY, PATH, WAYPOINT, SPAWNPOINT, USB_TOP, USB_BOTTOM, USB_MIDDLE, USB_HEAD };
    public static Vector3       spawnPoint;
    public static List<Vector3> waypoints = new List<Vector3>();

	// Use this for initialization
	void Start () {
        //Execute Map Creation
        waypoints.Clear();
        CreateLevel("Level");
	}
	
	// Update is called once per frame
	void Update () {
	}

    /*
     * Places the tiles according to the level file given.
     */
    private void CreateLevel(string filename)
    {
        Tiles = new Dictionary<Point, TileScript>();    //Allocate memory for tile grid dictionary
        int[,] mapData = ReadLevelText(filename);

        int mapHeight = mapData.GetLength(0);
        int mapWidth = mapData.GetLength(1);

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); //Coordinate of Top Left Corner of Camera/Screen
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++) //X 
            {
                int tile = mapData[y, x];
                if (tile < 0)
                    break;
                PlaceTile(tile, x, y, worldStart);
            }
        }

        maxTile = Tiles[new Point(mapWidth - 1, mapHeight - 1)].transform.position;      //Finding Max Tile (Bottom Right) through Dictionary
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
        waypoints = findWaypoints(mapData, worldStart);
    }

    /*
     * Given a tile type integer, places the corresponding Tile prefab at the specified coordinates.
     */
    private void PlaceTile(int tileType, int x, int y, Vector3 worldStart)   //Places Tiles Accordingly to Level.txt
    {
        TileScript newTile = Instantiate(tilePrefabs[tileType]).GetComponent<TileScript>();   //New Object Tile
        TileType type = (TileType)tileType;

        Vector3 position = new Vector3(
            worldStart.x + (TileSize * x),
            worldStart.y - (TileSize * y),
            0);

        switch (type)
        {
            case TileType.EMPTY:
                newTile.Setup(new Point(x, y), position, true);
                break;
            case TileType.PATH:
            case TileType.USB_TOP:
            case TileType.USB_MIDDLE:
            case TileType.USB_BOTTOM:
            case TileType.USB_HEAD:
                newTile.Setup(new Point(x, y), position, false);
                break;
            case TileType.WAYPOINT:
                newTile.Setup(new Point(x, y), position, false);
                break;
            case TileType.SPAWNPOINT:
                // center spawn point
                spawnPoint = new Vector3(position.x + (TileSize / 2), position.y - (TileSize / 2), 0);
                newTile.Setup(new Point(x, y), position, false);
                break;
            default:
                Debug.LogError("UNKNOWN TILE TYPE: " + tileType.ToString());
                break;
        }
        Tiles.Add(new Point(x, y), newTile);
    }

    /*
     * Reads level file at the given filename. Converts the space 
     * seperated tile numbers into integers and returns the tile
     * numbers as a 2D array. The end of line '-' character is 
     * represented as a negative integer.
     */
    private int[,] ReadLevelText(string filename)
    {
        TextAsset bindData = Resources.Load(filename) as TextAsset;
        string[] data = bindData.text.Split('\n');
        int[,] level = new int[data.Length, data[0].Split(' ').Length];
        int y = 0;
        foreach (string line in data)
        {
            string[] tile_types = line.Split(' ');
            for (int i = 0; i < tile_types.Length; i++)
            {
                level[y, i] = int.Parse(tile_types[i]); 
            }
            y++;
        }
        return level;
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
    private List<Vector3> findWaypoints(int[,] mapData, Vector3 worldStart)
    {
        // copy data into 2d array
        TileType[,] map = new TileType[mapData.GetLength(0), mapData.GetLength(1)];
        int startx, starty;
        startx = starty = 0;
        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                // we're gonna cheat and get the spawn point while we are copying the map
                TileType type = (TileType)mapData[y,x];
                if (type == TileType.SPAWNPOINT)
                {
                    startx = x;
                    starty = y;
                }
                map[y, x] = type;
            }
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
            if (xcoord < mapData.GetLength(1))
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
            if (ycoord < mapData.GetLength(0))
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
        return waypoints;
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
