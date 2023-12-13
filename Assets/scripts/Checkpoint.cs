using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        foreach (var p in FindObjectsOfType<Player>())
            p.disableAutoWalk();
            
        FindObjectOfType<LevelManager>().CurrentCheckpoint = gameObject;
    }
}
