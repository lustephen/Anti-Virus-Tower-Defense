using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

   
    public static bool wave_over = false;

    private bool gameOver = false;


    public static GameState Instance;



    private int lives;

    private int score;

    private int currency;

    private int wave;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text currencyText;

    [SerializeField]
    private Text waveText;

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

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            this.score = value;

            if (score <= 0)
            {
                this.score = 0;
            }

            scoreText.text = string.Format("<color=cyan>Score:</color> {0}", score);
        }
    }

    public int Currency
    {
        get
        {
            return Currency;
        }
        set
        {
            this.currency = value;
            currencyText.text = string.Format("<color=lime>$</color> {0}", currency);
        }
    }

    public int Wave
    {
        get
        {
            return wave;

        }
        set
        {
            this.wave = value;
            waveText.text = string.Format("Wave: <color=cyan>{0}</color>", wave);
        }
    }

	// Use this for initialization
	void Start () {
        Score = 200;
        Lives = 5;
        Currency = 100;

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
