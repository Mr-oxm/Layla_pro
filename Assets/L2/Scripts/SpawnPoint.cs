using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace L2.Scripts
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] LocationName locationName;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            var levelManager = FindObjectOfType<LevelManager>();
            levelManager.SetSpawnPoint(gameObject);
            levelManager.SetPlayerLocation(locationName);
        }
    }

}