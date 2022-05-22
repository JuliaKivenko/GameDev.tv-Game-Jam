using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager sharedInstance;
    public Transform despawnTransform;
    public float levelSpeed = 5f;

    [SerializeField] List<LevelSegment> levelSegments = new List<LevelSegment>();
    [SerializeField] Transform spawnTransform;


    List<LevelSegment> segmentInstances = new List<LevelSegment>();

    Transform lastSegmentEnd;
    int lastId = 99;

    private const float DISTANCE_TO_SPAWN_FROM_PLAYER = 30f;

    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {

        for (int i = 0; i < levelSegments.Count; i++)
        {
            GameObject segmentInstance = Instantiate(levelSegments[i].gameObject, spawnTransform.position, Quaternion.identity);
            segmentInstance.SetActive(false);
            segmentInstances.Add(segmentInstance.GetComponent<LevelSegment>());
        }

        lastSegmentEnd = spawnTransform;
        SpawnNextSegment();

    }

    private void Update()
    {
        if (Vector3.Distance(PlayerController.sharedInstance.transform.position, lastSegmentEnd.position) < DISTANCE_TO_SPAWN_FROM_PLAYER)
        {
            SpawnNextSegment();
        }
    }

    void SpawnNextSegment()
    {
        int id = Random.Range(0, segmentInstances.Count);
        while (id == lastId)
        {
            id = Random.Range(0, segmentInstances.Count);
        }
        segmentInstances[id].gameObject.transform.position = lastSegmentEnd.position;
        segmentInstances[id].gameObject.SetActive(true);
        lastSegmentEnd = segmentInstances[id].endPosition;
        lastId = id;
    }

}
