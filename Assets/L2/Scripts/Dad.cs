using System;
using UnityEngine;

namespace L2.Scripts
{
    public class Dad :MonoBehaviour
    {
        [SerializeField] GameObject targetPoint;
        [SerializeField] float speed = 2F;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private bool isTriggered;
        private bool reachedTargetPoint;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isTriggered && !reachedTargetPoint)
                MoveToTarget();

            if (targetPoint.transform.position.x >= transform.position.x)
                ReachedTargetPoint();
        }

        private void MoveToTarget()
        {
            if (reachedTargetPoint)
            {
                animator.SetFloat("Speed", 0);
                return;
            }
            
            var direction = targetPoint.transform.position - transform.position;
            direction.Normalize();
            rigidbody.velocity = direction * speed;
            
            var x = direction.x * speed;
            animator.SetFloat("Speed", Math.Abs(x));
        }

        public void GoDaddyGo()
        {
            isTriggered = true;
        }
        
        private void ReachedTargetPoint()
        {
            reachedTargetPoint = true;
            rigidbody.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }
    }
}
