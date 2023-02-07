using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUIPanel: MonoBehaviour
{
    private SO_DeliveryRecipe _panelRecipe;
    
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private GridLayoutGroup gridUI;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    
    public void SetRecipe(SO_DeliveryRecipe recipe)
    {
        _panelRecipe = recipe;
        foreach (Transform child in gridUI.transform)
        {
            if(child!=iconTemplate) Destroy(child.gameObject);
        }
        textMeshProUGUI.text = _panelRecipe.recipeName;
        foreach (var item in _panelRecipe.ingredients)
        {
            Transform t = Instantiate(iconTemplate, gridUI.transform);
            t.gameObject.SetActive(true);
            t.GetComponent<Image>().sprite = item.kitchenObjectSprite;
        }
    }
    
}