using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    #region EventBindings
    private void OnDeliveryManagerInstanceChange(object sender, EventArgs e)
    {
        DeliveryManager.Instance.OnRecipeDelivered += OnRecipeDelivered;
        DeliveryManager.Instance.OnDeliveryFailed += OnDeliveryFailed;
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }
    
    private void OnDisable()
    {
        DeliveryManager.OnDeliveryManagerInstanceChange -= OnDeliveryManagerInstanceChange;
        DeliveryManager.Instance.OnRecipeDelivered -= OnRecipeDelivered;
        DeliveryManager.Instance.OnDeliveryFailed -= OnDeliveryFailed;
        CutCounter.OnAnyCut -= OnAnyCut;
        Player.Instance.OnPickupKitchenObject -= OnPickupKitchenObject;
        TrashCounter.OnAnyObjectTrashed -= TrashCounter_OnAnyObjectTrashed;
        BaseCounter.OnKitchenObjectParentChange -= BaseCounter_OnKitchenObjectParentChange;
    }
    
    void Init()
    {
        DeliveryManager.OnDeliveryManagerInstanceChange += OnDeliveryManagerInstanceChange;
        DeliveryManager.Instance.OnRecipeDelivered += OnRecipeDelivered;
        DeliveryManager.Instance.OnDeliveryFailed += OnDeliveryFailed;
        CutCounter.OnAnyCut += OnAnyCut;
        Player.OnInstanceChange += Player_OnInstanceChange;
        if(Player.Instance) Player.Instance.OnPickupKitchenObject += OnPickupKitchenObject;
        BaseCounter.OnKitchenObjectParentChange += BaseCounter_OnKitchenObjectParentChange;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    

    private void Player_OnInstanceChange(object sender, EventArgs e)
    {
        Player.Instance.OnPickupKitchenObject += OnPickupKitchenObject;
    }

    #endregion
    
    [SerializeField] private SO_AudioClipRefs audioClipRefs;
    private void OnDeliveryFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.deliveryFail, DeliveryCounter.Instance ? DeliveryCounter.Instance.transform.position : Camera.main.transform.position);
    }

    private void OnRecipeDelivered(object sender, DeliveryManager.OnRecipeDeliveredEventArgs e)
    {
        PlaySound(audioClipRefs.deliverySuccess, DeliveryCounter.Instance ? DeliveryCounter.Instance.transform.position : Camera.main.transform.position);
    }

    private void OnAnyCut(object sender, EventArgs e)
    {
        CutCounter cutCounter = sender as CutCounter;
        if (cutCounter) PlaySound(audioClipRefs.chop, cutCounter.transform.position);
        else Debug.LogWarning("cutCounter is null!");
    }
    
    private void OnPickupKitchenObject(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.objectPickup, Player.Instance.transform.position);
    }
    
    private void BaseCounter_OnKitchenObjectParentChange(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        if(baseCounter) PlaySound(audioClipRefs.objectDrop, baseCounter.transform.position);
        else Debug.LogWarning("baseCounter is null!");
    }
    
    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        if(trashCounter) PlaySound(audioClipRefs.trash, trashCounter.transform.position);
        else Debug.LogWarning("trashCounter is null!");
    }
}
