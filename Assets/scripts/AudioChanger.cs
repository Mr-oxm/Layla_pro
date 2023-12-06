using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    public int choice; // "bgMusic", "bgEffectsOne", "bgEffectsTwo", "layla", "bgEffectsThree", "Aziz", "Salim", "Dad", "Mom"
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
                        audioManager.PlayBgEffectsThree(index);
                        break;
                    // New cases for additional categories
                    case 4:
                        audioManager.PlayLayla(index);
                        break;
                    case 5:
                        audioManager.PlayAziz(index);
                        break;
                    case 6:
                        audioManager.PlaySalim(index);
                        break;
                    case 7:
                        audioManager.PlayDad(index);
                        break;
                    case 8:
                        audioManager.PlayMom(index);
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
