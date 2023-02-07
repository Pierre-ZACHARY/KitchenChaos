using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
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
