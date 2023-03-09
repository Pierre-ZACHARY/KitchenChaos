using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private SceneReference gameScene;

    [SerializeField] private GameObject loadingScreen;
    
    private AsyncOperation _asyncOperation;
    private void Awake()
    {
        loadingScreen.SetActive(false);
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        _asyncOperation = SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Single);
        _asyncOperation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        _asyncOperation.completed += OnSceneLoaded;
        
    }

    private void Update()
    {
        if(_asyncOperation == null) return;
        Debug.Log(_asyncOperation.progress);
        if (_asyncOperation.progress >= 0.9f)
        {
            _asyncOperation.allowSceneActivation = true;
        }
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(gameScene));
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
