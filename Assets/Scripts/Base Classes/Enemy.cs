using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Damager
{
    [SerializeField] int pointsToSpawn;
    [SerializeField] float pointsSpawnRadius = 3f;
    [SerializeField] EnemyHealth health;


    private void OnEnable()
    {
        health.SetFullHealth();
        GameManager.onGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= OnGameOver;
    }
    public void InitializeEnemy()
    {
        health.SetFullHealth();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FireballProjectile>())
        {
            health.ReceiveDamage(other.gameObject.GetComponent<FireballProjectile>().GetDamage());
        }
    }

    public void OnDied()
    {
        //disables the object
        gameObject.SetActive(false);

        //resets stats
        health.SetFullHealth();

        //spawns X points around itself
        //I want the points to do the touhou thing where they sorta fly out of the enemy instead of just spawning around
        for (int i = 0; i < pointsToSpawn; i++)
        {
            GameObject pointPickup = PickUpObjectPool.sharedInstance.GetPooledObject();
            if (pointPickup != null)
            {
                pointPickup.transform.position = Random.insideUnitCircle * pointsSpawnRadius + new Vector2(transform.position.x, transform.position.y);
                pointPickup.transform.rotation = Quaternion.identity;
                pointPickup.SetActive(true);
            }
        }

    }

    void OnGameOver()
    {
        gameObject.SetActive(false);
        health.ResetHealth();
    }


}
