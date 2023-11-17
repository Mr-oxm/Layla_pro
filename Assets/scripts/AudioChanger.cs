using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    public int choice; // "bgMusic", "bgEffectsOne", "bgEffectsTwo", "layla"
    public int index; // Index of the audio clip

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();

            if (audioManager != null)
            {
                switch (choice)
                {
                    case 0:
                        audioManager.PlayBgMusic(index);
                        break;
                    case 1:
                        audioManager.PlayBgEffectsOne(index);
                        break;
                    case 2:
                        audioManager.PlayBgEffectsTwo(index);
                        break;
                    case 3:
                        audioManager.PlayLayla(index);
                        break;
                    default:
                        Debug.LogError("Invalid choice in AudioChanger");
                        break;
                }

                // Destroy this GameObject after changing the audio
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("AudioManager not found in the scene");
            }
        }
    }
}
