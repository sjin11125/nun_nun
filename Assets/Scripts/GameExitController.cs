using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
        print("save");*/

        WWWForm form2 = new WWWForm();
        Debug.Log("ÀÚ¿øÀúÀå");
        //isMe = true;                    //ÀÚ¿ø ºÒ·¯¿À±â
        form2.AddField("order", "setMoney");
        form2.AddField("player_nickname", GameManager.NickName);
        form2.AddField("money", GameManager.Money.ToString()+"|"+GameManager.ShinMoney.ToString());

        StartCoroutine(MoneyPost(form2));
    }
    IEnumerator MoneyPost(WWWForm form)
    {
        Debug.Log("ÀúÀåÇÏ¶ó");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // ¹Ýµå½Ã usingÀ» ½á¾ßÇÑ´Ù
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone)
            {

                MoneyResponse(www.downloadHandler.text);

            }
            else print("À¥ÀÇ ÀÀ´äÀÌ ¾ø½À´Ï´Ù.");
        }

    }

    void MoneyResponse(string json)                          //ÀÚ¿ø °ª ºÒ·¯¿À±â
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log("ÇöÀçµ·:      " + json);
        string[] moneys = json.Split('|');

        GameManager.Money = int.Parse(moneys[0]);
        GameManager.ShinMoney = int.Parse(moneys[1]);

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
