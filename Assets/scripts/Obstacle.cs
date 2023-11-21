using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<LevelManager>().RespawnPlayer();
            FindObjectOfType<EnemyAI>().RespawnEnemy();
        }

        if(other.tag == "RunningEnemy" && gameObject.tag == "Fire"){
            FindObjectOfType<EnemyAI>().RespawnEnemy();
        }
    }
}
