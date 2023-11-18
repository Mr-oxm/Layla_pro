using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToTrigger : MonoBehaviour
{
    public GameObject targetObject;
    public AudioSource sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Check if the targetObject is assigned
            if (targetObject != null)
            {
                sound.Play();
                // Get the length of the audio clip
                float audioLength = sound.clip.length;
                // Get the position of the target GameObject
                Vector2 targetPosition = targetObject.transform.position;
                
                FindObjectOfType<Player>().stopPlayer();
                // Call the RunTo function in the player script
                FindObjectOfType<Player>().RunTo(targetPosition);

                // Destroy this trigger after the audio clip finishes playing
                Invoke("DestroyTrigger", audioLength);
            }
            else
            {
                Debug.LogWarning("Target object not assigned in the inspector.");
            }
        }
    }

    void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
