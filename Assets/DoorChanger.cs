using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChanger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<DoorController>().SwitchObjects();
            Destroy(gameObject);
        }
    }
}
