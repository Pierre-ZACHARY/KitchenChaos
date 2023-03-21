using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{

    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    [SerializeField] private Color successColor = new Color(0.0f, 0.5f, 0.0f);
    [SerializeField] private Color failedColor = new Color(0.5f, 0.0f, 0.0f);
    [SerializeField] private Animator animator;
    private String showTrigger = "Popup";
    // Start is called before the first frame update
    void Start()
    {
        deliveryManager.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        deliveryManager.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(showTrigger);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        textMesh.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, DeliveryManager.OnRecipeDeliveredEventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(showTrigger);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        textMesh.text = "DELIVERY\nSUCCESS";
    }
}
