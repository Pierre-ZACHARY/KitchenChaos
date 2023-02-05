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
                Debug.Log("Player already has a kitchen object!");
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