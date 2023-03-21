using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private StoveCounterSound stoveCounterSound;
    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
        Hide();
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        if (stoveCounter.IsAboutToBurn())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void OnWarningAppear()
    {
        stoveCounterSound.PlayWarningSound();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
