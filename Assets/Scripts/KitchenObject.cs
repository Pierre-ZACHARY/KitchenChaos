using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    public SO_KitchenObjects associatedKitchenObject;

    private IKitchenObjectParent _parent;
    
    public void SetParent(IKitchenObjectParent parent)
    {
        if(_parent != null)
            _parent.ClearKitchenObject();
        if(parent.HasKitchenObject() && parent.GetKitchenObject() != this)
            Debug.LogError("There is already a kitchen object on this counter!");
        transform.position = parent.GetKitchenObjectSpawnTarget().position;
        transform.rotation = parent.GetKitchenObjectSpawnTarget().rotation;
        transform.parent = parent.GetKitchenObjectSpawnTarget();
        parent.SetKitchenObject(this); 
        _parent = parent; 
    }
    
    public IKitchenObjectParent GetParent()
    {
        return _parent;
    }

    public void SelfDestroy()
    {
        this._parent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
