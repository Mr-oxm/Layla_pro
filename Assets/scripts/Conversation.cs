using UnityEngine;

public class Conversation : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    private bool Playing = false;
    private bool isFinished = false;
    public float totalDuration;

    public bool InStelth=false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        CalculateTotalDuration();
    }

    private void CalculateTotalDuration()
    {
        totalDuration = 0f;
        foreach (AudioClip sound in sounds)
        {
            totalDuration += sound.length;
        }
    }

    private void PlaySoundsSequentially()
    {
        isFinished=true;
        StartCoroutine(PlaySoundsCoroutine());
    }
    private System.Collections.IEnumerator PlaySoundsCoroutine()
    {
        foreach (AudioClip sound in sounds)
        {
            audioSource.clip = sound;
            audioSource.Play();

            // Wait until the current sound has finished playing
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // All sounds have played, it is finished
        isFinished = true;
        FindObjectOfType<Player>().disableRunTo();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Playing = true;
            if (Playing && !isFinished && !InStelth)
                PlaySoundsSequentially();
            else if(Playing && !isFinished && InStelth && FindObjectOfType<Player>().isInStealth()){
                PlaySoundsSequentially();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player") && !isFinished)
        {
            audioSource.Stop();
        }
    }

}