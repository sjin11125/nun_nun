using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExitController : MonoBehaviour
{
    public void Awake()
    {
        GameLoad();

        int tutorialsDone = PlayerPrefs.GetInt("TutorialDone", 0);
        if (tutorialsDone == 0)
        {
            SceneManager.LoadScene("TutorialsScene");
            return;
        }
    }

    public void GameSave()
    {
        PlayerPrefs.SetInt("Money", GameManager.Money);//µ·
        PlayerPrefs.SetInt("ShinMoney", GameManager.ShinMoney);//µ·
        PlayerPrefs.Save();
        print("save");
    }
    public void GameLoad()
    {
        GameManager.Money = PlayerPrefs.GetInt("Money");
        GameManager.ShinMoney = PlayerPrefs.GetInt("ShinMoney");
        //GameManager.ShinMoney = 10000;
        print("load");
    }
    public void GameExit()
    {
        GameSave();
        print("exit");
        Application.Quit();
    }
}
