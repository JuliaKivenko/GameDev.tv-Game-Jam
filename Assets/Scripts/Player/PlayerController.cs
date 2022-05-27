using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] float rechargeTime;
    [SerializeField] float rechargeSpeed;
    [SerializeField] float flyForceMultiplier;
    [SerializeField] float invulnerabilityLength;
    public PlayerHealth health;

    [Header("Projectile")]
    [SerializeField] Transform fireballSocket;
    [SerializeField] GameObject fireball;

    Vector3 flyDirection = Vector3.up;
    Rigidbody playerRigidbody;
    float currentRechargeTime;
    float move = 0;
    bool invulnerabilityActive = false;
    bool enableControls = true;
    public Vector3 startPosition;

    public PlayerStats playerStats;

    public static PlayerController sharedInstance;


    private void OnEnable()
    {
        GameManager.onGameOver += DeactivatePlayer;
    }

    private void OnDisable()
    {
        GameManager.onGameOver += DeactivatePlayer;
    }

    void Start()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        currentRechargeTime = rechargeTime;
        health.SetFullHealth();
        startPosition = transform.position;

        DeactivatePlayer();

        sharedInstance = this;

    }


    void Update()
    {
        if (enableControls)
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

        GameObject fireballProjectile = FireballObjectPool.sharedInstance.GetPooledObject();
        if (fireballProjectile != null)
        {
            fireballProjectile.transform.position = fireballSocket.position;
            fireballProjectile.transform.rotation = Quaternion.identity;
            fireballProjectile.SetActive(true);
        }

        StartCoroutine(RechargeFireball());
    }

    public void GetHit(Damager damager)
    {
        if (invulnerabilityActive)
        {
            return;
        }
        health.ReceiveDamage(damager.GetDamage());
        StartCoroutine(InvulnerabilityFrames());
    }


    public void OnDie()
    {
        GameManager.sharedInstance.GameOver();
    }

    public Vector3 GetVelocity()
    {
        return playerRigidbody.velocity;
    }

    void DeactivatePlayer()
    {
        enableControls = false;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.transform.position = startPosition;
        playerRigidbody.isKinematic = true;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void ActivatePlayerCharacter()
    {
        enableControls = true;
        playerRigidbody.isKinematic = false;
        currentRechargeTime = rechargeTime;
        health.SetFullHealth();
        gameObject.SetActive(true);
    }

    public float GetFireballRechargeProgress()
    {
        return currentRechargeTime / rechargeTime;
    }


    //Recharge Fireball
    IEnumerator RechargeFireball()
    {
        while (currentRechargeTime < rechargeTime)
        {
            currentRechargeTime += rechargeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentRechargeTime = rechargeTime;
    }

    public IEnumerator InvulnerabilityFrames()
    {
        invulnerabilityActive = true;
        yield return new WaitForSeconds(invulnerabilityLength);
        invulnerabilityActive = false;
    }


}
