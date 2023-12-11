using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_object : MonoBehaviour
{
    public KeyCode PickKey;
    public int choice=0;

    private bool picakble=false;

    void Update(){
        if (Input.GetKeyDown(PickKey)&& picakble)
        {
            FindObjectOfType<Player>().ShowKnife = true;
            if(choice==1){
                FindObjectOfType<Player>().hasGun = true;
            }
            // Destroy the current GameObject
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            picakble=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            picakble=false;
        }
    }
}
