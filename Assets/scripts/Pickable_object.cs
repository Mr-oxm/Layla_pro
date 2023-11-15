using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_object : MonoBehaviour
{
    public KeyCode PickKey;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (Input.GetKeyDown(PickKey))
            {
                FindObjectOfType<Player>().ShowKnife = true;
                // Destroy the current GameObject
                Destroy(gameObject);
            }
        }
    }
}
