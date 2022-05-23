using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Damager>())
        {
            PlayerController.sharedInstance.GetHit(other.gameObject.GetComponent<Damager>());
        }
    }
}
