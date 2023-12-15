using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Window : MonoBehaviour
{
    [SerializeField] private float hits=3;
    private bool broken=false;
    private bool triggered=false;


    void Update(){
        if(!broken){
            if(hits<=0){
                broken=true;
                GetComponent<SpriteRenderer>().enabled=false;
            }
        }
        if(triggered&& broken){
            if(!FindObjectOfType<Player>().grounded){
                SceneManager.LoadScene(13);
                print("Game end");
            }
        }

    }
    public void getDamage(){
        hits= hits - 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            triggered=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            triggered=false;
        }
    }
}
