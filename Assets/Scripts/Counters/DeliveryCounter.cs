using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    
    private static DeliveryCounter _instance;
    
    public static event EventHandler OnDeliveryCounterInstanceChange;
    public static DeliveryCounter Instance
    {
        get { return _instance; }
        set
        {
            _instance = value;
            OnDeliveryCounterInstanceChange?.Invoke(null, null);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    
    private void OnEnable()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject plate = (PlateKitchenObject)player.GetKitchenObject();
                DeliveryManager.Instance.DeliverRecipe(plate);
            }
        }
    }
}
