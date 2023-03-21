using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuUI : MonoBehaviour
{
    private String musicVolumeKey = "MusicVolume";
    private String soundVolumeKey = "SoundVolume";
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioSource musicManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Button closeButton;
    [SerializeField] private PauseMenuUI pauseMenuUI;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        musicSlider.value = PlayerPrefs.GetFloat(musicVolumeKey, 1);
        soundSlider.value = PlayerPrefs.GetFloat(soundVolumeKey, 1);
        musicManager.volume = musicSlider.value;
        soundManager.globalVolume = soundSlider.value;
        Hide();
    }

    private void OnCloseButtonClicked()
    {
        Hide();
        pauseMenuUI.Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        soundSlider.Select();
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnSoundSliderValueChanged(float arg0)
    {
        soundManager.globalVolume = arg0;
        PlayerPrefs.SetFloat(soundVolumeKey, arg0);
        PlayerPrefs.Save();
    }

    private void OnMusicSliderValueChanged(float arg0)
    {
        musicManager.volume = arg0;
        PlayerPrefs.SetFloat(musicVolumeKey, arg0);
        PlayerPrefs.Save();
    }
}
