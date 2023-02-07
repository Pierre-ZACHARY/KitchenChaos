using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if(!player.HasKitchenObject())
                GetKitchenObject().SetParent(player);
            else
            {
                PlateKitchenObject plate = null;
                KitchenObject kitchenObject = null;
                if (player.GetKitchenObject() is PlateKitchenObject)
                {
                    plate = (PlateKitchenObject)player.GetKitchenObject();
                    kitchenObject = GetKitchenObject();
                }
                else if(GetKitchenObject() is PlateKitchenObject)
                {
                    plate = (PlateKitchenObject)GetKitchenObject();
                    kitchenObject = player.GetKitchenObject();
                }
                else
                {
                    Debug.Log("Player and counter don't have a plate!");
                    return;
                }
                if (plate && kitchenObject && plate.AddObjectOnPlate(kitchenObject))
                {
                    // Object correctly added on plate
                    kitchenObject.SelfDestroy();
                }
                else
                    Debug.Log("This object is not part of the recipe!");
            }
        }
        else
        {
            if(player.HasKitchenObject())
                player.GetKitchenObject().SetParent(this);
            else
                Debug.Log("Player doesn't have a kitchen object to put on the counter!");
        }
    }
}