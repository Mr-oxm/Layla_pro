using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace L2.Scripts
{
    public class Rock : MonoBehaviour
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
            FindObjectOfType<RockManager>().RockDestroyed();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (onGround) return;

            if (other.gameObject.CompareTag("Player"))
                FindObjectOfType<LevelManager>().RespawnPlayer();
            else if (other.gameObject.CompareTag("Ground"))
                onGround = true;
        }
    }
}
