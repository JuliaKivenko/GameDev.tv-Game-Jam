using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    float startHealth;

    private void Start()
    {
        startHealth = baseHealth;
    }

    public void ResetHealth()
    {
        baseHealth = startHealth;
        currentHealth = startHealth;
    }

    public override void ModifyHealth(float healthGrowth)
    {
        if (hasFullhealth())
        {
            baseHealth *= healthGrowth;
            SetFullHealth();
        }
    }
}
