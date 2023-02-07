
using UnityEngine;

[CreateAssetMenu(fileName = "DeliveryRecipe", menuName = "ScriptableObjects/DeliveryRecipe", order = 1)]
public class SO_DeliveryRecipe : ScriptableObject
{
    public SO_KitchenObjects[] ingredients;
    public string recipeName;
}
