using System.Collections.Generic;
using UnityEngine;

public class Salim_lvl4 : MonoBehaviour
{

    //Speed of movement
    public float speed;
    //Normal Speed of movement
    public float NormalSpeed ;
    //Running Speed of movement
    public float runningSpeed;

    [SerializeField] private bool isFacingRight = true;
    
    // private bool isDead=false;

    private Animator anim;

    public Transform enemyVision; // Add a reference to the EnemyVision point

    public bool autoWalk= false;

    public bool isHitting = false;
    public bool hasHit = false;
    public float hittingDuration=0.5f;
    public float hittingDelay=0.5f;
    public bool dangerDetected=false;

    //bullet
    public GameObject bullet;
    public Transform firepoint;

    public float maxHealth=100;
    private float currentHealth;
    public float damage=20;

    public GameObject anas;
    public GameObject SalimBar;



    void Start()
    {
        anim = GetComponent<Animator>();
        speed=NormalSpeed;
        InvokeRepeating("ShootAtPlayer", 0f, hittingDelay);
        currentHealth=maxHealth;
    }


    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("detected", dangerDetected);
        anim.SetBool("hit", isHitting);
        
        if (DetectPlayer())
        {
            autoWalk=false;
            if(isFacingRight){
                flip();
                isFacingRight=false;
            }
            speed=0;
        }

        if(autoWalk){
            Walk();
        }
        
        if(currentHealth<=0){
            disableDangerDetected();
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
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
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

    void enableHitting(){
        isHitting=true;

    }
    void disableHitting(){
        isHitting=false;
    }


    void ShootAtPlayer(){
        if(dangerDetected && !FindObjectOfType<Player>().isInStealth()){
            enableHitting();
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            Invoke("disableHitting", hittingDuration);
        }
    }

    public void getHit(){
        currentHealth-=damage;
        FindObjectOfType<SalimHealthBar>().SetHealth(currentHealth);
    }
    public void RespawnSalim(){
        currentHealth=maxHealth;
        FindObjectOfType<SalimHealthBar>().SetMaxHealth(maxHealth);
    }

    public void enableDangerDetected(){
        if(!dangerDetected){
            dangerDetected=true;
            SalimBar.SetActive(true);
            FindObjectOfType<SalimHealthBar>().SetMaxHealth(maxHealth);
            FindObjectOfType<Lvl4Manager>().startFinalFight();
        }
    }
    public void disableDangerDetected(){
        dangerDetected=false;
        FindObjectOfType<Lvl4Manager>().startScene();
    }

    public void fallToDeath(){
        anim.SetBool("Shaking", true);
        Invoke("Die", 7);
    }
    public void Die(){
        SalimBar.SetActive(false);
        anas.SetActive(false);
        FindObjectOfType<Lvl4Manager>().showAnas();
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
        autoWalk=true;
        speed=5;
        Walk();
        Destroy(gameObject,10);
    }
}