using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public float delay=0f;

    public bool byKey=false;
    public bool triggered=false;
    public KeyCode enterKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(byKey&&triggered){
            if(Input.GetKeyDown(enterKey)){
                StartLoading();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered=true;
            if(!byKey){
                Invoke("StartLoading", delay);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered=false;
        }
    }

    private void StartLoading(){
        FindObjectOfType<victory>().beVisible();
    }
}
