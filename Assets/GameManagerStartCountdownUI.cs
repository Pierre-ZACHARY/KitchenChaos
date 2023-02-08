using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private void FixedUpdate()
    {
        if(GameManager.Instance.CurrentState == GameManager.State.CountDownToStart)
        {
            textMesh.text = Mathf.CeilToInt(GameManager.Instance.RemainingCountownTime).ToString("D");
        }
        else
        {
            textMesh.text = "";
        }
    }
}
