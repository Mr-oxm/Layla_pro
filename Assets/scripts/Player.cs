using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
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
    public bool isAutoWalk = true;
    private bool isChecking = false;
    private bool isRuningTo = false;
    private bool isStabbed = false;


    //sound effects
    private bool hasPlayedWalkSound = false; // for autowalk
    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource sprintSound;
    public AudioSource crouchSound;


    //knife
    public bool ShowKnife = false;
    public bool ShowStabingKnife = false;

    //combat
    public KeyCode hit;
    public bool isHitting = false;
    public float hittingDuration=5;
    public bool hasGun=false;
    public GameObject bullet;
    public Transform firepoint;

    private bool paused=false;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update(){
        if(!paused){

            UpdateAnimations();

            if (isAutoWalk)
            {
                AutoWalk();
            }

            if (isHitting)
            {
                HandleHitting();
            }

            if (isStabbed)
            {
                // Handle stabbed logic if needed
            }

            
            
            if (!isChecking && !isAutoWalk && !isRuningTo && !isStabbed)
            {
                HandleSprint();
                HandleCrouch();
                HandleJump();
                HandleMovement();
                HandleSoundEffects();
                HandleStealth();
                HandleHittingInput();
            }
        }
    }

    void UpdateAnimations()
    {
        //animation
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("stealth", isInStealthMode);
        anim.SetBool("crouch", isInCrouchMode);
        anim.SetBool("Check", isChecking);
        anim.SetBool("hit", isHitting);
        anim.SetBool("hasGun", hasGun);
    }

    void AutoWalk()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (!hasPlayedWalkSound)
        {
            walkSound.Play();
            hasPlayedWalkSound = true;
        }
    }

    void HandleHitting()
    {
        Invoke("disableHitting", hittingDuration);
    }

    void HandleSprint()
    {
        if (Input.GetKeyDown(Sprint))
        {
            moveSpeed = runningSpeed;
        }

        if (Input.GetKeyUp(Sprint))
        {
            moveSpeed = normalSpeed;
            sprintSound.Stop();
        }
    }

    void HandleCrouch()
    {
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
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(Spacebar) && grounded && !isInStealthMode)
        {
            jumpSound.Play();
            Jump();
        }
    }

    void HandleMovement()
    {
        if (Input.GetKey(L))
        {
            MoveLeft();
        }

        if (Input.GetKey(R))
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (isFacingRight)
        {
            flip();
            isFacingRight = false;
        }
    }

    void MoveRight()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (!isFacingRight)
        {
            flip();
            isFacingRight = true;
        }
    }

    void HandleSoundEffects()
    {
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
    }

    void HandleStealth()
    {
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

    }

    void HandleHittingInput()
    {
        if (Input.GetKeyDown(hit) && ShowKnife && !isInStealthMode && grounded )
        {
            enableHitting();
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
    public void stopPlayerPermanant()
    {
        // playerRigidbody.velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

        // Set the target velocity to zero
        Vector2 targetVelocity = Vector2.zero;

        // Linearly interpolate between the current velocity and the target velocity
        playerRigidbody.velocity = Vector2.Lerp(playerRigidbody.velocity, targetVelocity, 0.001f);

        moveSpeed=0;
        isStabbed=true;
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
        if(hasGun && !isHitting){
            Shoot();
        }
        isHitting=true;
    }
    void disableHitting(){
        isHitting=false;
    }


    public void getStapped(){
        isInCrouchMode=true;
        ShowKnife=false;
        ShowStabingKnife=true;
        
    }

    void Shoot(){
        Instantiate(bullet, firepoint.position, firepoint.rotation);
    }

    public void pausePlayer(){
        paused=true;
    }
    public void resumePlayer(){
        paused=false;
    }
}
