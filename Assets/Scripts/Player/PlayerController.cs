using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Vector3 startPosition;
    public PlayerStats playerStats;
    public float fireballRechargeProgress;

    [Header("Base Stats")]
    [SerializeField] float rechargeTime;
    [SerializeField] float rechargeSpeed;
    [SerializeField] float flyForceMultiplier;
    [SerializeField] float invulnerabilityLength;
    public PlayerHealth health;

    [Header("Projectile")]
    [SerializeField] Transform fireballSocket;
    [SerializeField] GameObject fireball;
    [SerializeField] AudioClip fireballSFX;

    Rigidbody playerRigidbody;
    float currentRechargeTime;
    bool invulnerabilityActive = false;
    float move = 0;

    private void OnEnable() => GameManager.onGameOver += DisablePlayerCharacter;

    private void OnDisable() => GameManager.onGameOver -= DisablePlayerCharacter;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        currentRechargeTime = rechargeTime;
        health.SetFullHealth();
        startPosition = transform.position;

        DisablePlayerCharacter();
    }

    void Update()
    {
        //Disabled for mobile
        //if (enableControls)
        //ManageInput();
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(Vector3.up * flyForceMultiplier * move * Time.deltaTime);
    }

    //Disabled for mobile
    /*void ManagePCInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerRigidbody.velocity = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
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
    }*/

    public void ManageMobileInput(int value) => move = value;

    public void ResetVelocity() => playerRigidbody.velocity = Vector3.zero;

    public void Fire()
    {
        //Check for recharge
        if (currentRechargeTime != rechargeTime) { return; }

        //If fully charged, shoot the fireball and set to recharge
        currentRechargeTime = 0;

        SoundManager.PlaySound(fireballSFX);

        GameObject fireballProjectile = FireballObjectPool.sharedInstance.GetPooledObject();
        if (fireballProjectile != null)
        {
            fireballProjectile.transform.position = fireballSocket.position;
            fireballProjectile.transform.rotation = Quaternion.identity;
            fireballProjectile.SetActive(true);
        }

        StartCoroutine(RechargeFireball());
    }

    IEnumerator RechargeFireball()
    {
        while (currentRechargeTime < rechargeTime)
        {
            currentRechargeTime += rechargeSpeed * Time.deltaTime;
            fireballRechargeProgress = currentRechargeTime / rechargeTime;
            yield return new WaitForEndOfFrame();
        }
        currentRechargeTime = rechargeTime;
        fireballRechargeProgress = 1;
    }

    public void GetHit(Damager damager)
    {
        if (invulnerabilityActive)
        {
            return;
        }
        health.ReceiveDamage(damager.damage);
        StartCoroutine(InvulnerabilityFrames());
    }

    IEnumerator InvulnerabilityFrames()
    {
        invulnerabilityActive = true;
        yield return new WaitForSeconds(invulnerabilityLength);
        invulnerabilityActive = false;
    }

    public Vector3 GetVelocity()
    {
        return playerRigidbody.velocity;
    }

    void DisablePlayerCharacter()
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.transform.position = startPosition;
        playerRigidbody.isKinematic = true;
        move = 0;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void ActivatePlayerCharacter()
    {
        playerRigidbody.isKinematic = false;
        currentRechargeTime = rechargeTime;
        health.SetFullHealth();
        gameObject.SetActive(true);
    }
}
