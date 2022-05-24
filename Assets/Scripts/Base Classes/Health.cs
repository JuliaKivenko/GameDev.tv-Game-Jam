using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    public UnityEvent OnDied;
    public float baseHealth;
    public float currentHealth;


    public void SetFullHealth()
    {
        currentHealth = baseHealth;
    }

    public void ReceiveDamage(float damage)
    {
        //take damage
        currentHealth -= damage;

        //If lost all health,  die.
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public bool hasFullhealth()
    {
        return currentHealth == baseHealth;
    }

    void Die()
    {
        if (OnDied != null)
            OnDied.Invoke();
    }
}
