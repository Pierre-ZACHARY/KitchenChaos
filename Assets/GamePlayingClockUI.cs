using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;
    
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        clockImage.fillAmount = gameManager.GameTimeNormalized();
    }
}
