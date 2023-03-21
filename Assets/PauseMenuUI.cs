using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionButton;

    [SerializeField] private SceneReference mainMenuScene;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private OptionMenuUI optionMenuUI;

    private void Start()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        optionButton.onClick.AddListener(OnOptionButtonClicked);
        gameManager.OnGamePaused += OnGamePaused;
        gameManager.OnGameUnPaused += OnGameUnPaused;
        gameObject.SetActive(false);
    }

    private void OnOptionButtonClicked()
    {
        optionMenuUI.Show();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void OnGamePaused(object sender, EventArgs e)
    {
        Debug.Log("Game Paused");
        Show();
    }

    private void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(mainMenuScene);
        gameManager.TogglePaused();
    }

    private void OnResumeButtonClicked()
    {
        gameManager.TogglePaused();
    }
}
