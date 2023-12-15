using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{
    public GameObject PauseScreen;
    public static bool Paused;
    public KeyCode PauseButton;

    void Start()
    {
        Paused = false;
        PauseScreen.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(PauseButton) && !Paused && !FindObjectOfType<victory>().show)
        {
            Pause();
        }
        else if (Input.GetKeyDown(PauseButton) && Paused)
        {
            Resume();
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

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
