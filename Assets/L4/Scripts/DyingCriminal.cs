using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingCriminal : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private bool shot=false;
    private Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
        Invoke("StartAnimation", duration);
    }



    void StartAnimation(){
        shot=true;
        anim.SetBool("Shot", shot);
    }

}
