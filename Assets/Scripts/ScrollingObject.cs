using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    Coroutine scrollCoroutine;

    private void OnEnable()
    {
        scrollCoroutine = StartCoroutine(ScrollObject());
        GameManager.onGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= OnGameOver;
        StopScrolling();
    }

    public void StopScrolling()
    {
        StopCoroutine(scrollCoroutine);
    }

    IEnumerator ScrollObject()
    {
        Vector3 targetPosition = new Vector3(LevelManager.instance.despawnTransform.position.x, transform.position.y, transform.position.z);
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, LevelManager.instance.GetLevelSpeed() * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    void OnGameOver()
    {
        StopScrolling();
    }
}
