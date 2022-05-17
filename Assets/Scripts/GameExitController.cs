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
        int tutorialsDone = PlayerPrefs.GetInt("TutorialsDone", 0);
        
        if (tutorialsDone == 0)//종료는 튜토매니저
        {
            TutoManager.SetActive(true);
            RandomSelect.isTuto = 0;
            return;
        }
        else
        {
            TutoManager.SetActive(false);
            RandomSelect.isTuto = 1;
            GameLoad();
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
        form2.AddField("money", GameManager.Money.ToString()+"|"+GameManager.ShinMoney.ToString());

        StartCoroutine(MoneyPost(form2));
    }
    IEnumerator MoneyPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) 
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone)
            {
                MoneyResponse(www.downloadHandler.text);
            }
        }

    }

    void MoneyResponse(string json)                        
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        string[] moneys = json.Split('|');

        GameManager.Money = int.Parse(moneys[0]);
        GameManager.ShinMoney = int.Parse(moneys[1]);

    }
    public void GameLoad()
    {
        GameManager.Money = PlayerPrefs.GetInt("Money");
        GameManager.ShinMoney = PlayerPrefs.GetInt("ShinMoney");
        print("load");
    }
    public void GameExit()
    {
        GameSave();
        print("exit");
        Application.Quit();
    }
}
