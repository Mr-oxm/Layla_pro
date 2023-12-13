using System;
using UnityEngine;

namespace L3_2.Scripts
{
    public class AzizTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject aziz;
        [SerializeField] private GameObject newAziz;

        private bool isTriggered;

        private void Awake()
        {
            newAziz.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isTriggered || !other.CompareTag("Player")) return;
            
            aziz.SetActive(false);
            newAziz.transform.position = aziz.transform.position;
            newAziz.SetActive(true);
            Destroy(aziz);
            isTriggered = true;
        }
    }
}