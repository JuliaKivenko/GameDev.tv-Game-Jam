using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Damager
{
    [SerializeField] int pointsToSpawn;
    [SerializeField] float pointsSpawnRadius = 3f;
    [SerializeField] EnemyHealth health;
    [SerializeField] GameObject enemyHealthbar;
    [SerializeField] Image healthbarFill;
    [SerializeField] protected float _damage;
    public override float damage { get => _damage; set => _damage = value; }

    bool enableHealthBar = false;



    private void OnEnable()
    {
        enableHealthBar = false;
        GameManager.onGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= OnGameOver;
    }

    private void Update()
    {
        if (!enableHealthBar)
        {
            enemyHealthbar.SetActive(false);
        }
        if (enableHealthBar)
        {
            enemyHealthbar.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FireballProjectile>())
        {
            health.ReceiveDamage(PlayerController.instance.playerStats.damage);
            enableHealthBar = true;
        }
    }


    public void OnDied()
    {
        //resets stats
        health.SetFullHealth();
        enableHealthBar = false;
        healthbarFill.fillAmount = 1;
        enemyHealthbar.SetActive(false);

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
        health.ResetHealth();
        gameObject.SetActive(false);
    }


}
