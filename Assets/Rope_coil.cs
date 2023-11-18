using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_coil : MonoBehaviour
{
    private bool inContact=false;
    public float DelayDuration=0.45f;

    void Update(){
        if( FindObjectOfType<Player>().isHitting && inContact){
            Invoke("destroyObj", DelayDuration);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            inContact=true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            inContact=false;
        }
    }

    void destroyObj(){
        Destroy(gameObject);
    }
}
