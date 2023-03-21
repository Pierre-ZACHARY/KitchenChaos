using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveProgressBarFlashing : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    [SerializeField] private StoveCounter stoveCounter;
    private String flashingTrigger = "IsFlashing";
    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        if (stoveCounter.IsAboutToBurn())
        {
            animator.SetBool(flashingTrigger, true);
        }
        else
        {
            animator.SetBool(flashingTrigger, false);
        }
    }
}
