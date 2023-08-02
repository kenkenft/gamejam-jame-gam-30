using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using TMPro;

public class PlayerOverlay : MonoBehaviour
{
    public ScoreTextProperties scoreTextProperties;
    public Timer timer;
    // Canvas playerOverlayCanvas;

    void OnEnable()
    {
        // SetUp();
        // StartTimer(0);
        UIManager.StartGameSetUp += GameStartSetUp;
    }

    void OnDisable()
    {
        // SetUp();
        // StartTimer(0);
        UIManager.StartGameSetUp -= GameStartSetUp;
    }
    
    public void SetUp()
    {
        // playerOverlayCanvas = GetComponentInChildren<Canvas>();
        
        // scoreTextProperties = GetComponentInChildren<ScoreTextProperties>();
        // timer = GetComponentInChildren<Timer>();
    }

    public void GameStartSetUp()
    {
        // playerOverlayCanvas = GetComponentInChildren<Canvas>();
        
        // scoreTextProperties = GetComponentInChildren<ScoreTextProperties>();
        // timer = GetComponentInChildren<Timer>();
        StartTimer(120);
        ResetOverlay();
    }

    public void ResetOverlay()
    {
        scoreTextProperties.ResetScore();
    }

    public void StartTimer(int startTime)
    {
        StartCoroutine(timer.Countdown(startTime));
    }

}
