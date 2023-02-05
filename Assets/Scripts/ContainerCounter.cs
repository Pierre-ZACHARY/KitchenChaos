using UnityEngine;

class ContainerCounter : BaseCounter
{
    [SerializeField] private SO_KitchenObjects _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.kitchenObjectPrefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetParent(player);  
        }
    }
}