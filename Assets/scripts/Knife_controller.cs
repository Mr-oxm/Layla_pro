using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife_controller : MonoBehaviour
{
    // Reference to the SpriteRenderer component of the knife object
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component from the knife object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check if the player has the ShowKnife boolean and it's set to true
        if (FindObjectOfType<Player>().ShowKnife)
        {
            // Enable the sprite renderer if ShowKnife is true
            spriteRenderer.enabled = true;
        }
        else
        {
            // Disable the sprite renderer if ShowKnife is false
            spriteRenderer.enabled = false;
        }
    }
}
