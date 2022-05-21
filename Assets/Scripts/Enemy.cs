using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damager, IReceiveDamage
{
    [SerializeField] float baseHealth;
    float currentHealth;

    void InitializeEnemy()
    {
        currentHealth = baseHealth;
        //set spawn position
    }

    public void ReceiveDamage(float damage)
    {
        //take damage
        currentHealth -= damage;

        //If enemy lost all health it should die.
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        //disables the object

        //resets stats
        currentHealth = baseHealth;

        //spawns X points around itself

    }


}
