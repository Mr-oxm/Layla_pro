using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_rope : MonoBehaviour
{
    public GameObject HangingRope;
    public GameObject SideRope;
    public GameObject Fire;

    public GameObject RopeCoil;
    public GameObject HangingPart;

    private Rigidbody2D fireRigid;
    private bool isDestroyed= false;
    private Animator animator;

    public float rotationSpeed = 2f;

    // Update is called once per frame
    void Start(){
        fireRigid= Fire.GetComponent<Rigidbody2D>();
        fireRigid.isKinematic = true;
        animator = HangingPart.GetComponent<Animator>();
    }
    void Update()
    {
        if(RopeCoil==null && !isDestroyed){
            Fire.tag="Fire";
            Destroy(SideRope);
            Destroy(HangingRope);
            fireRigid.isKinematic = false;
            animator.enabled = false;

            // Get the current rotation
            Vector3 currentRotation = HangingPart.transform.eulerAngles;

            // Set the z-axis rotation to zero
            currentRotation.z = 0f;

            // Apply the updated rotation using Vector3
            HangingPart.transform.eulerAngles = currentRotation;

            // // Get the current rotation
            // Vector3 currentRotation = HangingPart.transform.eulerAngles;

            // // Set the target rotation with z-axis set to zero
            // Vector3 targetRotation = new Vector3(currentRotation.x, currentRotation.y, 0f);

            // // Interpolate between the current rotation and the target rotation
            // Vector3 newRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);

            // // Apply the interpolated rotation
            // HangingPart.transform.eulerAngles = newRotation;
        }
    }
}
