using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ContainerCounterAnimator: MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;
    private string OPEN_CLOSE = "OpenClose";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnOpenContainer += ContainerCounter_OnOpenContainer;
    }

    private void OnEnable()
    {
        containerCounter.OnOpenContainer += ContainerCounter_OnOpenContainer;
    }

    private void OnDisable()
    {
        containerCounter.OnOpenContainer -= ContainerCounter_OnOpenContainer;
    }

    private void ContainerCounter_OnOpenContainer(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}