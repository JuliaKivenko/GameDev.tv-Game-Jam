using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(MoveLevelSegment());
    }

    IEnumerator MoveLevelSegment()
    {
        while (transform.position != LevelManager.sharedInstance.despawnTransform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, LevelManager.sharedInstance.despawnTransform.position, LevelManager.sharedInstance.levelSpeed);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
