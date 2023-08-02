using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int timeLeft = 9;
    Text timerText;
    // GameManager gameManager;
    WaitForSecondsRealtime timerDelay = new WaitForSecondsRealtime(0.5f);
    WaitForSecondsRealtime addTimerDelay = new WaitForSecondsRealtime(0.4f);
    Color defaultColor = Color.white, addColor = Color.green, currColor;

    [HideInInspector] public delegate void OnTimeHasPassed();
    [HideInInspector] public static OnTimeHasPassed SpawnNewCoin;
    [HideInInspector] public static OnTimeHasPassed TriggerEndGame;
    [HideInInspector] public static OnTimeHasPassed CheckCoinsDespawn;

    [HideInInspector] public delegate bool OnCheckIsPaused();
    [HideInInspector] public static OnCheckIsPaused CheckIsPaused;


    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        timerText.text = "Time: " + timeLeft;
        currColor = timerText.color;
        // gameManager = GetComponentInParent<GameManager>();
    }

    public IEnumerator Countdown(int startAmount)
    {
        timeLeft = startAmount;
        while(timeLeft > 0)
        {
            yield return timerDelay;
            if(!CheckIsPaused.Invoke())
            {
                timeLeft--;
                timerText.text = "Time: " + timeLeft;
                CheckCoinsDespawn?.Invoke();
                TimeToSpawnNewCoin();
            }
        }
        StopCoroutine("Countdown");
        // gameManager.TriggerEndgame();
        TriggerEndGame?.Invoke();
        yield return null;
    }

    IEnumerator WaitAddTimer()
    {
        yield return addTimerDelay;
    }

    void TimeToSpawnNewCoin()
    {
        if(timeLeft % 3 == 0)
            SpawnNewCoin?.Invoke();
    }

}
