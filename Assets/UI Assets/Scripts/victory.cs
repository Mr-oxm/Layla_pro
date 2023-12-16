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
        if(PauseScreen!=null)
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
        if(FindObjectOfType<Player>()!=null){
            FindObjectOfType<Player>().pausePlayer();
        }
    }

    public void Resume()
    {
        PauseScreen.SetActive(false);
        Paused = false;
        Time.timeScale = 1;
        if(FindObjectOfType<Player>()!=null){
            FindObjectOfType<Player>().resumePlayer();
        }
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
