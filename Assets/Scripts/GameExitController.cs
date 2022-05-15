using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameExitController : MonoBehaviour
{
    public void Awake()
    {
        //GameLoad();
    }
    public void GameSave()
    {/*
        PlayerPrefs.SetInt("Money", GameManager.Money);//돈
        PlayerPrefs.SetInt("ShinMoney", GameManager.ShinMoney);//돈
        PlayerPrefs.Save();
        print("save");*/

        WWWForm form2 = new WWWForm();
        Debug.Log("자원저장");
        //isMe = true;                    //자원 불러오기
        form2.AddField("order", "setMoney");
        form2.AddField("player_nickname", GameManager.NickName);
        form2.AddField("money", GameManager.Money.ToString()+"|"+GameManager.ShinMoney.ToString());

        StartCoroutine(MoneyPost(form2));
    }
    IEnumerator MoneyPost(WWWForm form)
    {
        Debug.Log("저장하라");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
           /* if (www.isDone)
            {

                MoneyResponse(www.downloadHandler.text);

            }
            else print("웹의 응답이 없습니다.");*/
        }

    }

   /* void MoneyResponse(string json)                          //자원 값 불러오기
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log("현재돈:      " + json);
        string[] moneys = json.Split('|');

        GameManager.Money = int.Parse(moneys[0]);
        GameManager.ShinMoney = int.Parse(moneys[1]);

    }*/
    public void GameLoad()
    {
        GameManager.ShinMoney = 10000;
        print("load");
    }
    public void GameExit()
    {
        GameSave();
        print("exit");
        Application.Quit();
    }
}
