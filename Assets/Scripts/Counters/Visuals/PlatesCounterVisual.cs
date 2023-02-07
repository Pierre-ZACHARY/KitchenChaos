
using System;
using UnityEngine;

public class PlatesCounterVisual: MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private float offset;

    private void Start()
    {
        platesCounter.OnCurrentPlateNumberChanged += OnCurrentPlateNumberChanged;
    }

    private void OnEnable()
    {
        platesCounter.OnCurrentPlateNumberChanged += OnCurrentPlateNumberChanged;
    }

    private void OnDisable()
    {
        platesCounter.OnCurrentPlateNumberChanged -= OnCurrentPlateNumberChanged;
    }

    private void OnCurrentPlateNumberChanged(object sender, PlatesCounter.OnCurrentPlateNumberChangedEventArgs e)
    {
        foreach(Transform child in platesCounter.GetSpawnTarget())
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < e.currentPlates; i++)
        {
            Transform t = Instantiate(platesCounter.platePrefab, platesCounter.GetSpawnTarget());
            t.position = platesCounter.GetSpawnTarget().position + Vector3.up * offset * i;
        }
    }
}
