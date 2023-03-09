using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    
    [SerializeField] private GameManager gameManager;

    private void FixedUpdate()
    {
        if(gameManager.CurrentState == GameManager.State.CountDownToStart)
        {
            textMesh.text = Mathf.CeilToInt(gameManager.RemainingCountownTime).ToString("D");
        }
        else
        {
            textMesh.text = "";
        }
    }
}
