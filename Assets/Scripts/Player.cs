using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Transform))]
public class Player : MonoBehaviour, IKitchenObjectParent
{
    public class OnLastSelectedCounterChangeArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public event EventHandler<OnLastSelectedCounterChangeArgs> OnLastSelectedCounterChange;

    [SerializeField] private float mooveSpeed = 7f;
    private bool isWalking = false;
    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractionDirection;
    [SerializeField] private LayerMask counterLayerMask;
    private BaseCounter lastSelectedCounter;

    public void SetSelectedCounter(BaseCounter counter)
    {
        lastSelectedCounter = counter;
        OnLastSelectedCounterChange?.Invoke(this, new OnLastSelectedCounterChangeArgs() { selectedCounter = counter });
    }

    private void GameInput_OnInteractAlternativeAction(object sender, EventArgs e)
    {
        if(gameManager.CurrentState != GameManager.State.Playing) return;
        Debug.Log("Interact Alternative : " + lastSelectedCounter);
        lastSelectedCounter?.InteractAlternative(this);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(gameManager.CurrentState != GameManager.State.Playing) return;
        Debug.Log("Interact : " + lastSelectedCounter);
        lastSelectedCounter?.Interact(this);
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mooveDirection = gameInput.GetMovement();
        Vector3 mooveDirection3D = new Vector3(mooveDirection.x, 0, mooveDirection.y);

        if (mooveDirection3D != Vector3.zero)
        {
            HandleMovement(mooveDirection3D);
            HandleInteractions(mooveDirection3D);
        }

        isWalking = mooveDirection3D != Vector3.zero;
    }

    private void HandleInteractions(Vector3 mooveDirection)
    {
        if (mooveDirection == Vector3.zero) lastInteractionDirection = mooveDirection;
        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, mooveDirection, out RaycastHit hit, interactionDistance,
                counterLayerMask))
        {
            SetSelectedCounter(hit.collider.GetComponent<BaseCounter>());
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement(Vector3 mooveDirection)
    {
        var transform1 = transform;
        transform.forward = Vector3.Slerp(transform1.forward, mooveDirection, Time.deltaTime * mooveSpeed);

        float playerHeight = 2f;
        float playerSize = 0.5f;
        var position = transform1.position;
        bool canMove = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerSize, mooveDirection,
            Time.deltaTime * mooveSpeed);
        if (!canMove)
        {
            // Attempt to moove only on the X axis
            Vector3 moveDirX = new Vector3(mooveDirection.x, 0, 0).normalized;
            canMove = moveDirX.x != 0 &&!Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerSize, moveDirX,
                Time.deltaTime * mooveSpeed);
            if (canMove) mooveDirection = moveDirX;
            else
            {
                // Attempt to moove only on the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, mooveDirection.z).normalized;
                canMove = moveDirZ.z != 0 && !Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerSize, moveDirZ,
                    Time.deltaTime * mooveSpeed);
                if (canMove) mooveDirection = moveDirZ;
            }
        }
        
        if(canMove) transform.position += mooveDirection * (Time.deltaTime * mooveSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    #region KitchenObjectLogic

    [SerializeField] private Transform _kitchenObjectSpawnTarget;
    private KitchenObject _kitchenObject;
    [SerializeField] private GameManager gameManager;

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    
    public event EventHandler OnPickupKitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
       _kitchenObject = kitchenObject;
       if(kitchenObject != null) OnPickupKitchenObject?.Invoke(this, EventArgs.Empty);
    }

    public bool HasKitchenObject()
    {
        return GetKitchenObject() != null;
    }

    public void ClearKitchenObject()
    {
        SetKitchenObject(null);
    }

    public Transform GetKitchenObjectSpawnTarget()
    {
        return _kitchenObjectSpawnTarget;
    }

    #endregion
}