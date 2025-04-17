using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource soundEffect;

    public void PlayEffectAudio(AudioClip clip, float pitch = 1f)
    {
        // Optional: stop the current clip if needed
        soundEffect.Stop();

        // Swap the clip and play
        soundEffect.clip = clip;
        soundEffect.pitch = pitch;
        soundEffect.Play();
    }
}
