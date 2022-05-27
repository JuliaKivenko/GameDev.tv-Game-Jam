using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballObjectPool : ObjectPool
{
    public static FireballObjectPool sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        InitializeObjectPool(gameObject);
    }
}
