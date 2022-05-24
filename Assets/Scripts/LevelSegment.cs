using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScrollingObject))]
public class LevelSegment : MonoBehaviour
{
    public Transform endPosition;
    public Component[] enemySpawnPoints;

    private void Start()
    {
        //Get all the enemy spawn points in the level segment
        enemySpawnPoints = GetComponentsInChildren(typeof(EnemySpawnPoint));

    }

    //Loop through all the enemy spawn points in the segment and spawn enemies from the pool on each point
    public void ActivateEnemies()
    {
        foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
        {
            enemySpawnPoint.SpawnEnemy();
        }
    }

}
