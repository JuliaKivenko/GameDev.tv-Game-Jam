using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : Obstacle
{
    [SerializeField] float rotSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
    }
}
