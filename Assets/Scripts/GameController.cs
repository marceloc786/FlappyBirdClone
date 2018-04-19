using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates {
    START,
    WAITGAME,
    MAINMENU,
    TUTORIAL,
    INGAME,
    GAMEOVER,
    RANKING,
    }

public class GameController : MonoBehaviour {

    public Transform player;
    private Vector3 startPositionPlayer;
    private GameStates currentState = GameStates.START;
    public Text numberScore;
    private int score;
    public float timeToRestart;
    private float currentTimeToRestart;
    private GameOverController gameOverController;
    public SoundController soundController;
    public GameObject mainMenu;
    public GameObject tutorial;

    // Use this for initialization
    void Start () {
        startPositionPlayer = player.position;
        gameOverController = FindObjectOfType(typeof(GameOverController)) as GameOverController;
    }
	
	// Update is called once per frame
	void Update () {
        switch(currentState)
        {
            case GameStates.START:
                {
                    player.position = startPositionPlayer;
                    currentState = GameStates.MAINMENU;
                    mainMenu.SetActive(true);
                    player.gameObject.SetActive(false);

                }
                break;
            case GameStates.MAINMENU:
                {
                    player.position = startPositionPlayer;
                    
                }
                break;
            case GameStates.TUTORIAL:
                {
                    player.position = startPositionPlayer;

                }
                break;
            case GameStates.WAITGAME:
                {
                    player.position = startPositionPlayer;
                    
                }
                break;
            case GameStates.INGAME:
                {
                    numberScore.text = score.ToString();
                }
                break;
            case GameStates.GAMEOVER:
                {
                    currentTimeToRestart += Time.deltaTime;
                    if(currentTimeToRestart > timeToRestart)
                    {
                        currentTimeToRestart = 0;
                        currentState = GameStates.RANKING;
                        
                        gameOverController.SetGameOver(score);
                        soundController.PlaySound(soundsGame.menu);
                    }

                }
                break;
            case GameStates.RANKING:
                {
                    
                    numberScore.enabled = false;
                }
                break;
        }
		
	}

    public void StartGame ()
    {
        currentState = GameStates.INGAME;
        numberScore.enabled = true;
        score = 0;
        gameOverController.HideGameOver();
        tutorial.SetActive(false);
    }

    public GameStates GetCurrentState ()
    {
        return currentState;
    }

    public void CallGameOver ()
    {
        if (currentState != GameStates.GAMEOVER && currentState != GameStates.RANKING) {
            soundController.PlaySound(soundsGame.hit);
            gameOverController.ShowFade();
            currentState = GameStates.GAMEOVER;
        }
    }

    public void CallTutorial()
    {
        currentState = GameStates.TUTORIAL;
        gameOverController.HideGameOver();
        mainMenu.SetActive(false);
        tutorial.SetActive(true);
        player.gameObject.SetActive(true);
        ResetGame();
        soundController.PlaySound(soundsGame.menu);
    }

    public void CallMainMenu()
    {
        currentState = GameStates.MAINMENU;
        gameOverController.HideGameOver();
        mainMenu.SetActive(true);
        player.gameObject.SetActive(false);
        ResetGame();
        soundController.PlaySound(soundsGame.menu);
    }

    public void ResetGame ()
    {
        player.position = startPositionPlayer;
        player.GetComponent<PlayerBehaviour>().RestartRotation();
        ObstaclesBehaviour[] pipesObj = FindObjectsOfType(typeof(ObstaclesBehaviour)) as ObstaclesBehaviour[];
        foreach (ObstaclesBehaviour o in pipesObj)
        {
            o.gameObject.SetActive(false);
        }

        numberScore.enabled = false;
        numberScore.text = score.ToString();
        
    }

    public void AddScore ()
    {
        score++;
        soundController.PlaySound(soundsGame.point);
    }
}
