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
    
    private static GameManager _instance;
    
    public static event EventHandler OnStateChange;
    public static event EventHandler OnInstanceChange;
    
    [ExecuteOnReload] private static void CleanUpEvents() 
    {
        OnStateChange = null;
        OnInstanceChange = null;
    }
    public static GameManager Instance
    {
        get { return _instance; }
        private set
        {
            _instance = value;
            OnInstanceChange?.Invoke(null, null);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Instance = this;
    }
    
    [SerializeField] private float waitingToStartTimer = 3f;
    [SerializeField] private float countDownTimer = 5f;
    [SerializeField] private float gameOverTimer = 10f;
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
                _currentStatetimer += Time.deltaTime;
                if (_currentStatetimer >= waitingToStartTimer)
                    CurrentState = State.CountDownToStart;
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
