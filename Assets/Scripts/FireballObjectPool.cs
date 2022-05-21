using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballObjectPool : MonoBehaviour
{
    public static FireballObjectPool sharedInstance;
    public ObjectPool objectPool;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        objectPool.InitializeObjectPool(gameObject);
    }
}
