using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public class OnObjetsOnPlateChangedEventArgs
    {
        public List<KitchenObject> objectsOnPlate;
    }

    private List<KitchenObject> objectsOnPlate = new List<KitchenObject>();
    
    public event EventHandler<OnObjetsOnPlateChangedEventArgs> OnObjetsOnPlateChanged;
    public bool canBePlacedOnPlate(KitchenObject kitchenObject, [CanBeNull] out GameObject objectToActivate)
    {
        Debug.Log("PlateKitchenObject.canBePlacedOnPlate() : " + kitchenObject.associatedKitchenObject.name);

        // check if that object is already on the plate
        foreach (var alreadyOnPlate in objectsOnPlate)
        {
            if (alreadyOnPlate.associatedKitchenObject == kitchenObject.associatedKitchenObject)
            {
                // if so return false : can't be placed on the plate
                objectToActivate = null;
                return false;
            }    
        }
        
        // check if that object is part of the recipe
        foreach (var kitchenObjectOnPlate in availableObjectsOnPlate)
        {
            if (kitchenObjectOnPlate.associatedKitchenObjectSo == kitchenObject.associatedKitchenObject)
            {
                objectToActivate = kitchenObjectOnPlate.transform.gameObject;
                return true;
            }
        }
        objectToActivate = null;
        return false;
    }

    public bool AddObjectOnPlate(KitchenObject kitchenObject)
        {
            if (canBePlacedOnPlate(kitchenObject, out var objectToActivate))
            {
                objectsOnPlate.Add(kitchenObject);
                OnObjetsOnPlateChanged?.Invoke(this, new OnObjetsOnPlateChangedEventArgs {objectsOnPlate = new List<KitchenObject>(objectsOnPlate)});
                objectToActivate!.SetActive(true);
                return true;
            }

            return false;
        }

    public override void SelfDestroy()
        {
            // foreach (var kitchenObject in objectsOnPlate)
            // {
            //     kitchenObject.SelfDestroy();
            // }
            base.SelfDestroy();
        }
    
    public List<KitchenObject> GetObjectsOnPlate()
    {
        return new List<KitchenObject>(objectsOnPlate);
    }

    [Serializable]
    public struct KitchenObjectOnPlate
        {
            public Transform transform;
            public SO_KitchenObjects associatedKitchenObjectSo;
        }
    
    [SerializeField] private List<KitchenObjectOnPlate> availableObjectsOnPlate;
}