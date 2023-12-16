using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI  scoreText;

    void Update(){
        if(scoreText!=null)
            scoreText.text= score+"";
    }
    public void resetScore(){
        score=0;
    }
    public void increaseScore(){
        score++;
    }


}
