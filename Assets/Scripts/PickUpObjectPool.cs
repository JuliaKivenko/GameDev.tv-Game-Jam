using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjectPool : MonoBehaviour
{

    public static PickUpObjectPool sharedInstance;
    public ObjectPool objectPool;


    void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        objectPool.InitializeObjectPool(gameObject);
    }
}
