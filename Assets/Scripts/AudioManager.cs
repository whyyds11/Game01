using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    private AudioSource audio;
    public AudioClip collectibleAudioClip;

    void Awake()
    {
        _instance = this;
        audio = this.GetComponent<AudioSource>();
    }
    public void PlayCollectible()
    {
        audio.PlayOneShot(collectibleAudioClip);
    }
}
