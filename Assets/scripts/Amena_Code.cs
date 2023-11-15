using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amena_Code : MonoBehaviour
{
    private Conversation conver;
    private bool hasBeenChecked = false;
    void Start()
    {
        conver = GetComponent<Conversation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player") && !hasBeenChecked)
        {
            FindObjectOfType<Player>().StartChecking(conver.totalDuration);
            hasBeenChecked = true;
        }
    }
}
