using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NuniManager : MonoBehaviour                    //게임 시작하고 구글 스크립트에서 가지고 있는 누니 부를 때
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator RewardStart()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "questTime");
        form.AddField("player_nickname", GameManager.NickName);
        yield return StartCoroutine(RewardPost(form));
    }

    IEnumerator RewardPost(WWWForm form)
    {
        Debug.Log("RewardPost");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Reward_response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }

    }

    void Reward_response(string json)
    {
        Debug.Log("날짜: "+json);
        string time = json;
        if (time!= DateTime.Now.ToString("yyyy.MM.dd"))     //오늘날짜가 아니냐 일괄수확 가능
        {
            Debug.Log("마지막으로 수확했던 날짜: " + time);
            Debug.Log("오늘날짜: "+DateTime.Now.ToString("yyyy.MM.dd"));
            GameManager.isReward=true;
        }
        else
        {
            GameManager.isReward = false;               //오늘날짜면 수확 불가능
        }
        Debug.Log("수확가능여부: "+ GameManager.isReward);
    }
    public IEnumerator NuniStart()          //시작할 때 구글 스크립트에서 누니 목록 불러옴
    {
        Debug.Log("NuniStart");
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "nuniGet");
        form1.AddField("player_nickname", GameManager.NickName);




        yield return StartCoroutine(NuniPost(form1));                        //구글 스크립트로 초기화했는지 물어볼때까지 대기

    }
    IEnumerator NuniPost(WWWForm form)
    {
        Debug.Log("NuniPost");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Response_Nuni(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }

    }
    void Response_Nuni(string json)                          
    {
        //List<QuestInfo> Questlist = new List<QuestInfo>();
        Debug.Log(json);
        if (json == "null")
        {
            return;
        }
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        
        string[] Nunis = json.Split(',');//게임매니저에 있는 모든 누니배열에서 해당 누니 찾아서 가지고 있는 누니 배열에 넣기
        Debug.Log(Nunis.Length);
        
        for (int j = 0; j < Nunis.Length; j++)
        {
            
            string[] Nunis_nuni = Nunis[j].Split(':');
            for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
            {
                if (GameManager.AllNuniArray[i].cardName == Nunis_nuni[0])
                {
                    Card nuni = new Card();
                       Debug.Log(Nunis_nuni[0]);
                    Debug.Log(GameManager.AllNuniArray[i].cardName);
                    nuni.SetValue( GameManager.AllNuniArray[i]);
                    //Debug.Log("누니는 현재 "+Nunis_nuni[1]);
                    if (Nunis_nuni[1] == "T")
                    {
                        nuni.isLock = "T";

                    }
                    else
                        nuni.isLock = "F";
                    GameManager.CharacterList.Add(nuni);
                    break;
                  
                }
            }
           

        }
        Debug.Log("누니의 총 갯수는 " + GameManager.CharacterList.Count);
        for (int k = 0; k < GameManager.CharacterList.Count; k++)
        {

            Debug.Log("들어간 값: " + GameManager.CharacterList[k].isLock);
        }
    }

}
