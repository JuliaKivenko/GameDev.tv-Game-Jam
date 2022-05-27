using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : SpawnPoint
{
    EnemyObjectPool enemyObjectPool;

    private void Awake()
    {
        enemyObjectPool = EnemyObjectPool.sharedInstance;
    }

    public override void SpawnObjectFromPool()
    {
        SpawnLevelObject<EnemyObjectPool>(enemyObjectPool);
    }

}
