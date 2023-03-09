using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private DeliveryManager deliveryManager;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject plate = (PlateKitchenObject)player.GetKitchenObject();
                deliveryManager.DeliverRecipe(plate);
            }
        }
    }
}
