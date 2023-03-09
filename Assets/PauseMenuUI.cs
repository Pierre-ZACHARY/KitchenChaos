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
    }

    private void OnGameUnPaused(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void OnGamePaused(object sender, EventArgs e)
    {
        Debug.Log("Game Paused");
        gameObject.SetActive(true);
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
