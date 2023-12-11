using System;
using UnityEngine;

namespace L2.Scripts
{
    public class Mom : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float allowedMargin = 2f;


        private Animator animator;
        private bool isFacingRight = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;

                // Check if the distance is greater than the allowed margin
                if (direction.magnitude > allowedMargin)
                {
                    MoveTowardsTarget(direction);
                    FlipCharacter(direction.x);
                }
                else
                {
                    animator.SetFloat("Speed", 0);

                }
            }
        }

        private void MoveTowardsTarget(Vector3 direction)
        {
            direction.Normalize();
            Vector3 movement = speed * Time.deltaTime * direction;
            transform.position += new Vector3(movement.x, movement.y, 0);
            animator.SetFloat("Speed", speed);
        }

        private void FlipCharacter(float horizontalMovement)
        {
            var localScale = transform.localScale;
            var direction = 1;

            if (horizontalMovement < 0 && isFacingRight)
            {
                isFacingRight = false;
                direction = -1;
            }
            else if (horizontalMovement > 0 && !isFacingRight)
            {
                isFacingRight = true;
                direction = -1;
            }

            transform.localScale = new Vector3(direction * localScale.x, localScale.y, localScale.z);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}