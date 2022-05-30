using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static GameObject soundGameObject;
    static AudioSource audioSource;
    public static void PlaySound(AudioClip sound)
    {
        if (soundGameObject == null)
        {
            soundGameObject = new GameObject("Sound");
            audioSource = soundGameObject.AddComponent<AudioSource>();
        }
        audioSource.PlayOneShot(sound);
    }
}
