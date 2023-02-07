using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUIGrid : MonoBehaviour
{
    #region EventSubscriptions
    private void Start()
    {
        DeliveryManager.OnDeliveryManagerInstanceChange += DeliveryManager_OnDeliveryManagerInstanceChange;
        DeliveryManager.Instance.OnRecipesChanged += DeliveryManager_OnRecipesChanged;
    }

    private void OnEnable()
    {
        DeliveryManager.OnDeliveryManagerInstanceChange += DeliveryManager_OnDeliveryManagerInstanceChange;
        if(DeliveryManager.Instance) DeliveryManager.Instance.OnRecipesChanged += DeliveryManager_OnRecipesChanged;
    }

    private void OnDisable()
    {
        DeliveryManager.OnDeliveryManagerInstanceChange -= DeliveryManager_OnDeliveryManagerInstanceChange;
        DeliveryManager.Instance.OnRecipesChanged -= DeliveryManager_OnRecipesChanged;
    }

    

    private void DeliveryManager_OnDeliveryManagerInstanceChange(object sender, EventArgs e)
    {
        // new instance of DeliveryManager
        DeliveryManager.Instance.OnRecipesChanged += DeliveryManager_OnRecipesChanged;
    }
    
    #endregion

    [SerializeField] private Transform panelTemplate;
    [SerializeField] private GridLayoutGroup gridUI;
    private void DeliveryManager_OnRecipesChanged(object sender, DeliveryManager.OnRecipesChangedEventArgs e)
    {
        foreach (Transform child in gridUI.transform)
        {
           if(child!=panelTemplate) Destroy(child.gameObject);
        }
        foreach (var recipe in e.Recipes)
        {
            Transform t = Instantiate(panelTemplate, gridUI.transform);
            t.gameObject.SetActive(true);
            t.GetComponent<DeliveryManagerUIPanel>().SetRecipe(recipe);
        }
    }
}