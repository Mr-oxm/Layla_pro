using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestruction : MonoBehaviour
{
    public float duration = 3f;

    private Animator anim;
    private bool Shaking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        anim.SetBool("Shake", Shaking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Player"
        if (collision.collider.CompareTag("Player"))
        {
            Shaking = true;
            anim.SetBool("Shake", Shaking);
            Invoke("CrumblePlatform", duration);
        }
    }

    private void CrumblePlatform()
    {
        // Disable the SpriteRenderer and BoxCollider
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

    }

    public void RespawnPlatform()
    {
        Shaking = false;
        // Enable the SpriteRenderer and BoxCollider
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

}
