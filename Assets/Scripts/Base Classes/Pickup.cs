using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float flySpeed = 5f;

    public abstract void ApplyEffectOnPlayer();
    public IEnumerator FlyToPlayer(Vector3 playerPos)
    {
        while (transform.position != playerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, flySpeed);
            yield return null;
        }
        ApplyEffectOnPlayer();
        gameObject.SetActive(false);
    }


}
