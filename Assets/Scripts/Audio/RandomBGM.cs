using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGM : MonoBehaviour
{
    [SerializeField] List<AudioClip> bgmList = new List<AudioClip>();
    [SerializeField] AudioClip menuAudio;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        GameManager.onGameStart += OnGameStart;
        GameManager.onGameOver += OnGameOver;

        PlayMenuMusic();
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= OnGameStart;
        GameManager.onGameOver -= OnGameOver;
    }

    void OnGameStart()
    {
        audioSource.Stop();
        //pick a random track from the list and start playing, so long as it is not the same as the previous track
        if (!audioSource.playOnAwake)
        {
            audioSource.loop = false;
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        audioSource.clip = bgmList[Random.Range(0, bgmList.Count)];
        audioSource.Play();
        StartCoroutine(PlayNewTrack(audioSource.clip.length));
    }

    void OnGameOver()
    {
        StopAllCoroutines();
        PlayMenuMusic();
    }

    void PlayMenuMusic()
    {
        audioSource.Stop();
        audioSource.clip = menuAudio;
        audioSource.Play();
        audioSource.loop = true;
    }

    IEnumerator PlayNewTrack(float previousTracklength)
    {
        yield return new WaitForSeconds(previousTracklength);
        PlayRandomTrack();
    }

}
