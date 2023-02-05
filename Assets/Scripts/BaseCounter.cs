using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    #region KitchenObjectLogic
    [SerializeField] protected Transform _spawnTarget;
    private KitchenObject _kitchenObject;
    public abstract void Interact(Player player);

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
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