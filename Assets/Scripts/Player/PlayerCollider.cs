using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] AudioSource getHitSfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Damager>())
        {
            getHitSfx.Play();
            PlayerController.instance.GetHit(other.gameObject.GetComponent<Damager>());
        }
    }
}
