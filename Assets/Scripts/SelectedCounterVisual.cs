using System;
using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{
    private bool _selected = false;
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private BaseCounter baseCounter;
    private void Start()
    {
        SetSelected(false);
        Player.Instance.OnLastSelectedCounterChange += Instance_OnLastSelectedCounterChange;
    }

    private void Instance_OnLastSelectedCounterChange(object sender, Player.OnLastSelectedCounterChangeArgs e)
    {
        SetSelected(e.selectedCounter == baseCounter);
    }

    public void SetSelected(Boolean selected)
    {
        this._selected = selected;
        selectedIndicator.SetActive(selected);
    }
}