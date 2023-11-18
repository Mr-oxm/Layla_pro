using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed;
    public float runningSpeed;
    public float normalSpeed;

    public float jumpHeight;
    public bool isFacingRight;
    public KeyCode Spacebar;

    public KeyCode L;
    public KeyCode R;

    public KeyCode Sprint;
    public KeyCode Crouch;


    public Transform groundCheck;
    public float groundCheckRadius;

    public LayerMask whatIsGround;
    public bool grounded;

    private Animator anim;

    private Rigidbody2D playerRigidbody;


    //stealth 
    public GameObject stealthObject;
    public KeyCode stealthKey;
    private bool isInStealthMode = false;


    private bool isInCrouchMode = false;
    private bool isColidingWithStealth = false;
    private bool isAutoWalk = true;
    private bool isChecking = false;
    private bool isRuningTo = false;


    //sound effects
    private bool hasPlayedWalkSound = false; // for autowalk
    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource sprintSound;
    public AudioSource crouchSound;


    //knife
    public bool ShowKnife = false;

    //combat
    public KeyCode hit;
    public bool isHitting = false;
    public float hittingDuration=5;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //animation
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("stealth", isInStealthMode);
        anim.SetBool("crouch", isInCrouchMode);
        anim.SetBool("Check", isChecking);
        anim.SetBool("hit", isHitting);


        if (isAutoWalk)
        {
            autoWalk();
        }

        if(isHitting){
            Invoke("disableHitting", hittingDuration);
        }

        
        if (!isChecking && !isAutoWalk && !isRuningTo)
        {

            // Check if the Shift key is pressed
            if (Input.GetKeyDown(Sprint))
            {
                // change the player's speed
                moveSpeed = runningSpeed;
            }
            if (Input.GetKeyUp(Sprint))
            {
                // Reset the player's speed to the original value
                moveSpeed = normalSpeed;

                sprintSound.Stop();
            }

            // Check if the crouch key is pressed
            if (Input.GetKey(Crouch) && (Input.GetKey(R) || Input.GetKey(L)) && Input.GetKey(Sprint))
            {
                isInCrouchMode = true;
                sprintSound.Stop();
            }
            if (Input.GetKeyUp(Crouch))
            {
                isInCrouchMode = false;

                crouchSound.Stop();
            }
            if (Input.GetKeyDown(Crouch))
            {
                crouchSound.Play();
            }


            //jump
            if (Input.GetKeyDown(Spacebar) && grounded)
            {
                jumpSound.Play();
                Jump();
            }

            //moving left
            if (Input.GetKey(L))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                if (isFacingRight)
                {
                    flip();
                    isFacingRight = false;
                }

            }

            //moving right
            if (Input.GetKey(R))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                if (!isFacingRight)
                {
                    flip();
                    isFacingRight = true;

                }

            }

            // walking sound effects logic
            if ((Input.GetKeyDown(R) || Input.GetKeyDown(L)) || ((Input.GetKey(R) || Input.GetKey(L)) && Input.GetKeyUp(Sprint)))
            {
                walkSound.Play();
            }
            if ((Input.GetKey(R) || Input.GetKey(L)) && Input.GetKeyDown(Sprint))
            {
                sprintSound.Play();
            }
            if ((Input.GetKeyUp(R) && !Input.GetKey(L)) || (Input.GetKeyUp(L) && !Input.GetKey(R)) || Input.GetKeyDown(Sprint))
            {
                walkSound.Stop();
            }
            

            //stealth logic
            if (isInStealthMode)
            {

                // Calculate the boundaries of the stealthObject based on its width
                float stealthObjectWidth = stealthObject.GetComponent<SpriteRenderer>().bounds.size.x;
                float leftBoundary = stealthObject.transform.position.x - stealthObjectWidth / 2f;
                float rightBoundary = stealthObject.transform.position.x + stealthObjectWidth / 2f;

                // Clamp the player's X position within the boundaries of the stealthObject
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary), transform.position.y, transform.position.z);

                // Stop walking animation when reaching boundaries
                if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
                {
                    anim.SetFloat("Speed", 0f); // Set the Speed parameter to 0 to stop the walking animation
                }
                else
                {
                    anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
                }

                // Exit stealth mode and return to "fore" sorting layer when the stealth key is pressed, Move left
                if (Input.GetKey(stealthKey) && Input.GetKey(L) && isColidingWithStealth)
                {
                    transform.Translate(Vector2.left * Time.deltaTime);
                    SetSortingLayer("Fore");
                    isInStealthMode = false;

                }
                // Exit stealth mode and return to "fore" sorting layer when the stealth key is pressed, Move right
                if (Input.GetKey(stealthKey) && Input.GetKey(R) && isColidingWithStealth)
                {
                    transform.Translate(Vector2.right * Time.deltaTime);
                    SetSortingLayer("Fore");
                    isInStealthMode = false;
                }
            }
            else
            {
                // Enter stealth mode upon collision with StealthObject
                if (Input.GetKeyDown(stealthKey) && isColidingWithStealth)
                {
                    // Calculate the closest edge of the stealthObject based on its width
                    float stealthObjectWidth = stealthObject.GetComponent<SpriteRenderer>().bounds.size.x;
                    float closestEdgeX = Mathf.Clamp(transform.position.x, stealthObject.transform.position.x - stealthObjectWidth / 2f,
                    stealthObject.transform.position.x + stealthObjectWidth / 2f);

                    // Move the player to the closest edge
                    transform.position = new Vector3(closestEdgeX, transform.position.y, transform.position.z);

                    // Enter stealth mode and take a step inside
                    SetSortingLayer("stealth");
                    transform.Translate(Vector2.right * Time.deltaTime); // Take a step inside
                    isInStealthMode = true;

                }
            }

            //Enable hitting
            if (Input.GetKeyDown(hit) && ShowKnife)
            {
                enableHitting();
            }
        }
    }

    void autoWalk()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (!hasPlayedWalkSound)
        {
            walkSound.Play();
            hasPlayedWalkSound = true;
        }
    }
    public void disableAutoWalk()
    {
        walkSound.Stop();
        isAutoWalk = false;
        hasPlayedWalkSound = false;
    }
    void flip()
    {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);

    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void SetSortingLayer(string layerName)
    {
        // Set sorting layer for the player object
        GetComponent<SpriteRenderer>().sortingLayerName = layerName;

        // Set sorting layer for all child sprites recursively
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = layerName;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StealthObject"))
        {
            stealthObject = other.gameObject;
            isColidingWithStealth = true;
        }
        /*        if (other.CompareTag("beaten"))
                {
                    FindObjectOfType<CameraZoom>().zoomIn();
                }*/
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("StealthObject"))
        {
            isColidingWithStealth = false;
        }
        /*        if (other.CompareTag("beaten"))
                {
                    FindObjectOfType<CameraZoom>().zoomOut();
                }*/
    }

    public void RunTo(Vector2 targetPosition)
    {
        isRuningTo = true;
        // Calculate the direction to the target position
        Vector2 direction = targetPosition - (Vector2)transform.position;


        // Update the velocity to move towards the target position
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, GetComponent<Rigidbody2D>().velocity.y);

        // Update the animation parameters
        anim.SetFloat("Speed", runningSpeed);
        anim.SetBool("Grounded", grounded);
        

    }
    public void stopPlayer()
    {
        playerRigidbody.velocity = Vector2.zero;
    }
    public void disableRunTo()
    {
        isRuningTo = false;
    }

    public bool isInStealth()
    {
        return isInStealthMode;
    }

    // Checking animation with daly
    public void StartChecking(float duration)
    {
        StartCoroutine(CheckOnCoroutine(duration));
    }

    // Coroutine for checking
    private IEnumerator CheckOnCoroutine(float duration)
    {
        isChecking = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        isChecking = false;
    }

    void enableHitting(){
        isHitting=true;
    }
    void disableHitting(){
        isHitting=false;
    }

}
