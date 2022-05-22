using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Damager
{
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Collider projectileCollider;

    private void Update()
    {
        if (ICanSee())
        {
            gameObject.transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }


    private bool ICanSee()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, projectileCollider.bounds);
    }

}
