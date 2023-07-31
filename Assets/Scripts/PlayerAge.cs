using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAge : MonoBehaviour
{
    
    [SerializeField] private int _maxTime = 60, _currentTime = 0;
    WaitForSecondsRealtime timerDelay = new WaitForSecondsRealtime(0.5f);

    [HideInInspector] public static ReturnBool CheckIsPaused;
    [HideInInspector] public static OnSomeEvent TriggerEndGame;

    [HideInInspector] public static SendInt AgeChanged;
    [HideInInspector] public static ReturnInt AgeRequested;
    // Start is called before the first frame update
     
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
            yield return timerDelay;
            // if(!CheckIsPaused.Invoke())
            // {
                _currentTime--;
                Debug.Log($"Time Left: {_currentTime}");
                SetPlayerAge();
            // }
        }
        StopCoroutine("Countdown");
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
}
