using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestruction : MonoBehaviour
{
    public float duration = 3f;

    private Animator anim;
    [SerializeField] private bool Shaking = false;
    private BoxCollider2D bc;
    private SpriteRenderer sp;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bc=GetComponent<BoxCollider2D>();
        sp= GetComponent<SpriteRenderer>();
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
            StartCoroutine(CrumblePlatformWithDelay());
        }
    }

    IEnumerator CrumblePlatformWithDelay()
    {
        yield return new WaitForSeconds(duration);

        if(Shaking){
            sp.enabled = false;
            bc.enabled = false;
        }
    }

    public void RespawnPlatform()
    {
        Shaking = false;
        sp.enabled = true;
        bc.enabled = true;
    }

}
