using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Pickup>())
        {
            StartCoroutine(other.gameObject.GetComponent<Pickup>().FlyToPlayer(PlayerController.sharedInstance.transform.position));
        }
    }
}
