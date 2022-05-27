using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : ObjectPool
{
    public static EnemyObjectPool sharedInstance;


    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        InitializeObjectPool(gameObject);
    }

}
