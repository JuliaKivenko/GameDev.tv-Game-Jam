using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        baseHealth *= healthGrowth;
        SetFullHealth();
    }
}
