using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float damage;

    public void ModifyPlayerStat(statTypes playerStat, float multiplier)
    {
        if (playerStat == statTypes.Health)
        {
            health *= multiplier;
        }
        if (playerStat == statTypes.Damage)
        {
            damage *= multiplier;
        }

    }
}
