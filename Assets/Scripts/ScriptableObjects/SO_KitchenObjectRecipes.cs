using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectRecipes", menuName = "ScriptableObjects/KitchenObjectRecipes")]
public class SO_KitchenObjectRecipes : ScriptableObject
{
    public SO_KitchenObjects input;
    public SO_KitchenObjects output;
    public int cutAmountMax;
}
