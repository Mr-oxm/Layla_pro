using UnityEngine;

namespace L3.Scripts
{
    public class Criminal:MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            foreach (var c in FindObjectsOfType<global::EnemyAI>())
            {
                Destroy(c.gameObject);
                Destroy(c);
            }
        }
    }
}
