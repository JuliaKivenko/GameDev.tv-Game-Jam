using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVisualsRotator : MonoBehaviour
{
    [SerializeField] float rotateStrength = 25f;

    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, PlayerController.instance.GetVelocity().y * rotateStrength);
    }

}
