using System;
using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{
    private bool _selected = false;
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private BaseCounter baseCounter;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetSelected(false);
        if(Player.Instance) Player.Instance.OnLastSelectedCounterChange += Instance_OnLastSelectedCounterChange;
        Player.OnInstanceChange += Player_OnInstanceChange;
    }
    private void OnEnable() 
    {
        Init();
    }

    private void Player_OnInstanceChange(object sender, EventArgs e)
    {
        Player.Instance.OnLastSelectedCounterChange += Instance_OnLastSelectedCounterChange;
    }

    private void OnDisable()
    {
        Player.Instance.OnLastSelectedCounterChange -= Instance_OnLastSelectedCounterChange;
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