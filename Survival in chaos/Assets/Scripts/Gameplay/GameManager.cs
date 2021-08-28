using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ThrowObject _thorwSystem;
    [SerializeField] private WaveSystem _waveSystemUI;
    [SerializeField] private float _waitTimeToShowNoteText = 1f;
    [SerializeField] private float _waitTimeToShowNumberText = 1f;
    [SerializeField] private float _initialTimeForEachWave = 15f;
    [SerializeField] private float _extraTimeForEachWave = 5f;
    [SerializeField] private float _throwBreakTimeReducePerWave = 0.2f;
    private int _waveNumber = 0;
    private bool _newWave = true;
    private bool _waveBreak = true;

    private bool _canCountDown = false;
    private float _timeForEachWave;

    public enum GameState
    {
        GameStart,
        WaveRunning,
        Wavebreak
    }

    public GameState gameState{get; private set;}

    public static event Action<bool> OnWaveStart;
    void Awake()
    {
        gameState = GameState.GameStart;
        _timeForEachWave = _initialTimeForEachWave;
    }
    void ChangeState(GameState state)
    {
       gameState = state;
    }

    
    void Update()
    {
        CheckState();
    }

    void CountDown()
    {
       if(_canCountDown)
        {
           if(_timeForEachWave <= 0)
           {
                _timeForEachWave = 0;
                ChangeState(GameState.Wavebreak);
                _waveBreak = true;
                _canCountDown = false;
           }

           else
           {
                _timeForEachWave -= Time.deltaTime;
           }

           _waveSystemUI.ChangeTimerText(_timeForEachWave.ToString("0") + " Sec");
            
        }
    }

    void CheckState()
    {
        
        switch (gameState)
        {
            
           case GameState.GameStart:
                StartCoroutine(WaitToShowNoteText(_waitTimeToShowNoteText, "Ready?"));
               
           break;

           case GameState.WaveRunning:
                if(_newWave)
                {
                    _waveNumber++;
                    StartCoroutine(WaitToShowNumberText("Wave " + _waveNumber.ToString() + " start"));
                    _newWave = false;
                    _timeForEachWave = _initialTimeForEachWave;
                }

                StartCoroutine(StartCountDown());
                CountDown();
                
           break;

           case GameState.Wavebreak:
                if(_waveBreak)
                {
                   OnWaveStart?.Invoke(false);
                  _initialTimeForEachWave += _extraTimeForEachWave;

                        if(_initialTimeForEachWave >= 90f)
                        {
                            _initialTimeForEachWave = 90f;
                        }
        
                   _thorwSystem.throwBreak -= _throwBreakTimeReducePerWave;
                   StartCoroutine(WaitToShowNoteText(_waitTimeToShowNoteText, "Wave End"));
                   _newWave = true;
                   _waveBreak = false;
                }
           break;
        }
    }

    IEnumerator WaitToShowNoteText(float waitTime, string text)
    {
        _waveSystemUI.ChangeNoteText(text);
        yield return new WaitForSeconds(waitTime);
        _waveSystemUI.ChangeNoteText(" ");
        ChangeState(GameState.WaveRunning);
    }

    IEnumerator WaitToShowNumberText(string text)
    {
        _waveSystemUI.ChangeNumberText(text);
        yield return new WaitForSeconds(_waitTimeToShowNumberText);
        _waveSystemUI.ChangeNumberText(" ");
        OnWaveStart?.Invoke(true);
        
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(_waitTimeToShowNumberText + 1f);
        _canCountDown = true;
        
    }

}
