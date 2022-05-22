using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> levelSegments = new List<GameObject>();
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform despawnTransform;
    [SerializeField] float levelSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLevelSegment(levelSegments[0]));
    }

    IEnumerator SpawnLevelSegment(GameObject segmentToSpawn)
    {
        GameObject segmentInstance = Instantiate(segmentToSpawn, spawnTransform.position, Quaternion.identity);
        segmentToSpawn.SetActive(true);

        while (segmentInstance.transform.position != despawnTransform.position)
        {
            segmentInstance.transform.position = Vector3.MoveTowards(segmentInstance.transform.position, despawnTransform.position, levelSpeed);
            yield return null;
        }

        segmentInstance.SetActive(false);

    }

}
