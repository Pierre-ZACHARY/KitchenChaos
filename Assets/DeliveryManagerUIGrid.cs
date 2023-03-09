using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUIGrid : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    private void Start()
    {
        deliveryManager.OnRecipesChanged += DeliveryManager_OnRecipesChanged;
    }


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