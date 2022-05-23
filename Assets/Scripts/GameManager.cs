using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    int points = 0;

    private void Awake()
    {
        sharedInstance = this;

    }

    private void Update()
    {
        //probably keeps track of increasing difficulty?    
    }
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log("+" + pointsToAdd + "! You now have " + points + " points.");
    }

    public void StartGame()
    {
        //Initializes all the things needed to play the game, like player, object pools and stuff
        //In the future would also be applying increased player stats?
    }

    public void GameOver()
    {
        UIManager.sharedInstance.ActivateGameOverPanel();
        //passes on distance value, then sets it to 0
        //stop the level scroll
        //reset everything needed

    }

}
