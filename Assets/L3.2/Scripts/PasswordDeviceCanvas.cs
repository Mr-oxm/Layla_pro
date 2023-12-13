using System.Collections;
using UnityEngine;

namespace L3._2.Scripts
{
    public class PasswordDeviceCanvas : MonoBehaviour
    {
        [SerializeField] private SecurityDevice securityDevice;
        [SerializeField] private AudioClip buttonPressClip;
        [SerializeField] private AudioClip successClip;
        [SerializeField] private AudioClip failureClip;
        
        private AudioSource audioSource;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            
            if (securityDevice == null)
                securityDevice = FindObjectOfType<SecurityDevice>();
        }
        
        public void TryPassword(string password)
        { 
            audioSource.clip = buttonPressClip;
            audioSource.Play();

            StartCoroutine(PlayResultSoundDelayedCoroutine(password));
        }

        private IEnumerator PlayResultSoundDelayedCoroutine(string password)
        {
            // Wait for the duration of the buttonPressClip
            yield return new WaitForSeconds(buttonPressClip.length);

            // Now that the buttonPressClip has finished, play the result sound
            audioSource.clip = securityDevice.IsPasswordCorrect(password) ? successClip : failureClip;
            audioSource.Play();
        }


    }
}