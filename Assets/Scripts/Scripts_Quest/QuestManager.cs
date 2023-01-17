using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using Random = System.Random;

[Serializable]
public class QuestInfo   //구글 스크립트와 통신할 퀘스트 인포
{
    public string quest,count,title;           //퀘스트코드, 횟수

}
public class QuestManager : MonoBehaviour
{


    TextAsset csvData;             //퀘스트 리스트(일단 3개만 하고 나중에 추가해)
    List<QuestInfo> Questlist;
    public Text[] QuestText;         //퀘스트 텍스트들
    QuestInfo[] Quest;

    QuestInfo[] QuestArray=new QuestInfo[3];          //퀘스트 진행상황 배열
    List<QuestInfo> GetQuestList = new List<QuestInfo>();
    bool isStart = false;

    string isReset;           //일퀘 초기화 변수
    // Start is called before the first frame update


    public void QuestSave(string quest)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "questSave");
        form1.AddField("player_nickname", GameManager.NickName);
        form1.AddField("quest", quest);
        form1.AddField("isReset", isReset);
        form1.AddField("time", DateTime.Now.ToString("yyyy.MM.dd"));

      

    }
    public void QuestClick()            //퀘스트 버튼 클릭했을 때 퀘스트 진행 상황 불러와
    {
        for (int i = 0; i < 3; i++)
        {
            WWWForm form1 = new WWWForm();                                      //진행상황 불러옴
            form1.AddField("order", "questGet");
            form1.AddField("player_nickname", GameManager.NickName);

        }
       
    }

   
    void Response_Time(string json)                          //퀘스트 초기화 확인
    {
        Debug.Log(json);
       

        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }

        if (json == DateTime.Now.ToString("yyyy.MM.dd"))                         //불러온 날짜가 오늘 날짜면 초기화 변수 true
        {
            //GameManager.isReset = true;
            isReset = "true";
            QuestClick();    //초기화 했으면 구글 스크립트에서 진행상황만 불러오고 구글 스크립트에 오늘날짜 넣어

            return;
        }
        else                                                                    //불러온 날짜가 오늘이 아니고(초기화 안했다면) 새로 가입했다면 false
        {
            isReset = "false";
            // GameManager.isReset = false;
        }
        isStart = true;


    }
        void Response(string json)                          //퀘스트 진행상황 불러오기
    {
        //List<QuestInfo> Questlist = new List<QuestInfo>();
        Debug.Log("Quest: "+json);
        if (json=="null")
        {
            return;
        }
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        
        QuestInfo questInfo = JsonUtility.FromJson<QuestInfo>(json);
        
        GetQuestList.Add(questInfo);
        Debug.Log(GetQuestList.Count);
    }

    public void QuestExit()
    {
        GetQuestList.Clear();               //퀘스트 진행상황 리스트 초기화
    }
}
