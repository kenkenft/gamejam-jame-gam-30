using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAge : MonoBehaviour
{
    
    [SerializeField] private int _maxTime = 60, _currentTime = 0, _enemySpawnTimer = 10;
    [HideInInspector] public static ReturnBool CheckIsPaused;
    [HideInInspector] public static OnSomeEvent TriggerEndGame;
    [HideInInspector] public static OnSomeEvent HourGlassFlipped;
    [HideInInspector] public static OnSomeEvent SpawnTimerExpired;

    [HideInInspector] public static SendInt AgeChanged;
    [HideInInspector] public static ReturnInt AgeRequested;
    [HideInInspector] public static SendFloat HourGlassUpdated;
    [SerializeField] public static SendString PlaySFX;
     
    void OnEnable()
    {
        PlayerMain.HourGlassFlipped = FlipHourGlass;
    }

    void Disable()
    {
        PlayerMain.HourGlassFlipped = null;
    }

    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        _currentTime = _maxTime;
        StartCoroutine("Countdown");
    }

    public IEnumerator Countdown()
    {
        _currentTime = _maxTime;
        while(_currentTime > 0)
        {
            yield return GameProperties.timerDelay;
            // if(!CheckIsPaused.Invoke())
            // {
                CheckSpawnTime();
                _currentTime--;
                // Debug.Log($"Time Left: {_currentTime}");
                HourGlassUpdated?.Invoke((float)_currentTime/(float)_maxTime);
                SetPlayerAge();
            // }
        }
        StopCoroutine("Countdown");
        PlaySFX?.Invoke("GameOver");
        // gameManager.TriggerEndgame();
        // TriggerEndGame?.Invoke();
        yield return null;
    }

    void SetPlayerAge()
    {

        if(_currentTime > 40)
        {    
            if(AgeRequested.Invoke() != (int)AgeState.Young)
                AgeChanged?.Invoke((int)AgeState.Young);
        }
        else if(_currentTime > 20 && _currentTime <= 40)
        {
            if(AgeRequested.Invoke() != (int)AgeState.Middle)
                AgeChanged?.Invoke((int)AgeState.Middle);
        }
        else if(_currentTime > 0 && _currentTime <= 20)
        {
            if(AgeRequested.Invoke() != (int)AgeState.Old)
                AgeChanged?.Invoke((int)AgeState.Old);
        }
        else
            AgeChanged?.Invoke((int)AgeState.Dead);
    }

    void FlipHourGlass()
    {
        Debug.Log($"FlipHourGlass called!");
        _currentTime = _maxTime - _currentTime;
        HourGlassFlipped?.Invoke();
        HourGlassUpdated?.Invoke((float)_currentTime/(float)_maxTime);
    }

    void CheckSpawnTime()
    {
        if(_enemySpawnTimer > 0)
             _enemySpawnTimer--;
        else
        {
            SpawnTimerExpired?.Invoke();
            _enemySpawnTimer = 10;
        }
    }
}
