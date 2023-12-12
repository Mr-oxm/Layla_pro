using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class RockManager_level2_2 : MonoBehaviour
    {
        [Header("Rock Spawning")]
        [SerializeField] int maxRocks = 10;
        [SerializeField] GameObject rockPrefab;
        [SerializeField] float spawnMinDelay = 3f;
        [SerializeField] float spawnMaxDelay = 5f;
        
        private BoxCollider2D boxCollider;
        private float stratX;
        float endX;
        int rockCount = 0;
        float delay = 1f;
        float timer = 20;

        void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            
            var bounds = boxCollider.bounds;
            stratX = bounds.min.x;
            endX = bounds.max.x;
        }

        private void Update()
        {
            if (rockCount < maxRocks && timer >= delay)
            {
                SpawnRock();
                timer = 0;
                delay = UnityEngine.Random.Range(spawnMinDelay, spawnMaxDelay);
            }
            
            timer += Time.deltaTime;
          
        }
        
        void SpawnRock()
        {
            var position = new Vector3(UnityEngine.Random.Range(stratX, endX), transform.position.y, transform.position.z);
            Instantiate(rockPrefab, position, Quaternion.identity);
            rockCount++;
        }

        public void RockDestroyed()
        {
            rockCount--;
        }
    }

