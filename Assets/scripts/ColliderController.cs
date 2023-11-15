using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public BoxCollider2D firstCollider; // Reference to the first BoxCollider2D component
    public BoxCollider2D secondCollider; // Reference to the second BoxCollider2D component

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.C) && (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A)))
        {
            // Toggle the active state of the colliders
            firstCollider.enabled = false;
            secondCollider.enabled = true;
        }
        else
        {
            firstCollider.enabled = true;
            secondCollider.enabled = false;
        }
    }
}
