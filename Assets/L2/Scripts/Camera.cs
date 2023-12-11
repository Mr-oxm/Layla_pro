using UnityEngine;

namespace L2.Scripts
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] public float speed;
        [SerializeField] float[] bounds;
        
        private void Stat()
        {
            if (target == null)
                target = FindObjectOfType<Player>().transform;

            speed = 5F;
            bounds = new float[] {-7, 42, 2.8f, 0}; // minX, maxX, minY, maxY
        }

        void FixedUpdate()
        {
            Vector2 newCamPosition = Vector2.Lerp(transform.position, target.position, Time.deltaTime * speed);

            float x = Mathf.Clamp(newCamPosition.x, bounds[0], bounds[1]);
            float y = Mathf.Clamp(newCamPosition.y, bounds[2], bounds[3]);

            transform.position = new Vector3(x, y, -9f);
        }

        public void SetBounds(float[] bounds)
        {
            print("Bounds: " + bounds);
            this.bounds = bounds;
        }
        
        public float[] GetBounds()
        {
            return bounds;
        }
    }
}