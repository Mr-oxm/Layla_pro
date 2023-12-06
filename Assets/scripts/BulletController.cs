using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float bulletLifetime = 10f; // Lifetime of the bullet in seconds

    // Start is called before the first frame update
    void Start()
    {
        Player player;

        player = FindObjectOfType<Player>();
        
        if (player.transform.localScale.x < 0)
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            FindObjectOfType<Salim_lvl4>().getHit();
        }
    }
    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(this.gameObject);
    }
    
}
