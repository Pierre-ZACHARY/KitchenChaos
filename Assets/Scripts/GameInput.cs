using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActionsClass _playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }
    
    private void Init() 
    {
        Debug.Log("GameInput.Init : "+_playerInputActions);  
        if (_playerInputActions == null)
        {
            _playerInputActions = new PlayerInputActionsClass();
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += Interact_performed;
            _playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed; 
        }
    }

    private void InteractAlternative_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovement()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}
