using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjectPool : ObjectPool
{

    public static PickUpObjectPool sharedInstance;


    void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        InitializeObjectPool(gameObject);
    }
}
