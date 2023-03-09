using System;
using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{
    private bool _selected = false;
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private Player player;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetSelected(false);
        if(player) player.OnLastSelectedCounterChange += Instance_OnLastSelectedCounterChange;
    }
    private void OnEnable() 
    {
        Init();
    }

    private void Player_OnInstanceChange(object sender, EventArgs e)
    {
        player.OnLastSelectedCounterChange += Instance_OnLastSelectedCounterChange;
    }

    private void OnDestroy()
    {
        player.OnLastSelectedCounterChange -= Instance_OnLastSelectedCounterChange;
    }

    private void OnDisable()
    {
        player.OnLastSelectedCounterChange -= Instance_OnLastSelectedCounterChange;
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