using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4Manager : LevelManager
{
    [SerializeField] private bool shaking = false;
    [SerializeField] private GameObject rocksManagers;
    [SerializeField] private GameObject hideOut;
    [SerializeField] private GameObject light;
    private Animator hideOutAnimator;

    void Start()
    {
        rocksManagers.SetActive(false);
        hideOutAnimator = hideOut.GetComponent<Animator>();
    }

    void Update()
    {
        hideOutAnimator.SetBool("Shaking", shaking);
    }

    public override void RespawnPlayer()
    {
        FindObjectOfType<Player>().transform.position = CurrentCheckpoint.transform.position;
    }

    public void enableShaking()
    {
        shaking = true;
        rocksManagers.SetActive(true);
        SetChildAnimatorsEnabled(light.transform, shaking);
    }
    

    private void SetChildAnimatorsEnabled(Transform parent, bool enabled)
    {
        foreach (Transform child in parent)
        {
            Animator animator = child.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Shaking", enabled);
            }

            // Recursively set child animators
            SetChildAnimatorsEnabled(child, enabled);
        }
    }
}
