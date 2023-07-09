using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    [SerializeField] private AudioClip hopAudioClip, waterAudioClip, sunlightAudioClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayHopAudio()
    {
        audioSource.PlayOneShot(hopAudioClip);
    }
    
    public void PlayCollectSunlightAudio()
    {
        audioSource.PlayOneShot(sunlightAudioClip);
    }
    
    public void PlayCollectWaterAudio()
    {
        audioSource.PlayOneShot(waterAudioClip);
    }
}
