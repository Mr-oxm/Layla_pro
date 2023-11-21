using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Reference to waypoints
    public List<Transform> points;
    //The int value for next point index
    public int nextID = 0;
    //The value of that applies to ID for changing
    int idChangeValue = 1;
    //Speed of movement
    public float speed;
    //Normal Speed of movement
    public float NormalSpeed ;
    //Running Speed of movement
    public float runningSpeed;

    private bool isFacingRight = true;
    private bool runing = false;
    
    // private bool isDead=false;

    private Animator anim;

    public Transform enemyVision; // Add a reference to the EnemyVision point

    //for running enemy
    public Vector2 originalEmptyPosition;
    private bool hasChangeVision=false;

    //voice 
    public List<AudioSource> audioSources;
    private bool hasScreamed = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Reset()
    {
        Init();
    }

    void Init()
    {

        //Create Root object
        GameObject root = new GameObject(name + "_Root");
        //Reset Position of Root to enemy object
        root.transform.position = transform.position;
        //Set enemy object as child of root
        transform.SetParent(root.transform);
        //Create Waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //Reset waypoints position to root        
        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //Create two points (gameobject) and reset their position to waypoints objects
        //Make the points children of waypoint object
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        GameObject vision = new GameObject("Enemy_vision"); vision.transform.SetParent(this.transform); vision.transform.position = root.transform.position;
        enemyVision = vision.transform;
        originalEmptyPosition = enemyVision.position;
        //Init points list then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    private void Update()
    {
        if (transform.position.x== GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            FindObjectOfType<LevelManager>().RespawnPlayer();
            transform.position = points[1].position;
        }
        if (DetectPlayer())
        {
            speed = runningSpeed;
            MoveTowardsPlayer();
            runing = true;
            if (!hasScreamed)
            {
                PlayRandomAudio();
                hasScreamed = true;
            }
        }
        else
        {
            speed = NormalSpeed;
            MoveToNextPoint();
            runing = false;
            hasScreamed = false;
        }

        anim.SetBool("run", runing);
    }

    bool DetectPlayer()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        float distanceToEnemyVision = Vector2.Distance(transform.position, enemyVision.position);
        float distanceFromEnemyVisionToPlayer = Vector2.Distance(enemyVision.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        // Check if player is in between enemy and enemyVision point
        if (distanceToPlayer < distanceToEnemyVision&& distanceFromEnemyVisionToPlayer< distanceToEnemyVision && !FindObjectOfType<Player>().isInStealth() )
        {
            return true;
        }
        return false;
    }

    void MoveTowardsPlayer()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Flip the enemy to face the player
        if (playerTransform.position.x > transform.position.x && !isFacingRight)
        {
            flip();
            isFacingRight = true;
        }
        else if (playerTransform.position.x < transform.position.x && isFacingRight)
        {
            
            flip();
            isFacingRight = false;
        }


        // Calculate the target position only along the X-axis
        Vector2 targetPosition = new Vector2(playerTransform.position.x, transform.position.y);

        // Move the enemy towards the player only along the X-axis
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void MoveToNextPoint()
    {
        //Get the next Point transform
        Transform goalPoint = points[nextID];
        //Flip the enemy transform to look into the point's direction
        if (goalPoint.transform.position.x > transform.position.x)
        {
            /*GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);*/
            if (!isFacingRight)
            {
                flip();
                isFacingRight = true;

            }
        }
        else
        {
            /*GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);*/
            if (isFacingRight)
            {
                flip();
                isFacingRight = false;
            }
        }
        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        //Check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            //Check if we are at the end of the line (make the change -1)
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //Check if we are at the start of the line (make the change +1)
            if (nextID == 0)
                idChangeValue = 1;
            //Apply the change on the nextID
            nextID += idChangeValue;
        }
    }

    void flip()
    {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !FindObjectOfType<Player>().isInStealth())
        {
            FindObjectOfType<LevelManager>().RespawnPlayer();
            RespawnEnemy();
        }

        if(other.tag == "Player" && gameObject.tag== "RunningEnemy" && !hasChangeVision && FindObjectOfType<Player>().isInStealth()){
            originalEmptyPosition = enemyVision.position;
            float xPosition;
            if(isFacingRight){
                xPosition= enemyVision.position.x + 20;
            }else{
                xPosition= enemyVision.position.x - 20;
            }
            enemyVision.position = new Vector2(xPosition, enemyVision.position.y);
            hasChangeVision=true;
        }
    }

    void PlayRandomAudio()
    {
        if (audioSources.Count > 0)
        {
            int randomIndex = Random.Range(0, audioSources.Count);
            AudioSource randomAudio = audioSources[randomIndex];
            randomAudio.Play();
        }
    }
    public void RespawnEnemy(){
        transform.position = points[1].position;
        if(hasChangeVision){
            print("working");
            enemyVision.position = originalEmptyPosition;
        }
        hasChangeVision=false;
    }

}