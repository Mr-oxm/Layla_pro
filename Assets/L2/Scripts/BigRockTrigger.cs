using System;
using UnityEngine;

namespace L2.Scripts
{
    public class BigRockTrigger : MonoBehaviour
    {
        [SerializeField] GameObject bigRock;
        [SerializeField] GameObject wall;
        [SerializeField] int choice=0;

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            TriggerRock();
            if(choice==1){
                wall.SetActive(false);
            }
            FindObjectOfType<Table>().SetCanHide();
        }

        private void TriggerRock()
        {
            var rb = bigRock.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

}