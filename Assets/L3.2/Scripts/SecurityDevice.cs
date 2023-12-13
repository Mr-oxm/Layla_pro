using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace L3._2.Scripts
{
    public class SecurityDevice : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons;
        public KeyCode open;
        public KeyCode close;

        private const string CorrectPassword = "4729";
        private LevelManager levelManager;
        private bool canBeUsed;

        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void Update()
        {
            if (canBeUsed && Input.GetKeyDown(open))
                levelManager.ShowCanvas();

            if (levelManager.IsCanvasShown() && Input.GetKeyDown(close))
                levelManager.CloseCanvas();
        }
        
        public bool IsPasswordCorrect(string password)
        {
            if (password != CorrectPassword) return false;
            
            levelManager.SetSecuritySystemPassed();
            return true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                canBeUsed = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                canBeUsed = false;
        }
    }
}