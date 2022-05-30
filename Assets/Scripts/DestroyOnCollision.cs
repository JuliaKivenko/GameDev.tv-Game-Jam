using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField] AudioClip soundOnCollision;
    private void OnCollisionEnter(Collision other)
    {
        SoundManager.PlaySound(soundOnCollision);
        gameObject.SetActive(false);
    }
}
