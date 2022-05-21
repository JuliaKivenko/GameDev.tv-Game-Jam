using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool sharedInstance;

    public ObjectPool objectPool;

    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        objectPool.InitializeObjectPool(gameObject);
    }

}
