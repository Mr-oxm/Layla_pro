using System;
using UnityEngine;

namespace L3._2.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private DoorController doorController;
        [SerializeField] private GameObject aziz;

        private bool isCanvasShown;
        private bool isSecuritySystemPassed;

        private void Awake()
        {
            CloseCanvas();
            
            if (doorController == null)
                doorController = FindObjectOfType<DoorController>();
        }

        private void Update()
        {
            if (isSecuritySystemPassed && !doorController.IsOpened())
                doorController.Open();
        }

        public void CloseCanvas()
        {
            canvas.SetActive(false);
            isCanvasShown = false;
        }

        public void ShowCanvas()
        {
            canvas.SetActive(true);
            isCanvasShown = true;
        }
        
        public bool IsCanvasShown()
        {
            return isCanvasShown;
        }
        
        public void SetSecuritySystemPassed()
        {
            isSecuritySystemPassed = true;
        }
    }
}
