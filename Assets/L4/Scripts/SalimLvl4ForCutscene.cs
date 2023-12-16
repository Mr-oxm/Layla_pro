using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalimLvl4ForCutscene : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float animationDelay;
    [SerializeField] private bool isMoving=false;
    

    private Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
        Invoke("StartAnimation", duration);
        Destroy(this.gameObject, duration+animationDelay);
    }

    void Update(){
        if(isMoving){
            anim.SetBool("detected", isMoving);
            anim.SetFloat("Speed", 5);
            anim.SetBool("Move", isMoving);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void StartAnimation(){
        isMoving=true;
        anim.SetBool("detected", isMoving);
    }

}
