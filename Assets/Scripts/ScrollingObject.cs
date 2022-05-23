using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    Coroutine scrollCoroutine;
    private void OnEnable()
    {
        scrollCoroutine = StartCoroutine(ScrollObject());
    }

    private void OnDisable()
    {
        StopScrolling();
    }

    public void StopScrolling()
    {
        StopCoroutine(scrollCoroutine);
    }

    IEnumerator ScrollObject()
    {
        while (transform.position != LevelManager.sharedInstance.despawnTransform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, LevelManager.sharedInstance.despawnTransform.position, LevelManager.sharedInstance.GetLevelSpeed());
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
