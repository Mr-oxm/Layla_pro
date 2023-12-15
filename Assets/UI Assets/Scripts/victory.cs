using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victory : MonoBehaviour
{
    public GameObject PauseScreen;
    public static bool Paused;
    public bool show;

    void Start()
    {
        Paused = false;
        PauseScreen.SetActive(false);
        show=false;
    }

    
    void Update()
    {
        if (show && !Paused)
        {
            Pause();
        }
    }

    void Pause()
    {
        PauseScreen.SetActive(true);
        Paused = true;
        Time.timeScale = 0;
        FindObjectOfType<Player>().pausePlayer();
    }

    public void Resume()
    {
        PauseScreen.SetActive(false);
        Paused = false;
        Time.timeScale = 1;
        FindObjectOfType<Player>().resumePlayer();
    }
    public void beVisible(){
        show=true;
    }

    public void next(){
        show=false;
        Resume();
        FindObjectOfType<NavigationController>().next();
    }
}
