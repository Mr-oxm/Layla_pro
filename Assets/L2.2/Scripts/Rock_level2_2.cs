using System.Collections.Generic;
using UnityEngine;

public class Rock_level2_2 : MonoBehaviour
    {
        [Header("Rock Properties")]
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private float destroyAfter = 3f;

        private RockManager_level2_2 rockManager;
        private SpriteRenderer spriteRenderer;
        private bool onGround;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];

            Invoke("DestroySelf", destroyAfter);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
            rockManager.RockDestroyed();
        }

        public void SetManager( RockManager_level2_2 manager)
        { 
            rockManager = manager;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (onGround) return;

            if (other.gameObject.CompareTag("Player"))
                FindObjectOfType<LevelManager>().RespawnPlayer();
            else if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
            {
                onGround = true;
            }
        }
    }