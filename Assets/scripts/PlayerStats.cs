using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 6;
    public int health;
    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;
    public int damage = 20;

    private SpriteRenderer spriteRenderer;
    private List<SpriteRenderer> childSpriteRenderers = new List<SpriteRenderer>();

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        health = maxHealth;

        // Get SpriteRenderers of all child objects
        GetChildSpriteRenderers();

        if (FindObjectOfType<HealthBar>() != null)
            FindObjectOfType<HealthBar>().SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isImmune == true)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;
            if (this.immunityTime >= this.immunityDuration)
            {
                this.isImmune = false;
                this.spriteRenderer.enabled = true;

                // Reset flicker for child objects
                foreach (var childRenderer in childSpriteRenderers)
                {
                    childRenderer.enabled = true;
                }
            }
        }
    }

    void SpriteFlicker()
    {
        if (this.flickerTime < this.flickerDuration)
        {
            this.flickerTime += Time.deltaTime;
        }
        else if (this.flickerTime >= this.flickerDuration)
        {
            // Flicker for the main object
            spriteRenderer.enabled = !spriteRenderer.enabled;

            // Flicker for child objects
            foreach (var childRenderer in childSpriteRenderers){
                 if (childRenderer.gameObject.activeSelf) // Check if the child object is active
                {
                    childRenderer.enabled = !childRenderer.enabled;
                }
            }

            this.flickerTime = 0;
        }
    }

    void PlayHitReaction()
    {
        this.isImmune = true;
        this.immunityTime = 0f;
    }

    public void TakeDamage()
    {
        if (this.isImmune == false)
        {
            this.health -= damage;
            FindObjectOfType<HealthBar>().SetHealth(health);

            if (this.health < 0)
                this.health = 0;

            if (this.health == 0)
            {
                FindObjectOfType<HealthBar>().SetMaxHealth(maxHealth);
                FindObjectOfType<LevelManager>().RespawnPlayer();
                this.health = maxHealth;
            }
        }

        PlayHitReaction();
    }

    void GetChildSpriteRenderers()
    {
        // Get SpriteRenderers of all child objects
        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var childRenderer in childRenderers)
        {
            if (childRenderer != spriteRenderer)
            {
                childSpriteRenderers.Add(childRenderer);
            }
        }
    }
}
