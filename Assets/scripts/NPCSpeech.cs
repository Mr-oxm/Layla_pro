using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeech : MonoBehaviour
{
    public AudioSource speech;
    public bool isFinsihed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isFinsihed)
        {
            speech.Play();
            isFinsihed= true;
        }
    }
}
