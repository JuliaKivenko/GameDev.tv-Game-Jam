using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupVisualRotator : MonoBehaviour
{
    float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
