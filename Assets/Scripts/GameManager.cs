using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        WaitingToStart,
        CountDownToStart,
        Playing,
        GameOver
    }
    
    public event EventHandler OnStateChange;
    
    [SerializeField] private GameInput gameInput;

    private void Awake()
    {
        gameInput.OnPauseAction += GameInput_OnPauseAction;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (CurrentState == State.WaitingToStart)
        {
            CurrentState = State.CountDownToStart;
        }
    }


    private bool _isPaused = false;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePaused();
    }

    public bool TogglePaused()
    {
        Debug.Log("TogglePaused");
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            OnGamePaused?.Invoke(this, null);
            Time.timeScale = 0f;
        }
        else
        {
            OnGameUnPaused?.Invoke(this, null);
            Time.timeScale = 1f;
        }
        return _isPaused;
    }

    [SerializeField] private float countDownTimer = 5f;
    [SerializeField] private float gameOverTimer = 10f;

    public float GameTimeNormalized()
    {
        switch (CurrentState)
        {
            case State.Playing:
                return _currentStatetimer / gameOverTimer;
            case State.GameOver:
                return 1f;
        }
        return 0f;
    }
    
    private State _currentState = State.WaitingToStart;
    public State CurrentState
    {
        get { return _currentState; }
        private set
        {
            _currentState = value;
            _currentStatetimer = 0f;
            OnStateChange?.Invoke(this, null);
        }
    }
    
    private float _globaltimer;
    private float _currentStatetimer;
    public float CurrentStateTimer => _currentStatetimer;
    
    public float RemainingCountownTime => CurrentState==State.CountDownToStart ? countDownTimer - _currentStatetimer : 0f;
    private void FixedUpdate()
    {
        _globaltimer += Time.deltaTime;
        switch (CurrentState)
        {
            case State.WaitingToStart:
                break;
            case State.CountDownToStart:
                _currentStatetimer += Time.deltaTime;
                if (_currentStatetimer >= countDownTimer)
                    CurrentState = State.Playing;
                break;
            case State.Playing:
                _currentStatetimer += Time.deltaTime;
                if (_currentStatetimer >= gameOverTimer)
                    CurrentState = State.GameOver;
                break;
            case State.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
