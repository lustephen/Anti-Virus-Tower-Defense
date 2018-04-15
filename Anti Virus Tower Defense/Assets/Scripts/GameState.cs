using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

   
    public static bool wave_over = false;

    public static GameState Instance;
    private int lives;

    private bool gameOver = false;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private GameObject GameOverMenu;
        
        
    EnemyManager enemyManagerScript;

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            this.lives = value;
            
            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }

            livesText.text = value.ToString();
        }
    }



	// Use this for initialization
	void Start () {
        Lives = 5;

        enemyManagerScript = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        enemyManagerScript.Init();
	}
	
	// Update is called once per frame
	void Update () {

        if (enemyManagerScript.waveOver())
        {
            enemyManagerScript.nextWave();
            Debug.Log("Wave Over. Starting Next Wave.");
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            GameOverMenu.SetActive(true);
        }
    }


    public void Awake()
    {
        Instance = this;
    }

    public bool Test()
    {
        return (GameObject.Find("Lives") != null);
    }

    public bool Test2()
    {
        return (GameObject.Find("GameOver") != null);
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public int getlivesText()
    {
        return int.Parse(livesText.ToString());
    }
}
