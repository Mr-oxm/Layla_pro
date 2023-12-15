using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    void Start()
    {
        // Enable sprite renderer for the first object and disable it for the second object
        SetInitialState();
    }

    void SetInitialState()
    {
        if (firstObject != null && secondObject != null)
        {
            // Enable sprite renderer for the first object
            SpriteRenderer firstSpriteRenderer = firstObject.GetComponent<SpriteRenderer>();
            if (firstSpriteRenderer != null)
            {
                firstSpriteRenderer.enabled = true;
            }

            // Disable sprite renderer and collider2D for the second object
            SpriteRenderer secondSpriteRenderer = secondObject.GetComponent<SpriteRenderer>();
            Collider2D secondCollider = secondObject.GetComponent<Collider2D>();

            if (secondSpriteRenderer != null)
            {
                secondSpriteRenderer.enabled = false;
            }

            if (secondCollider != null)
            {
                secondCollider.enabled = false;
            }
        }
        else
        {
            Debug.LogError("Please assign both GameObjects in the inspector.");
        }
    }

    public void SwitchObjects()
    {
        // Disable sprite renderer for the first object
        SpriteRenderer firstSpriteRenderer = firstObject.GetComponent<SpriteRenderer>();
        if (firstSpriteRenderer != null)
        {
            firstSpriteRenderer.enabled = false;
        }

        // Enable sprite renderer and collider2D for the second object
        SpriteRenderer secondSpriteRenderer = secondObject.GetComponent<SpriteRenderer>();
        Collider2D secondCollider = secondObject.GetComponent<Collider2D>();

        if (secondSpriteRenderer != null)
        {
            secondSpriteRenderer.enabled = true;
        }

        if (secondCollider != null)
        {
            secondCollider.enabled = true;
        }
    }
}
