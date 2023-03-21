using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI forwardText;
    [SerializeField] private TextMeshProUGUI leftStickText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI downText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactTextGamePad;
    [SerializeField] private TextMeshProUGUI interactAlternativeText;
    [SerializeField] private TextMeshProUGUI interactAlternativeTextGamepad;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI pauseTextGamepad;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameManager gameManager;
    
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateText();
        gameManager.OnStateChange += GameManager_OnStateChange;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnKeyRebound += GameInput_OnKeyRebound;
    }

    private void GameInput_OnKeyRebound(object sender, EventArgs e)
    {
        UpdateText();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if(gameManager.CurrentState == GameManager.State.WaitingToStart)
            Show();
        else
            Hide();
    }

    private void UpdateText()
    {
        forwardText.text = gameInput.GetKeyBinding(GameInput.Bindings.MoveUp);
        leftStickText.text = "Left Stick";
        leftText.text = gameInput.GetKeyBinding(GameInput.Bindings.MoveLeft);
        rightText.text = gameInput.GetKeyBinding(GameInput.Bindings.MoveRight);
        downText.text = gameInput.GetKeyBinding(GameInput.Bindings.MoveDown);
        interactText.text = gameInput.GetKeyBinding(GameInput.Bindings.Interact);
        interactTextGamePad.text = gameInput.GetKeyBinding(GameInput.Bindings.Gamepad_Interact);
        interactAlternativeText.text = gameInput.GetKeyBinding(GameInput.Bindings.InteractAlternative);
        interactAlternativeTextGamepad.text = gameInput.GetKeyBinding(GameInput.Bindings.Gamepad_InteractAlternative);
        pauseText.text = gameInput.GetKeyBinding(GameInput.Bindings.Pause);
        pauseTextGamepad.text = gameInput.GetKeyBinding(GameInput.Bindings.Gamepad_Pause);
    }
}
