using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public class OnStateChangeEventArgs
    {
        public State new_state;
    }

    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    public enum State
    {
        Idle, Cooking, Done
    }

    public bool IsAboutToBurn()
    {
        if(currentRecipe.hasWarning)
        {
            return _cookingTimer > currentRecipe.cookingTime*0.3f;
        }
        else return false;
    }
    
    
    private State _state;
    private void SetState(State state)
    {
        _state = state;
        switch (_state)
        {
            case State.Idle:
                SetTimer(0.0f);
                break;
            case State.Cooking:
                currentRecipe = GetRecipeFromInput(GetKitchenObject());
                break;
            case State.Done:
                SetTimer(0.0f);
                break;
        }
        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
        {
            new_state = state
        });
    }
    
    [SerializeField] private SO_FriedRecipe[] recipes;
    
    private SO_FriedRecipe currentRecipe;
    
    private float _cookingTimer;

    public void SetTimer(float timer)
    {
        _cookingTimer = timer;
        if (currentRecipe)
        {
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                normalized_progress = _cookingTimer / currentRecipe.cookingTime
            });
        }
        
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Idle:
                if(HasKitchenObject() && HasRecipe(GetKitchenObject())) SetState(State.Cooking);
                break;
            case State.Cooking:
                if(!currentRecipe) SetState(State.Done);
                else
                {
                    SetTimer(_cookingTimer + Time.fixedDeltaTime);
                    
                    if (_cookingTimer >= currentRecipe.cookingTime)
                    {
                        GetKitchenObject().SelfDestroy();
                        Transform transform = Instantiate(currentRecipe.output.kitchenObjectPrefab, _spawnTarget.transform);
                        KitchenObject kitchenObject = transform.GetComponent<KitchenObject>();
                        kitchenObject.SetParent(this);
                        SetState(State.Idle);
                    }
                }
                break;
            case State.Done:
                break;
        }
        
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if(!player.HasKitchenObject())
            {
                SetState(State.Idle);
                GetKitchenObject().SetParent(player);
            }
            else
            {
                
                if (player.GetKitchenObject() is PlateKitchenObject)
                {
                    PlateKitchenObject plate = (PlateKitchenObject)player.GetKitchenObject();
                    if (plate.AddObjectOnPlate(GetKitchenObject()))
                    {
                        // Object correctly added on plate
                        SetState(State.Idle);
                        GetKitchenObject().SelfDestroy();
                    }
                    else
                        Debug.Log("This object is not part of the recipe!");
                }
                else
                    Debug.Log("Player already has a kitchen object!");
                
            }
        }
        else
        {
            if(player.HasKitchenObject() && HasRecipe(player.GetKitchenObject()))
            {
                SetState(State.Idle);
                player.GetKitchenObject().SetParent(this);
            }
            else
                Debug.Log("Player doesn't have a kitchen object to put on the counter, or that kitchen object isn't cookable!");
        }
    }
    
    private bool HasRecipe(KitchenObject kitchenObject)
    {
        return GetRecipeFromInput(kitchenObject) != null;
    }

    [CanBeNull]
    private SO_FriedRecipe GetRecipeFromInput(KitchenObject kitchenObject)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.input == kitchenObject.associatedKitchenObject)
                return recipe;
        }
        return null;
    }
}
