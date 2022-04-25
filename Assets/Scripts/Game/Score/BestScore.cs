using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public Text bestScoreText;
    public Text coinText;
    public Text currentScoreText;
    public Text shinText;

    private void OnEnable()
    {
        GameEvents.UpdateBestScore += UpdateBestScore;
    }

    private void OnDisable()
    {
        GameEvents.UpdateBestScore -= UpdateBestScore;
    }

    private void UpdateBestScore(int currentScore,  int bestScore, int currentShinScores)
    {
        bestScoreText.text = bestScore.ToString();
        coinText.text = currentScore.ToString();
        currentScoreText.text = currentScore.ToString();
        shinText.text = currentShinScores.ToString();
    }
}
