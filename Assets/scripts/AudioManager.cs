using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusicSource;
    public AudioSource bgEffectsOneSource;
    public AudioSource bgEffectsTwoSource;
    public AudioSource bgEffectsThreeSource;
    public AudioSource laylaSource;

    // New Audio Sources
    public AudioSource AzizSource;
    public AudioSource SalimSource;
    public AudioSource DadSource;
    public AudioSource MomSource;

    public AudioClip[] bgMusic;
    public AudioClip[] bgEffectsOne;
    public AudioClip[] bgEffectsTwo;
    public AudioClip[] bgEffectsThree;
    public AudioClip[] layla;

    // New AudioClip arrays
    public AudioClip[] Aziz;
    public AudioClip[] Salim;
    public AudioClip[] Dad;
    public AudioClip[] Mom;

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

    // Function to play background effects three
    public void PlayBgEffectsThree(int index)
    {
        SetAudioClipAndPlay(bgEffectsThreeSource, bgEffectsThree, index);
    }

    // Function to play Aziz
    public void PlayAziz(int index)
    {
        SetAudioClipAndPlay(AzizSource, Aziz, index);
    }

    // Function to play Salim
    public void PlaySalim(int index)
    {
        SetAudioClipAndPlay(SalimSource, Salim, index);
    }

    // Function to play Dad
    public void PlayDad(int index)
    {
        SetAudioClipAndPlay(DadSource, Dad, index);
    }

    // Function to play Mom
    public void PlayMom(int index)
    {
        SetAudioClipAndPlay(MomSource, Mom, index);
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
