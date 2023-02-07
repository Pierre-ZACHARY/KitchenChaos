﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DeliveryManager: MonoBehaviour
{
    public class OnRecipeDeliveredEventArgs
    {
        public SO_DeliveryRecipe Recipe;
    }

    public class OnRecipesChangedEventArgs
    {
        public List<SO_DeliveryRecipe> Recipes;
    }

    private static DeliveryManager _instance;
    public static DeliveryManager Instance
    {
        get { return _instance; }
        set
        {
            _instance = value;
            OnDeliveryManagerInstanceChange?.Invoke(null, null);
        }
    }
    
    public static event EventHandler OnDeliveryManagerInstanceChange;
    
    [ExecuteOnReload]
    private static void OnReload()
    {
        OnDeliveryManagerInstanceChange = null;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Instance = this;
    }
    
    [SerializeField] private float newRecipeMaxTime  = 8.0f;
    [SerializeField] private int maxRecipes = 3;
    private float _newRecipeTimer = 0f;
    [SerializeField] private SO_DeliveryRecipesList recipesList;
    private List<SO_DeliveryRecipe> _currentRecipes = new List<SO_DeliveryRecipe>();
    public event EventHandler<OnRecipesChangedEventArgs> OnRecipesChanged;
    public event EventHandler<OnRecipeDeliveredEventArgs> OnRecipeDelivered; 

    private void FixedUpdate()
    {
        if(_currentRecipes.Count >= maxRecipes) return;
        _newRecipeTimer += Time.deltaTime;
        if (_newRecipeTimer >= newRecipeMaxTime)
        {
            _newRecipeTimer -= newRecipeMaxTime;
            _currentRecipes.Add(recipesList.Recipes[UnityEngine.Random.Range(0, recipesList.Recipes.Length)]);
            OnRecipesChanged?.Invoke(this, new OnRecipesChangedEventArgs
            {
                Recipes = new List<SO_DeliveryRecipe>(_currentRecipes)
            });
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        if (plateKitchenObject == null) return false;
        // verify if all KitchenObject on the plate are part of one of the current recipes
        foreach (var recipe in _currentRecipes)
        {
            // for this recipe, check if all KitchenObject on the plate are part of it
            var allKitchenObjectOnPlateArePartOfThisRecipe = true;
            foreach (var kitchenObjectOnPlate in plateKitchenObject.GetObjectsOnPlate())
            {
                var kitchenObjectOnPlateIsPartOfThisRecipe = false;
                foreach (var kitchenObjectInRecipe in recipe.ingredients)
                {
                    if (kitchenObjectOnPlate.associatedKitchenObject == kitchenObjectInRecipe)
                    {
                        kitchenObjectOnPlateIsPartOfThisRecipe = true;
                        break;
                    }
                }
                if (!kitchenObjectOnPlateIsPartOfThisRecipe)
                {
                    allKitchenObjectOnPlateArePartOfThisRecipe = false;
                    break;
                }
            }
            if(allKitchenObjectOnPlateArePartOfThisRecipe)
            {
                // there is one recipes that match the plate
                // remove this recipe from the list
                _currentRecipes.Remove(recipe);
                OnRecipesChanged?.Invoke(this, new OnRecipesChangedEventArgs
                {
                    Recipes = new List<SO_DeliveryRecipe>(_currentRecipes)
                });
                // destroy the plate
                plateKitchenObject.SelfDestroy();
                OnRecipeDelivered?.Invoke(this, new OnRecipeDeliveredEventArgs
                {
                    Recipe = recipe
                });
                return true;
            }
        }
        return false;
    }
}