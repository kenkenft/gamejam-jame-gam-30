using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAge : MonoBehaviour
{
    
    [SerializeField] private int _maxTime = 60, _currentTime = 0;
    WaitForSecondsRealtime timerDelay = new WaitForSecondsRealtime(0.5f);

    [HideInInspector] public static ReturnBool CheckIsPaused;
    [HideInInspector] public static OnSomeEvent TriggerEndGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetUp()
    {
        _currentTime = _maxTime;
    }

    public IEnumerator Countdown(int startAmount)
    {
        _currentTime = _maxTime;
        while(_currentTime > 0)
        {
            yield return timerDelay;
            // if(!CheckIsPaused.Invoke())
            // {
                _currentTime--;
                
            // }
        }
        StopCoroutine("Countdown");
        // gameManager.TriggerEndgame();
        // TriggerEndGame?.Invoke();
        yield return null;
    }
}
