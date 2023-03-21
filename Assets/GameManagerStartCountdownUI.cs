using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerStartCountdownUI : MonoBehaviour
{
    private String numberPopupTrigger = "NumberPopup";
    [SerializeField] private TextMeshProUGUI textMesh;
    
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private Animator animator;
    
    [SerializeField] private SoundManager soundManager;
    
    private int lastCountdownTime = 0;

    private void FixedUpdate()
    {
        if(gameManager.CurrentState == GameManager.State.CountDownToStart)
        {
            int countdown = Mathf.CeilToInt(gameManager.RemainingCountownTime);
            if(countdown != lastCountdownTime)
            {
                lastCountdownTime = countdown;
                textMesh.text = countdown.ToString("D");
                animator.SetTrigger(numberPopupTrigger);
                soundManager.PlayCountdownSound();
            }
        }
        else
        {
            textMesh.text = "";
        }
    }
}
