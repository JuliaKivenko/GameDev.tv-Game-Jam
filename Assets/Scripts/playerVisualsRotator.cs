using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVisualsRotator : MonoBehaviour
{
    [SerializeField] float rotateStrength = 50f;

    float ROTATION_MIN = -45f;
    float ROTATION_MAX = 45f;



    void Update()
    {

        if (PlayerController.sharedInstance.GetNormalizedVelocity().y >= 0)
        {
            transform.Rotate(Vector3.forward * rotateStrength * 5 * Time.deltaTime * PlayerController.sharedInstance.GetNormalizedVelocity().y);

        }
        else
        {
            transform.Rotate(Vector3.forward * rotateStrength * 2 * Time.deltaTime * PlayerController.sharedInstance.GetNormalizedVelocity().y);
        }

        float currentZRotation = transform.eulerAngles.z;
        //currentZRotation = (currentZRotation > 180) ? currentZRotation - 360 : currentZRotation;
        currentZRotation = Mathf.Clamp(currentZRotation, ROTATION_MIN, ROTATION_MAX);
        transform.rotation = Quaternion.Euler(0, 0, currentZRotation);


    }
    private void LateUpdate()
    {

    }
}
