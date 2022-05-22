using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    int points = 0;

    private void Awake()
    {
        sharedInstance = this;
    }
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log("+" + pointsToAdd + "! You now have " + points + " points.");
    }
}
