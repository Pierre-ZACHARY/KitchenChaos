using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjects", menuName = "ScriptableObjects/KitchenObjects", order = 0)]
public class SO_KitchenObjects : ScriptableObject
{
    public Transform kitchenObjectPrefab;
    public string kitchenObjectName;
    public Sprite kitchenObjectSprite;
}
