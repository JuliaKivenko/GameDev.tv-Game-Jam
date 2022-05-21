using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IReceiveDamage
{
    [SerializeField] float baseHealth;
    [SerializeField] float flyForceMultiplier;
    [SerializeField] Vector3 flyDirection;
    [SerializeField] Transform fireballSocket;
    [SerializeField] GameObject fireball;
    [SerializeField] float rechargeTime;

    Rigidbody playerRigidbody;
    float currentRechargeTime;
    float currentHealth;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        currentRechargeTime = rechargeTime;
    }


    void Update()
    {
        ManageInput();
    }

    void ManageInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Smoothly fly up up to a certain limit
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(flyDirection * flyForceMultiplier);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Player slowly falls to the bottom of the screen
            playerRigidbody.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Fire projectile
            Fire();
        }
    }

    void Fire()
    {
        //Check for recharge
        if (currentRechargeTime != rechargeTime)
        {
            return;
        }

        //If fully charged, shoot the fireball and set to recharge
        currentRechargeTime = 0;

        GameObject fireballProjectile = FireballObjectPool.sharedInstance.objectPool.GetPooledObject();
        if (fireballProjectile != null)
        {
            fireballProjectile.transform.position = fireballSocket.position;
            fireballProjectile.transform.rotation = fireballSocket.rotation;
            fireballProjectile.SetActive(true);
        }
    }

    public void ReceiveDamage(float damage)
    {
        //take damage
        currentHealth -= damage;

        //If no health left - game over
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        //GameOver

    }
}
