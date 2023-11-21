using System.Collections.Generic;
using UnityEngine;

public class Salim : MonoBehaviour
{

    //Speed of movement
    public float speed;
    //Normal Speed of movement
    public float NormalSpeed ;
    //Running Speed of movement
    public float runningSpeed;

    private bool isFacingRight = true;
    
    // private bool isDead=false;

    private Animator anim;

    public Transform enemyVision; // Add a reference to the EnemyVision point

    private bool autoWalk= false;

    public bool isHitting = false;
    public bool hasHit = false;
    public float hittingDuration=0.5f;
    

    //knife


    void Start()
    {
        anim = GetComponent<Animator>();
        speed=NormalSpeed;
    }


    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        
        if(autoWalk){
            if (DetectPlayer()&& !hasHit)
            {
                autoWalk=false;
                flip();
                isFacingRight=false;
                speed=0;
            }
            else
            {
                Walk();
            }
        }

    }

    bool DetectPlayer()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        float distanceToEnemyVision = Vector2.Distance(transform.position, enemyVision.position);

        // Check if player is in between enemy and enemyVision point
        if (distanceToPlayer< distanceToEnemyVision )
        {
            return true;
        }
        return false;
    }


    void Walk()
    {
        if (this.isFacingRight == true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-(speed), this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void flip()
    {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
        }
    }

    public void enableAutoWalk(){
        autoWalk=true;
    }

    //For conversation to take place
    public void stopPlayerDelay(float duration){
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        Invoke("stopPlayer", duration);
    }

    //To hit Layla
    public void stopPlayer()
    {
        enableHitting();
        Invoke("disableHitting", hittingDuration);
    }

    void enableHitting(){
        isHitting=true;
        anim.SetBool("hit", isHitting);
    }
    void disableHitting(){
        isHitting=false;
        anim.SetBool("hit", isHitting);
        FindObjectOfType<Player>().getStapped();
        hasHit=true;
    }
}