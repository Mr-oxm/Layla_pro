using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2_2Manager : LevelManager
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void RespawnPlayer()
    {
        FindObjectOfType<Layla_crawling>().transform.position = CurrentCheckpoint.transform.position;
        FindObjectOfType<Layla_crawling>().transform.rotation = CurrentCheckpoint.transform.rotation;
        foreach (var obstacle in FindObjectsOfType<PlatformDestruction>())
            obstacle.RespawnPlatform();
        
    }
}