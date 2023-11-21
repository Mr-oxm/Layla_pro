using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGang : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Salim>().enableAutoWalk();
            FindObjectOfType<AutoWalkingEnemy>().enableAutoWalk();
            Destroy(gameObject);
        }
    }
}
