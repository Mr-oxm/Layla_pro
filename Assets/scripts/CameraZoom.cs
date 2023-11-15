using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    public float zoomSpeed;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    public void zoomIn()
    {
        cam.orthographicSize=Mathf.Lerp(cam.orthographicSize,4,Time.deltaTime*zoomSpeed);
    } 
    public void zoomOut()
    {
        cam.orthographicSize=Mathf.Lerp(cam.orthographicSize,5,Time.deltaTime*zoomSpeed);
    } 
}
