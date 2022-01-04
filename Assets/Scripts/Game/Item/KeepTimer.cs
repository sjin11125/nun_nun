using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepTimer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 10f;
    float timeLeft;

    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        timeLeft = maxTime;
    }

    void Update()
    {
        if (timeLeft > 0 && timeLeft <= maxTime)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
