using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public void ModifyPlayerStat(statTypes playerStat, float multiplier)
    {
        if (playerStat == statTypes.Health)
        {
            PlayerController.sharedInstance.health.ModifyHealth(multiplier);
        }
        if (playerStat == statTypes.Damage)
        {
            foreach (GameObject fireBall in FireballObjectPool.sharedInstance.pooledObjects)
            {
                fireBall.GetComponent<FireballProjectile>().ModifyDamage(multiplier);
            }
        }

    }
}
