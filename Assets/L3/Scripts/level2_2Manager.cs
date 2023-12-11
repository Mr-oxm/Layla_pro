using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2_2Manager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RespawnPlayer()
    {
        FindObjectOfType<PlatformDestruction>().RespawnPlatform();
        FindObjectOfType<Layla_crawling>().transform.position = CurrentCheckpoint.transform.position;
        FindObjectOfType<Layla_crawling>().transform.rotation = CurrentCheckpoint.transform.rotation;
        
    }
}