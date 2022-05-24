using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScrollingObject))]
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float flySpeed = 5f;

    ScrollingObject scrollingObject;

    private void Start()
    {
        scrollingObject = GetComponent<ScrollingObject>();

    }

    public abstract void ApplyEffectOnPlayer();
    public IEnumerator FlyToPlayer(Vector3 playerPos)
    {
        scrollingObject.StopScrolling();
        while (transform.position != playerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, flySpeed);
            yield return null;
        }
        ApplyEffectOnPlayer();
        gameObject.SetActive(false);
    }

    void onGameOver()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.onGameOver += onGameOver;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= onGameOver;
    }


}
