using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager sharedInstance;

    [Header("Difficulty Increase")]
    [SerializeField] float levelDifficultyUpInterval;
    [SerializeField] float levelSpeed = 5f;
    [SerializeField] float maxLevelSpeed = 0.07f;
    [SerializeField] float speedIncrease = 1.2f;
    [SerializeField] float enemyHealthIncrease = 1.2f;

    [Header("Level Generation")]
    [SerializeField] List<LevelSegment> levelSegments = new List<LevelSegment>();
    [SerializeField] Transform spawnTransform;
    public Transform despawnTransform;


    List<LevelSegment> segmentInstances = new List<LevelSegment>();
    Transform lastSegmentEnd;
    int lastId = 99;
    float timer;
    float startLevelSpeed;

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

        startLevelSpeed = levelSpeed;

    }

    private void Update()
    {
        if (!GameManager.sharedInstance.isGameRunning)
        {
            return;
        }

        //if player's distance from segment end is less than defined in the variable, spawn next segment
        if (Vector3.Distance(PlayerController.sharedInstance.transform.position, lastSegmentEnd.position) < DISTANCE_TO_SPAWN_FROM_PLAYER)
        {
            SpawnNextSegment();
        }

        //timer for difficulty increase
        timer += Time.deltaTime;

        //increase level speed and all the enemies health once it is time to increase difficulty
        if (timer >= levelDifficultyUpInterval)
        {
            foreach (GameObject enemyGameObject in EnemyObjectPool.sharedInstance.objectPool.pooledObjects)
            {
                enemyGameObject.GetComponent<EnemyHealth>().IncreaseHealth(enemyHealthIncrease);
            }

            if (levelSpeed < maxLevelSpeed)
            {
                levelSpeed *= speedIncrease;
                levelSpeed = Mathf.Clamp(levelSpeed, 0.03f, maxLevelSpeed);
            }

            timer = 0;
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
        segmentInstances[id].ActivateEnemies();
        lastSegmentEnd = segmentInstances[id].endPosition;
        lastId = id;
    }

    public float GetLevelSpeed()
    {
        return levelSpeed;
    }

    public void StartLevelGeneration()
    {
        gameObject.SetActive(true);
        timer = 0;
        lastSegmentEnd = spawnTransform;
        SpawnNextSegment();
    }

    public void StopLevelGeneration()
    {
        foreach (LevelSegment levelSegment in segmentInstances)
        {
            ScrollingObject scrollingObject = GetComponent<ScrollingObject>();
            scrollingObject.StopScrolling();
        }
        gameObject.SetActive(false);
    }

    public void ResetDifficulty()
    {
        levelSpeed = startLevelSpeed;
        foreach (GameObject enemyGameObject in EnemyObjectPool.sharedInstance.objectPool.pooledObjects)
        {
            enemyGameObject.GetComponent<EnemyHealth>().ResetHealth();
        }

    }



}
