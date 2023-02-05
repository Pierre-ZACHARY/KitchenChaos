using System;
using JetBrains.Annotations;
using UnityEngine;

public class CutCounter: BaseCounter
{
    public class CutAmountEventArgs
    {
        public int CutAmount;
        public int CutAmountMax; 
    }

    [SerializeField] private SO_KitchenObjectRecipes[] recipes;
    public event EventHandler<CutAmountEventArgs> OnCutAmountChange;
    public event EventHandler OnCut; 

    private int _cutAmount;
    public void SetCutAmount(int cutAmount) 
    {
        OnCutAmountChange?.Invoke(this, new CutAmountEventArgs() {CutAmount = cutAmount, CutAmountMax = GetRecipeFromInput(GetKitchenObject())?.cutAmountMax ?? 0});
        _cutAmount = cutAmount;
    }
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if(!player.HasKitchenObject())
            {
                SetCutAmount(0);
                GetKitchenObject().SetParent(player);
            }
            else
                Debug.Log("Player already has a kitchen object!");
        }
        else
        {
            if(player.HasKitchenObject())
            {
                if(HasRecipe(player.GetKitchenObject()))
                {
                    player.GetKitchenObject().SetParent(this);
                    SetCutAmount(0);
                }
                else
                {
                    Debug.Log("This object can't be cut!");
                }
            }
            else
                Debug.Log("Player doesn't have a kitchen object to put on the counter!");
        }
    }

    public override void InteractAlternative(Player player)
    {
        if (HasKitchenObject() && HasRecipe(GetKitchenObject()))
        {
            SetCutAmount(_cutAmount + 1);
            OnCut?.Invoke(this, EventArgs.Empty);
            Debug.Log(GetKitchenObject()); 
            Debug.Log(GetRecipeFromInput(GetKitchenObject()));
            Debug.Log(_cutAmount >= GetRecipeFromInput(GetKitchenObject())!.cutAmountMax);
            // print cutamount max from recipe
            if (_cutAmount >= GetRecipeFromInput(GetKitchenObject())!.cutAmountMax)
            {
                Transform kitchenObjectTransform =
                    Instantiate(GetRecipeFromInput(GetKitchenObject())!.output.kitchenObjectPrefab, transform);
                GetKitchenObject().SelfDestroy();
                KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
                kitchenObject.SetParent(this);
                SetCutAmount(0);
            }
        }
    }

    [CanBeNull]
    public SO_KitchenObjectRecipes GetRecipeFromInput(KitchenObject input)
    {
        if (input)
        {
            foreach (SO_KitchenObjectRecipes recipe in recipes)
            {
                if (recipe.input == input.associatedKitchenObject) return recipe;
            }
        }
        else
        {
            Debug.LogError("Input is null!");
        }
        
        return null;
    }

    public bool HasRecipe(KitchenObject input)
    {
        return GetRecipeFromInput(input) != null;
    }
    
}