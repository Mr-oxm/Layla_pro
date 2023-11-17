using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusicSource;
    public AudioSource bgEffectsOneSource;
    public AudioSource bgEffectsTwoSource;
    public AudioSource laylaSource;

    public AudioClip[] bgMusic;
    public AudioClip[] bgEffectsOne;
    public AudioClip[] bgEffectsTwo;
    public AudioClip[] layla;

    // Function to play background music
    public void PlayBgMusic(int index)
    {
        SetAudioClipAndPlay(bgMusicSource, bgMusic, index);
    }

    // Function to play background effects one
    public void PlayBgEffectsOne(int index)
    {
        SetAudioClipAndPlay(bgEffectsOneSource, bgEffectsOne, index);
    }

    // Function to play background effects two
    public void PlayBgEffectsTwo(int index)
    {
        SetAudioClipAndPlay(bgEffectsTwoSource, bgEffectsTwo, index);
    }

    // Function to play Layla
    public void PlayLayla(int index)
    {
        SetAudioClipAndPlay(laylaSource, layla, index);
    }

    // Set audio clip of AudioSource and play
    private void SetAudioClipAndPlay(AudioSource audioSource, AudioClip[] audioClips, int index)
    {
        if (audioSource != null && audioClips != null && index >= 0 && index < audioClips.Length)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid AudioSource or AudioClip array or index");
        }
    }
}
