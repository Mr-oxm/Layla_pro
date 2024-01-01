using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void GoCutScene1(){
        SceneManager.LoadScene(1);
    }
    public void GoLevel1(){
        SceneManager.LoadScene(2);
    }
    public void GoCutScene2(){
        SceneManager.LoadScene(3);
    }
    
    public void GoLevel2_1(){
        SceneManager.LoadScene(4);
    }
    public void GoCutScene3(){
        SceneManager.LoadScene(5);
    }

    public void GoLevel2_2(){
        SceneManager.LoadScene(6);
    }
    public void GoCutScene4(){
        SceneManager.LoadScene(7);
    }
    public void GoCutScene5(){
        SceneManager.LoadScene(8);
    }

    public void GoLevel3(){
        SceneManager.LoadScene(9);
    }
    public void GoLevel3_2(){
        SceneManager.LoadScene(10);
    }

    public void GoCutScene6(){
        SceneManager.LoadScene(11);
    }
    public void GoLevel4(){
        SceneManager.LoadScene(12);
    }
    public void GoCutScene7(){
        SceneManager.LoadScene(13);
    }

    public void next(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void reload(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void previous(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void ExecuteFunction(int functionIndex)
    {
        switch (functionIndex)
        {
            case 0:
                mainMenu();
                break;
            case 1:
                GoCutScene1();
                break;
            case 2:
                GoLevel1();
                break;
            case 3:
                GoCutScene2();
                break;
            case 4:
                GoLevel2_1();
                break;
            case 5:
                GoCutScene3();
                break;
            case 6:
                GoLevel2_2();
                break;
            case 7:
                GoCutScene4();
                break;
            case 8:
                GoCutScene5();
                break;
            case 9:
                GoLevel3();
                break;
            case 10:
                GoLevel3_2();
                break;
            case 11:
                GoCutScene6();
                break;
            case 12:
                GoLevel4();
                break;
            case 13:
                GoCutScene7();
                break;
            case 14:
                next();
                break;
            case 15:
                reload();
                break;
            case 16:
                previous();
                break;
            case 17:
                Quit();
                break;
            default:
                Debug.LogError("Invalid function index");
                break;
        }
    }
}
