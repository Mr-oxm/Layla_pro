using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4Manager : LevelManager
{
    [SerializeField] private bool shaking = false;
    [SerializeField] private GameObject rocksManagers;
    [SerializeField] private GameObject hideOut;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject battleField;
    [SerializeField] private GameObject npcs;
    [SerializeField] private GameObject afterMath;
    [SerializeField] private GameObject anas;
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
        FindObjectOfType<ScoreController>().increaseScore();
        FindObjectOfType<Player>().transform.position = CurrentCheckpoint.transform.position;
        FindObjectOfType<Salim_lvl4>().RespawnSalim();
    }

    public void enableShaking()
    {
        shaking = true;
        rocksManagers.SetActive(true);
        SetChildAnimatorsEnabled(light.transform, shaking);
        battleField.SetActive(false);
        npcs.SetActive(false);
        FindObjectOfType<Salim_lvl4>().fallToDeath();
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

    public void startFinalFight(){
        battleField.SetActive(true);
    }

    public void startScene(){
        afterMath.SetActive(true);
    }

    public void showAnas(){
        anas.SetActive(true);
    }


}
