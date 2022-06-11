using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameExitController : MonoBehaviour
{
    public GameObject TutoManager;
    public GameObject ExitPanel;

    public void Awake()
    {
        if (TutorialsManager.itemIndex < 14)
        {
            TutoManager.SetActive(true);
            if (TutorialsManager.itemIndex > 9)
            {
                RandomSelect.isTuto = 1;
            }
            else
            {
                RandomSelect.isTuto = 0;//����Ʃ�� ����
            }
        }
        else
        {
            TutoManager.SetActive(false);
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

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {
                ExitPanel.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                ExitPanel.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.Menu))
            {
                ExitPanel.SetActive(true);
            }
        }
    }
}
