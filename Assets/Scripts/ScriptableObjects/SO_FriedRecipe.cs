using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FriedRecipe", menuName = "ScriptableObjects/FriedRecipe")]
public class SO_FriedRecipe : ScriptableObject
{
    public SO_KitchenObjects input;
    public SO_KitchenObjects output;
    public float cookingTime;
}
