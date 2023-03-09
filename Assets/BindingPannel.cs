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
        GameInput.InputActionIndex actionIndex = gameInput.GetActionIndex(bindings);
        label.text = actionIndex.displayName;
        InputControl control = InputSystem.FindControl(actionIndex.action.bindings[actionIndex.index].overridePath ?? actionIndex.action.bindings[actionIndex.index].path);
        buttonText.text = control.displayName ?? actionIndex.action.bindings[actionIndex.index].ToDisplayString();
        button.onClick.AddListener(() =>
        {
            buttonText.text = "...";
            gameInput.RebindBinding(bindings,
                () =>
                {
                    actionIndex = gameInput.GetActionIndex(bindings);
                    control = InputSystem.FindControl(actionIndex.action.bindings[actionIndex.index].overridePath ?? actionIndex.action.bindings[actionIndex.index].path);
                    buttonText.text = control.displayName ?? actionIndex.action.bindings[actionIndex.index].ToDisplayString();

                });
        });
    }
}
