using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public int points { get { return _points; } set { } }
    int _points = 0;
    public int pointsForThisRun { get { return _points; } set { } }
    int _pointsForThisRun = 0;

    public float distance { get { return _distance; } set { } }
    float _distance = 0;

    float timePassed;

    public bool isGameRunning = true;

    public delegate void GameOverAction();
    public static event GameOverAction onGameOver;

    private void Awake()
    {
        timePassed = 0;
        sharedInstance = this;

    }

    private void Start()
    {
        StopGame();
    }

    private void Update()
    {
        if (isGameRunning)
        {
            timePassed += Time.deltaTime;
            _distance = Mathf.Round(timePassed * LevelManager.sharedInstance.GetLevelSpeed() * 100);
        }

    }
    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        _pointsForThisRun += pointsToAdd;
        Debug.Log("+" + pointsToAdd + "! You now have " + points + " points.");
    }

    public void StartGame()
    {
        PlayerController.sharedInstance.ActivatePlayerCharacter();
        LevelManager.sharedInstance.StartLevelGeneration();
        _distance = 0;
        _pointsForThisRun = 0;
        timePassed = 0;
        isGameRunning = true;
        //Initializes all the things needed to play the game, like player, object pools and stuff
        //In the future would also be applying increased player stats?
    }

    public void GameOver()
    {
        UIManager.sharedInstance.ActivateGameOverPanel();

        isGameRunning = false;

        if (onGameOver != null)
            onGameOver.Invoke();
    }

    public void StopGame()
    {
        isGameRunning = false;
        LevelManager.sharedInstance.StopLevelGeneration();
    }



}
