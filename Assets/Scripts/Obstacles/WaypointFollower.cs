using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script has been retreived from GameDev.tv game jam starting kit! Thank you!
public class WaypointFollower : MonoBehaviour
{
    [SerializeField] Transform pathContainer;
    [SerializeField] bool isCircularPath = true;
    [SerializeField] float moveSpeed;
    [SerializeField] float targetDistanceThreshold = 0.1f;
    [SerializeField] float waitTime;

    [SerializeField] List<Transform> path = new List<Transform>();
    Transform target;
    bool isCyclingForward = true;
    int nextWaypointIndex = 0;
    float waitTimeElapsed;

    void Awake()
    {
        SetUpPath();
    }

    void Update()
    {
        MoveToNextWaypoint();
    }

    void SetUpPath()
    {
        //Warn if no path has been given and exit
        if (pathContainer == null || pathContainer.childCount == 0)
        {
            Debug.LogWarningFormat("Warning: No waypoint path found for {0}", gameObject.name);
            return;
        }
        //Add waypoints to the path 
        foreach (Transform waypoint in pathContainer)
        {
            path.Add(waypoint);
        }

        //Start at the first waypoint position
        transform.position = path[0].position;

        //Set first waypoint target
        target = path[0];
    }

    void MoveToNextWaypoint()
    {
        if (path == null) { return; }
        if (target == null) { return; }

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        //if the object is not close enough to the next waypoint
        if (distanceToTarget > targetDistanceThreshold)
        {
            //move the object towards the waypoint
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            //wait before moving to the next waypoint
            if (waitTimeElapsed < waitTime)
            {
                waitTimeElapsed += Time.deltaTime;
            }
            else
            {
                ChangeTarget();
                waitTimeElapsed = 0;
            }
        }
    }

    void ChangeTarget()
    {
        //If the path is circular, link the first and last waypoints.
        if (isCyclingForward)
        {
            nextWaypointIndex++;
            if (nextWaypointIndex >= path.Count)
            {
                if (isCircularPath)
                {
                    nextWaypointIndex = 0;
                }
                else
                {
                    nextWaypointIndex -= 2;
                    isCyclingForward = false;
                }
            }
        }
        //otherwise, reverse the direction of the path
        else
        {
            nextWaypointIndex--;
            if (nextWaypointIndex < 0)
            {
                if (isCircularPath)
                {
                    nextWaypointIndex = path.Count - 1;
                }
                else
                {
                    nextWaypointIndex += 2;
                    isCyclingForward = true;
                }
            }
        }
        target = path[nextWaypointIndex];
    }

    //Draws the path connecting the waypoints in Scene view (make sure you enable gizmos if you can't see the lines)
    void OnDrawGizmos()
    {
        if (path.Count <= 0) { return; }

        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i].position, path[i + 1].position, Color.blue);
        }
        if (isCircularPath)
        {
            Debug.DrawLine(path[path.Count - 1].position, path[0].position, Color.blue);
        }
    }
}