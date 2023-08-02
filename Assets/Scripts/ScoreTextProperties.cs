using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreTextProperties : MonoBehaviour
{
    int currentScore = 0;
    Text scoreText;

    void OnEnable()
    {
        // PlayerStorage.DepositCoin += UpdateScore;
        UIManager.GetFinalScore += GetCurrentScore;
        EnemyMain.EnemyDied += UpdateScore;
    }

    void OnDisable()
    {
        // PlayerStorage.DepositCoin -= UpdateScore;
        UIManager.GetFinalScore -= GetCurrentScore;
        EnemyMain.EnemyDied -= UpdateScore;
    }
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "Score:\n" + currentScore;
    }

    public void UpdateScore(int points)
    {
        currentScore += points;
        scoreText.text = "Score:\n" + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "Score:\n" + currentScore;
    }

    public int GetCurrentScore()
    {
        Debug.Log("currentScore: " + currentScore);
        return currentScore;
    }
}
