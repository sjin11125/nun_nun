using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 100f;
    [HideInInspector]
    public float timeLeft;

    public GameObject GameOverPanel;
    public GameObject timer;

    void Start()
    {
        GameOverPanel.SetActive(false);
        timer.SetActive(true);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (timeLeft > 0 )//&& timeLeft <= maxTime)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            GameOverPanel.SetActive(true);
            timer.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void plusScore()
    {
        timeLeft += 0.5f;
    }
}
