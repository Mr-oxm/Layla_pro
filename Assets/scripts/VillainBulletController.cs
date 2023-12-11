using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainBulletController : MonoBehaviour
{
    public float speed;
    private Transform player;

    public float bulletLifetime = 10f; // Lifetime of the bullet in seconds

    private Vector2 initialPlayerPosition;
    public float destroyDistance = 0.5f; // Distance threshold for destroying the bullet


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        initialPlayerPosition = player.position;

        // Adjust speed and scale based on the player's position
        if (player.position.x > transform.position.x)
        {
            speed = -speed;
            transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        

        // Start the destroy timer
        StartCoroutine(DestroyAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction towards the initial player position
        Vector2 direction = (initialPlayerPosition - (Vector2)transform.position).normalized;

        // Set the velocity to move towards the initial player position
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);

        // Check if the bullet is close enough to the initial position to destroy it
        
        if (Vector2.Distance(transform.position, initialPlayerPosition) < destroyDistance)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !FindObjectOfType<Player>().isInStealth())
        {
            // Handle player hit, e.g., reduce health
            Destroy(this.gameObject);
            FindObjectOfType<LevelManager>().RespawnPlayer();
        }
    }
    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(this.gameObject);
    }
}
