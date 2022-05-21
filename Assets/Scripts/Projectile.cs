using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damager
{
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected Rigidbody rb;

    private void Update()
    {
        gameObject.transform.Translate(direction * speed * Time.deltaTime);
    }

}
