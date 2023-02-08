using System;
using UnityEngine;

public class StoveCounterSound: MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void OnEnable()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void OnDisable()
    {
        stoveCounter.OnStateChange -= StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if (e.new_state == StoveCounter.State.Cooking)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}