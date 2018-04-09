using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    //changing towerHealth to lives
    

    private int lives;

    private int wave = 0;

    private bool gameOver = false;

    public static GameState Instance;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text waveText;

    [SerializeField]
    private GameObject waveButton;

    [SerializeField]
    private GameObject gameOverMenu;

    public bool getGameOver()
    {
        return gameOver;
    }

    public int Lives
    {
        get {
            return lives;
        }
        set {
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
        Lives = 10;
	}
	
	// Update is called once per frame
	void Update () {

     
    }

    public void StartWave()
    {
        wave++;
        waveText.text = string.Format("Wave: <color=cyan>(0)</color>", wave);

        waveButton.SetActive(false);
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    void Awake()
    {
        Instance = this;
    }


     public bool Test()
    {
        return (GameObject.Find("Lives") != null);
    }
 

}
