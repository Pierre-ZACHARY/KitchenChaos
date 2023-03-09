using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProCount;
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager.OnStateChange += OnStateChange;
        Hide();
    }

    private void OnStateChange(object sender, EventArgs e)
    {
        GameManager gm = sender as GameManager;
        if (gm == null) return;
        switch (gameManager.CurrentState)
        {
            case GameManager.State.GameOver:
                Show();
                break;
            default:
                Hide();
                break;
        }
    }

    public void Show()
    {
        textMeshProCount.text = deliveryManager.GetSuccessfulDeliveries().ToString();
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
