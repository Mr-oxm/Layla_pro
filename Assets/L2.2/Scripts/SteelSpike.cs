using System.Collections;
using UnityEngine;
public class SteelSpike : MonoBehaviour
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
            FindObjectOfType<level2_2Manager>().RespawnPlayer();
            FindObjectOfType<PlatformDestruction>().RespawnPlatform();
        }
    }
}