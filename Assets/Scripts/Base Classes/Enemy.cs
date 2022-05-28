using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Damager
{
    [SerializeField] int pointsToSpawn;
    [SerializeField] float pointsSpawnRadius = 3f;
    [SerializeField] EnemyHealth health;
    [SerializeField] GameObject enemyHealthBar;



    private void OnEnable()
    {
        health.SetFullHealth();
        enemyHealthBar.SetActive(false);
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
            if (health.currentHealth != 0)
                enemyHealthBar.SetActive(true);
        }
    }


    public void OnDied()
    {
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

        //disables the object
        gameObject.SetActive(false);

    }

    void OnGameOver()
    {
        gameObject.SetActive(false);
        health.ResetHealth();
    }


}
