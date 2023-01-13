using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BestScoreData
{
    public int score = 0;
}

public class Scores : MonoBehaviour
{
    public Text scoreText;
    public Text shinText;

    private bool newBestScore_ = false;
    public GameObject newBestScoreObj;

    private BestScoreData bestScores_ = new BestScoreData();
    public int currentScores_;
    public int currentShinScores_;

    public string bestScoreKey_ = "bsdat";

    private void Awake()
    {
        if (BinaryDataStream.Exist(bestScoreKey_))
        {
            StartCoroutine(ReadDataFile());
        }
        bestScores_.score = GameManager.BestScore;
    }

    private IEnumerator ReadDataFile()
    {
        bestScores_ = BinaryDataStream.Read<BestScoreData>(bestScoreKey_);
        yield return new WaitForEndOfFrame();
        GameEvents.UpdateBestScore(currentScores_, bestScores_.score, currentShinScores_);
    }

    void Start()
    {
        currentScores_ = 0;
        currentShinScores_ = 0;
        newBestScore_ = false;
        newBestScoreObj.SetActive(false);
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameEvents.AddScores += AddScores;
        GameEvents.GameOver += SaveBestScores;
    }

    private void OnDisable()
    {
        GameEvents.AddScores -= AddScores;
        GameEvents.GameOver -= SaveBestScores;
    }

    public void SaveBestScores(bool newBestScores)
    {
        BinaryDataStream.Save<BestScoreData>(bestScores_,bestScoreKey_);
    }

    private void AddScores(int scores, int shinscores)
    {
        currentScores_ += scores;
        currentShinScores_ = shinscores;
        if (currentScores_ > bestScores_.score)
        {
            newBestScore_ = true;
            bestScores_.score = currentScores_;
            SaveBestScores(true);
        }
       
        GameEvents.UpdateBestScore(currentScores_, bestScores_.score, currentShinScores_);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScores_.ToString();
        shinText.text = currentShinScores_.ToString();
        if (newBestScore_ == true)
        {
            newBestScoreObj.SetActive(true);
        }
    }

    public void coinInput()
    {
        int Money = int.Parse(GameManager.Instance.PlayerUserInfo.Money);
        Money += currentScores_;
        GameManager.Instance.PlayerUserInfo.Money = Money.ToString();


        CanvasManger.AchieveMoney += currentScores_;

        int ShinMoney = int.Parse(GameManager.Instance.PlayerUserInfo.ShinMoney);
        ShinMoney += currentShinScores_;
        GameManager.Instance.PlayerUserInfo.ShinMoney = ShinMoney.ToString();
         
        CanvasManger.AchieveShinMoney += currentShinScores_;

        GameManager.BestScore = bestScores_.score;
    }

    public void GameRestart()
    {
        if (ItemController.reStart)
        {
            currentScores_ = 0;
            currentShinScores_ = 0;
            newBestScore_ = false;
            newBestScoreObj.SetActive(false);
            UpdateScoreText();
            GameManager.BestScore = bestScores_.score;
        }

    }
}
