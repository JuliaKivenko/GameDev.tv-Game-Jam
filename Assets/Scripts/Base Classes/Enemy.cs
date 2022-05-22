using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Damager, IReceiveDamage
{
    [SerializeField] float baseHealth;
    [SerializeField] int pointsToSpawn;
    [SerializeField] float pointsSpawnRadius = 3f;

    float currentHealth;



    public void InitializeEnemy()
    {
        currentHealth = baseHealth;
        //set spawn position
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FireballProjectile>())
        {
            ReceiveDamage(other.gameObject.GetComponent<FireballProjectile>().GetDamage());
        }
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
        gameObject.SetActive(false);

        //resets stats
        currentHealth = baseHealth;

        //spawns X points around itself
        //I want the points to do the touhou thing where they sorta fly out of the enemy instead of just spawning around
        for (int i = 0; i < pointsToSpawn; i++)
        {
            GameObject pointPickup = PickUpObjectPool.sharedInstance.objectPool.GetPooledObject();
            if (pointPickup != null)
            {
                pointPickup.transform.position = Random.insideUnitCircle * pointsSpawnRadius + new Vector2(transform.position.x, transform.position.y);
                pointPickup.transform.rotation = Quaternion.identity;
                pointPickup.SetActive(true);
            }
        }

    }


}
