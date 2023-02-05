using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CutCounterAnimator: MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CutCounter cutCounter;
    private string CUT = "Cut";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        cutCounter.OnCut += CutCounter_OnCut;
    }

    private void OnEnable()
    {
        cutCounter.OnCut += CutCounter_OnCut;
    }

    private void OnDisable()
    {
        cutCounter.OnCut -= CutCounter_OnCut;
    }

    private void CutCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
} 
