using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction towards the player
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        // Calculate the rotation angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle to degrees and rotate the gun
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
}
