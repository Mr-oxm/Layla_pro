using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatorTrigger : MonoBehaviour
{
    public int sceneNumber=0;
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
        if (other.CompareTag("Player") || other.CompareTag("Dad"))
        {
            triggered=true;
            if(!byKey){
                Invoke("StartLoading", delay);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dad"))
        {
            triggered=false;
        }
    }

    private void StartLoading(){
        FindObjectOfType<NavigationController>().ExecuteFunction(sceneNumber);
    }
}
