using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyObjectPool : ObjectPool
{
    public static ShootingEnemyObjectPool sharedInstance;

    void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        InitializeObjectPool(gameObject);
    }
}
