using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileObjectPool : ObjectPool
{
    public static EnemyProjectileObjectPool sharedInstance;

    void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        InitializeObjectPool(gameObject);
    }
}
