using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    
    public float globalVolume = 1.0f;
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * globalVolume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume * globalVolume);
    }
    
    #region EventBindings

    [SerializeField] private Player player;
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private DeliveryCounter deliveryCounter;

    private void Start()
    {
        deliveryManager.OnRecipeDelivered -= OnRecipeDelivered;
        deliveryManager.OnDeliveryFailed -= OnDeliveryFailed;
        player.OnPickupKitchenObject -= OnPickupKitchenObject;
    }

    private void OnEnable()
    {
        CutCounter.OnAnyCut += OnAnyCut;
        BaseCounter.OnKitchenObjectParentChange += BaseCounter_OnKitchenObjectParentChange;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void OnDisable()
    {
        CutCounter.OnAnyCut -= OnAnyCut;
        TrashCounter.OnAnyObjectTrashed -= TrashCounter_OnAnyObjectTrashed;
        BaseCounter.OnKitchenObjectParentChange -= BaseCounter_OnKitchenObjectParentChange;
    }
    
    #endregion
    
    [SerializeField] private SO_AudioClipRefs audioClipRefs;
    private void OnDeliveryFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.deliveryFail, deliveryCounter.transform.position);
    }

    private void OnRecipeDelivered(object sender, DeliveryManager.OnRecipeDeliveredEventArgs e)
    {
        PlaySound(audioClipRefs.deliverySuccess, deliveryCounter.transform.position);
    }

    private void OnAnyCut(object sender, EventArgs e)
    {
        Debug.Log("OnAnyCut");
        CutCounter cutCounter = sender as CutCounter;
        if (cutCounter) PlaySound(audioClipRefs.chop, cutCounter.transform.position);
        else Debug.LogWarning("cutCounter is null!");
    }
    
    private void OnPickupKitchenObject(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.objectPickup, player.transform.position);
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
