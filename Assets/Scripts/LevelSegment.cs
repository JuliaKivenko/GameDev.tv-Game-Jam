using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScrollingObject))]
public class LevelSegment : MonoBehaviour
{
    public Transform endPosition;
    public ScrollingObject scrollingObject;
    [SerializeField] List<EnemySpawnPoint> enemySpawnPoints = new List<EnemySpawnPoint>();


    //Loop through all the enemy spawn points in the segment and spawn enemies from the pool on each point
    public void ActivateEnemies()
    {
        foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
        {
            enemySpawnPoint.SpawnEnemy();
        }
    }

}
