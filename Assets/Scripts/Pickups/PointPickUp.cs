using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickUp : Pickup
{
    [SerializeField] int pointValue = 1;


    public override void ApplyEffectOnPlayer()
    {
        GameManager.instance.AddPoints(pointValue);
    }
}
