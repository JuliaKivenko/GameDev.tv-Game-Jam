using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IReceiveDamage
{
    [Header("Base Stats")]
    [SerializeField] float baseHealth;
    [SerializeField] float rechargeTime;
    [SerializeField] float rechargeSpeed;
    [SerializeField] float flyForceMultiplier;
    [SerializeField] float invulnerabilityLength;

    [Header("Projectile")]
    [SerializeField] Transform fireballSocket;
    [SerializeField] GameObject fireball;

    Vector3 flyDirection = Vector3.up;
    Rigidbody playerRigidbody;
    float currentRechargeTime;
    float currentHealth;
    float move = 0;
    bool invulnerabilityActive = false;

    public static PlayerController sharedInstance;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        currentRechargeTime = rechargeTime;
        currentHealth = baseHealth;

        sharedInstance = this;
    }


    void Update()
    {
        ManageInput();
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(flyDirection * flyForceMultiplier * move);
    }

    void ManageInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerRigidbody.velocity = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Smoothly fly up up to a certain limit
            move = 1;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Player slowly falls to the bottom of the screen
            playerRigidbody.velocity = Vector3.zero;
            move = 0;
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
            fireballProjectile.transform.rotation = Quaternion.identity;
            fireballProjectile.SetActive(true);
        }

        StartCoroutine(IRechargeFireball());
    }

    public void ReceiveDamage(float damage)
    {
        if (invulnerabilityActive)
        {
            return;
        }

        //take damage
        currentHealth -= damage;
        Debug.Log("Current health is " + currentHealth);

        //Small invulnerability window so that player does not get insta-killed
        StartCoroutine(IInvulnerabilityFrames());

        //If no health left - game over
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        //GameOver
        Debug.Log("Game Over!");
    }

    public Vector3 GetVelocity()
    {
        return playerRigidbody.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Damager>())
        {
            ReceiveDamage(other.gameObject.GetComponent<Damager>().GetDamage());
        }
    }

    //Recharge Fireball
    IEnumerator IRechargeFireball()
    {
        while (currentRechargeTime < rechargeTime)
        {
            currentRechargeTime += rechargeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentRechargeTime = rechargeTime;
    }

    IEnumerator IInvulnerabilityFrames()
    {
        invulnerabilityActive = true;
        yield return new WaitForSeconds(invulnerabilityLength);
        invulnerabilityActive = false;
    }


}
