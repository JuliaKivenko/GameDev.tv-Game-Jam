using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemySpawnPoint : SpawnPoint
{
    ShootingEnemyObjectPool shootingEnemyObjectPool;

    private void Awake()
    {
        shootingEnemyObjectPool = ShootingEnemyObjectPool.sharedInstance;
    }

    public override void SpawnObjectFromPool()
    {
        SpawnLevelObject<ShootingEnemyObjectPool>(shootingEnemyObjectPool);
    }
}
