using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatesCounter : BaseCounter
{
    public class OnCurrentPlateNumberChangedEventArgs
    {
        public int currentPlates;
    }

    [SerializeField] private int maxPlates = 4;
    [SerializeField] private float plateSpawnTime = 5f;
    private float _plateSpawnTimer = 0f;
    private int _currentPlates = 0;
    
    public event EventHandler<OnCurrentPlateNumberChangedEventArgs> OnCurrentPlateNumberChanged;

    private void FixedUpdate()
    {
        if (_currentPlates < maxPlates)
        {
            _plateSpawnTimer += Time.deltaTime;
            if (_plateSpawnTimer >= plateSpawnTime)
            {
                _plateSpawnTimer = _plateSpawnTimer - plateSpawnTime;
                _currentPlates++;
                OnCurrentPlateNumberChanged?.Invoke(this, new OnCurrentPlateNumberChangedEventArgs
                {
                    currentPlates = _currentPlates
                });
            }
        }
    }
    
    public Transform platePrefab;

    public override void Interact(Player player)
    {
        if (_currentPlates > 0)
        {
            if (!player.HasKitchenObject())
            {
                _currentPlates--;
                OnCurrentPlateNumberChanged?.Invoke(this, new OnCurrentPlateNumberChangedEventArgs
                {
                    currentPlates = _currentPlates
                });
                Transform kitchenObjectTransform = Instantiate(platePrefab, _spawnTarget.transform);
                PlateKitchenObject kitchenObject = kitchenObjectTransform.GetComponent<PlateKitchenObject>();
                if(kitchenObject == null)
                    Debug.LogError("PlateKitchenObject is null!");
                else
                    kitchenObject.SetParent(player);
            }
            else
            {
                // test if the player kitchen object can be place on the plate
                if (platePrefab.GetComponent<PlateKitchenObject>().canBePlacedOnPlate(player.GetKitchenObject(), out GameObject objectToActivate))
                {
                    // Object correctly added on plate
                    KitchenObject kitchenObject = player.GetKitchenObject();
                    Transform kitchenObjectTransform = Instantiate(platePrefab, player.GetKitchenObjectSpawnTarget());
                    PlateKitchenObject plate = kitchenObjectTransform.GetComponent<PlateKitchenObject>();
                    plate.AddObjectOnPlate(kitchenObject);
                    Debug.Log(plate);
                    _currentPlates--;
                    OnCurrentPlateNumberChanged?.Invoke(this, new OnCurrentPlateNumberChangedEventArgs
                    {
                        currentPlates = _currentPlates
                    });
                    player.GetKitchenObject().SelfDestroy();
                    plate.SetParent(player);
                }
                else
                    Debug.Log("This object is not part of the recipe!");

            }
        }
    }
    
    public Transform GetSpawnTarget()
    {
        return _spawnTarget;
    }
}
