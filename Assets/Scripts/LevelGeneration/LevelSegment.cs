using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScrollingObject))]
public class LevelSegment : MonoBehaviour
{
    public Transform endPosition;
    public ScrollingObject scrollingObject;
    [SerializeField] List<SpawnPoint> spawnPoints = new List<SpawnPoint>();


    //Loop through all the enemy spawn points in the segment and spawn enemies from the pool on each point
    public void SpawnAllObjects()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnObjectFromPool();
        }
    }

}
