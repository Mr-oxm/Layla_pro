using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFightTrigger : MonoBehaviour
{

    [SerializeField] private float delay;
    [SerializeField] private int  choice=0; //0 for final fight, 1 for earthquck
    private bool triggered=false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggered=true;
            Invoke("StartAction", delay);
        }
    }

    void StartAction(){
        if(triggered){
            if(choice==0){
                FindObjectOfType<Salim_lvl4>().enableDangerDetected();
            }else if(choice==1){
                FindObjectOfType<Lvl4Manager>().enableShaking();
            }else if(choice==2){
                FindObjectOfType<Lvl4Manager>().enableShaking();
            }
        }
    }
}
