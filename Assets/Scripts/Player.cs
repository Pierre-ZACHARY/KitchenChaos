using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Transform))]
public class Player : MonoBehaviour, IKitchenObjectParent
{
    //Singleton
    public static Player Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

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

    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Debug.Log("Interact : " + lastSelectedCounter);
        lastSelectedCounter?.Interact(this);
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
        transform.forward = Vector3.Slerp(transform.forward, mooveDirection, Time.deltaTime * mooveSpeed);

        float playerHeight = 2f;
        float playerSize = 0.5f;
        var transform1 = transform;
        var position = transform1.position;
        bool canMove = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerSize, mooveDirection,
            Time.deltaTime * mooveSpeed);
        if (!canMove)
        {
            // Attempt to moove only on the X axis
            Vector3 moveDirX = new Vector3(mooveDirection.x, 0, 0).normalized;
            canMove = moveDirX!=Vector3.zero && !Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerSize, moveDirX,
                Time.deltaTime * mooveSpeed);
            if (canMove) mooveDirection = moveDirX;
            else
            {
                // Attempt to moove only on the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, mooveDirection.z).normalized;
                canMove = moveDirZ!=Vector3.zero && !Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerSize, moveDirZ,
                    Time.deltaTime * mooveSpeed);
                if (canMove) mooveDirection = moveDirZ;
            }
        }

        transform.position += mooveDirection * (Time.deltaTime * mooveSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    #region KitchenObjectLogic

    [SerializeField] private Transform _kitchenObjectSpawnTarget;
    private KitchenObject _kitchenObject;

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
       _kitchenObject = kitchenObject;
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