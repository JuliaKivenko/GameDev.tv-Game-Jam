using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSpawnPoint : SpawnPoint
{
    PickUpObjectPool pickUpObjectPool;

    void Awake()
    {
        pickUpObjectPool = PickUpObjectPool.sharedInstance;
    }

    public override void SpawnObjectFromPool()
    {
        SpawnLevelObject<PickUpObjectPool>(pickUpObjectPool);
    }

}
