using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    #region KitchenObjectLogic
    [SerializeField] protected Transform _spawnTarget;
    private KitchenObject _kitchenObject;
    public abstract void Interact(Player player);

    public virtual void InteractAlternative(Player player){}

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject; 
    }

    private void OnEnable()
    {
        if(_kitchenObject) _kitchenObject.SetParent(this);
    }
    
    public static event EventHandler OnKitchenObjectParentChange;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
        if(_kitchenObject) OnKitchenObjectParentChange?.Invoke(this, null);
    }

    public bool HasKitchenObject()
    {
        return GetKitchenObject() != null;
    }

    public void ClearKitchenObject()
    {
        SetKitchenObject(null);
    }

    public Transform GetKitchenObjectSpawnTarget()
    {
        return _spawnTarget;
    }

    #endregion
}