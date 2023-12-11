using UnityEngine;

namespace L2.Scripts
{
    public class Table : MonoBehaviour
    {
        private bool canHide;
        private Player player;
        private LevelManager levelManager;
        private Dad daddy;
        
        private void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            player = FindObjectOfType<Player>();
            daddy = FindObjectOfType<Dad>();
        }

        private void Update()
        {
            if (canHide && player.isInStealth())
                daddy.GoDaddyGo();
        }

        public void SetCanHide()
        {
            canHide = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.CompareTag("Player"))
            //     isTriggered = true;
        }
        
    }
}
