using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameExitController : MonoBehaviour
{
    public GameObject TutoManager;
    public void Awake()
    {       
        if (TutorialsManager.itemIndex < 14)
        {
            TutoManager.SetActive(true);
            RandomSelect.isTuto = 0;
        }
        else
        {
            TutoManager.SetActive(false);
            RandomSelect.isTuto = 1;
        }
    }

    public void GameSave()
    {
        PlayerPrefs.SetInt("Money", GameManager.Money);
        PlayerPrefs.SetInt("ShinMoney", GameManager.ShinMoney);
        PlayerPrefs.Save();
        print("save");

        WWWForm form2 = new WWWForm();
        //isMe = true;                 
        form2.AddField("order", "setMoney");
        form2.AddField("player_nickname", GameManager.NickName);
        //form2.AddField("money", GameManager.Money.ToString()+"@"+GameManager.ShinMoney.ToString());

    }

    public void GameExit()
    {
        GameSave();
        print("exit");
        Application.Quit();
    }
}
