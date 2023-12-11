using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Rock_level2_2 : MonoBehaviour
    {
        [Header("Rock Properties")]
        [SerializeField] List<Sprite> sprites;
        [SerializeField] float destoryAfter = 3f;

        SpriteRenderer spriteRenderer;
        bool onGround = false;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];

            Invoke("DestroySelf", destoryAfter);
        }

        void DestroySelf()
        {
            Destroy(gameObject);
            FindObjectOfType<RockManager_level2_2>().RockDestroyed();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (onGround) return;

            if (other.gameObject.CompareTag("Player"))
                FindObjectOfType<level2_2Manager>().RespawnPlayer();
            else if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
            {
                onGround = true;
            }
        }
    }

