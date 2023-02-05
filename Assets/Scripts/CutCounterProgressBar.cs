using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CutCounter))]
public class CutCounterProgressBar : MonoBehaviour
{
    private CutCounter cutCounter;
    [SerializeField] private Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        cutCounter = GetComponent<CutCounter>();
        cutCounter.OnCutAmountChange += CutCounter_OnCutAmountChange;
        SetVisible(false); 
    }

    private void OnEnable()
    {
        cutCounter.OnCutAmountChange += CutCounter_OnCutAmountChange;
    }
    
    private void OnDisable()
    {
        cutCounter.OnCutAmountChange -= CutCounter_OnCutAmountChange;
    }
    

    private void CutCounter_OnCutAmountChange(object sender, CutCounter.CutAmountEventArgs e)
    {
        progressBar.fillAmount = (float) e.CutAmount/ (float) e.CutAmountMax;
        SetVisible(cutCounter.HasKitchenObject() && e.CutAmount < e.CutAmountMax );
    }
    
    private void SetVisible(bool visible)
    {
        progressBar.gameObject.transform.parent.gameObject.SetActive(visible);
    }
}
