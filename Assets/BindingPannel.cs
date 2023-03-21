using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

public class BindingPannel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private GameInput gameInput;

    public void Init(GameInput.Bindings bindings)
    {
        buttonText.text = gameInput.GetKeyBinding(bindings);
        label.text = gameInput.GetActionIndex(bindings).displayName;
        button.onClick.AddListener(() =>
        {
            buttonText.text = "...";
            gameInput.RebindBinding(bindings,
                () =>
                {
                    buttonText.text = gameInput.GetKeyBinding(bindings);
                });
        });
    }
}
