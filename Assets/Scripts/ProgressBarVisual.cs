using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarVisual : MonoBehaviour
{
    [CanBeNull]
    private IHasProgress progressOwner;
    [SerializeField] private MonoBehaviour progressOwnerGameObject;
    [SerializeField] private Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        progressOwner = progressOwnerGameObject.GetComponent<IHasProgress>();
        if (progressOwner == null)
            Debug.LogError("No IHasProgress component found on " + progressOwnerGameObject.name);
        else 
            progressOwner.OnProgressChange += ProgressOwner_OnProgressChange;
        SetVisible(false);  
    }

    private void OnEnable()
    {
        progressOwner = progressOwnerGameObject.GetComponent<IHasProgress>();
        if (progressOwner == null)
            Debug.LogError("No IHasProgress component found on " + progressOwnerGameObject.name);
        if(progressOwner!=null) progressOwner.OnProgressChange += ProgressOwner_OnProgressChange;
        SetVisible(false); 
    }

    private void ProgressOwner_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        progressBar.fillAmount = e.normalized_progress;
        SetVisible(e.normalized_progress > 0.0f && e.normalized_progress < 1.0f );
    }

    private void OnDisable()
    {
        if(progressOwner!=null) progressOwner.OnProgressChange -= ProgressOwner_OnProgressChange;
        SetVisible(false);
    }

    private void SetVisible(bool visible)
    {
        progressBar.gameObject.transform.parent.gameObject.SetActive(visible);
    }
}
