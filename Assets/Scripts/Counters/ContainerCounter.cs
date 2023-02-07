using System;
using UnityEngine;

class ContainerCounter : BaseCounter
{
    [SerializeField] private SO_KitchenObjects _kitchenObjectSO;
    
    public event EventHandler OnOpenContainer;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            OnOpenContainer?.Invoke(this, EventArgs.Empty);
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.kitchenObjectPrefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetParent(player);  
        }
        else
        {
            if (player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject plate  = (PlateKitchenObject)player.GetKitchenObject();
                if(plate.AddObjectOnPlate(_kitchenObjectSO.kitchenObjectPrefab.GetComponent<KitchenObject>()))
                {
                    // Object correctly added on plate
                    Debug.Log("Correctly added on plate!");
                }
                else
                    Debug.Log("This object is not part of the recipe!");
            }
        }
    }
}