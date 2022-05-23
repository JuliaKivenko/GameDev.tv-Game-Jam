using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupRange : MonoBehaviour
{
    [SerializeField] PlayerCollider playerCollider;

    private void LateUpdate()
    {
        transform.position = playerCollider.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Pickup>())
        {
            StartCoroutine(other.gameObject.GetComponent<Pickup>().FlyToPlayer(PlayerController.sharedInstance.transform.position));
        }
    }
}
