using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damager : MonoBehaviour
{
    [SerializeField] protected float damage;

    public float GetDamage()
    {
        float damageValue = damage;
        return damageValue;
    }

}
