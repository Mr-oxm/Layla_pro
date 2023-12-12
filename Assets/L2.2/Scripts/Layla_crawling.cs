using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layla_crawling : MonoBehaviour
{

    public float moveSpeed; 
   

    public bool isFacingRight;

    public KeyCode L; 
    public KeyCode R;

    private Animator anim;

    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {


        if (Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
        }
        
        if (Input.GetKey(R)) 
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (!isFacingRight) 
            {
                Flip(); 
                isFacingRight = true; 
            }
        }
        
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }
void Flip()
{
    transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
}
}
