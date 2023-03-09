using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private String _playerInputJsonKey = "PlayerInput";
    public enum Bindings
    {
        MoveUp,
        Interact,
        MoveDown,
        InteractAlternative,
        MoveLeft,
        Pause,
        MoveRight

    }

    public class InputActionIndex
    {
        public Bindings binding;
        public InputAction action;
        public int index;
        public String displayName;
    }

    public InputActionIndex GetActionIndex(Bindings bindings)
    {
        switch (bindings)
        {
            case Bindings.Interact:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.Interact, index = 0, displayName = "Interact"};
            case Bindings.InteractAlternative:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.InteractAlternative, index = 0, displayName = "Interact Alternative"};
            case Bindings.Pause:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.Pause, index = 0, displayName = "Pause"};
            case Bindings.MoveUp:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.Move, index = 1, displayName = "Move Up"};
            case Bindings.MoveDown:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.Move, index = 2, displayName = "Move Down"};
            case Bindings.MoveLeft:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.Move, index = 3, displayName = "Move Left"};
            case Bindings.MoveRight:
                return new InputActionIndex() {binding = bindings, action = _playerInputActions.Player.Move, index = 4, displayName = "Move Right"};
        }
        return null;
    }

    public void RebindBinding(Bindings bindings, Action onRebound)
    {
        _playerInputActions.Player.Disable();
        InputActionIndex actionIndex = GetActionIndex(bindings);
        actionIndex.action.PerformInteractiveRebinding(actionIndex.index).OnComplete(operation =>
        {
            operation.Dispose();
            _playerInputActions.Player.Enable();
            PlayerPrefs.SetString(_playerInputJsonKey, _playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            onRebound?.Invoke();
            
        }).Start();
    }
    
    
    private PlayerInputActionsClass _playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    
    public event EventHandler OnPauseAction;

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
            if(PlayerPrefs.HasKey(_playerInputJsonKey))
                _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(_playerInputJsonKey));
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += Interact_performed;
            _playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed; 
            _playerInputActions.Player.Pause.performed += Pause_performed;
            
            
        }
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= Interact_performed;
        _playerInputActions.Player.InteractAlternative.performed -= InteractAlternative_performed;
        _playerInputActions.Player.Pause.performed -= Pause_performed;
        _playerInputActions.Dispose();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Interact.performed -= Interact_performed;
        _playerInputActions.Player.InteractAlternative.performed -= InteractAlternative_performed;
        _playerInputActions.Player.Pause.performed -= Pause_performed;
        _playerInputActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInput.Pause_performed");
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternative_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInput.InteractAlternative_performed");
        OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInput.Interact_performed");
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovement()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}
