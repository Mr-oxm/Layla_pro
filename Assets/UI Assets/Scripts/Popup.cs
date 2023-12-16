using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    public GameObject PauseScreen;
    public static bool Paused;
    public bool show;
    public TextMeshProUGUI  displayText;

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
        show=false;
        PauseScreen.SetActive(false);
        Paused = false;
        Time.timeScale = 1;
        if(FindObjectOfType<Player>()!=null){
            FindObjectOfType<Player>().resumePlayer();
        }
    }
    public void beVisible(string s){
        displayText.text=s;
        show=true;
    }

}
