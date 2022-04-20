using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class QuestInfo   //구글 스크립트와 통신할 퀘스트 인포
{
    public string quest,count;           //퀘스트([코드:횟수])

}
public class QuestManager : MonoBehaviour
{


    TextAsset csvData;             //퀘스트 리스트(일단 3개만 하고 나중에 추가해)
    List<string[]> Questlist;
    public Text[] QuestText;         //퀘스트 텍스트들
    string[][] Quest;

    QuestInfo[] QuestArray=new QuestInfo[3];          //퀘스트 진행상황 배열
    List<QuestInfo> GetQuestList = new List<QuestInfo>();
    // Start is called before the first frame update
    void Start()                //시작할 때 퀘스트 목록 불러옴
    {

        Questlist = new List<string[]>();

        Quest = new string[Questlist.Count][];

        csvData = Resources.Load<TextAsset>("Quest");
        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] pro_data = data[i].Split(',');

            Questlist.Add(pro_data); //퀘스트 리스트에 넣기
        }

        Quest = Questlist.ToArray();

        if (GameManager.Quest == null)
        {
            Debug.Log("퀘스트는 널");
        }
        for (int i = 0; i < Questlist.Count; i++)
        {
            Debug.Log(Quest[i][0]);
            GameManager.Quest.Add(Quest[i][0], 0); //게임매니저에 있는 퀘스트 딕셔너리에 퀘스트 코드 넣기
                                                   //구글 스크립트에 퀘스트 리스트 업데이트
            QuestSave(Quest[i][0]);                             // 처음엔 퀘스트 아무것도 안했으니까 0으로 두자
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void QuestSave(string quest)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "questSave");
        form1.AddField("player_nickname", GameManager.NickName);
        form1.AddField("quest", quest);

        StartCoroutine(Post(form1));


    }
    public void QuestClick()            //퀘스트 버튼 클릭했을 때 퀘스트 진행 상황 불러와
    {
        for (int i = 0; i < 3; i++)
        {
            WWWForm form1 = new WWWForm();                                      //진행상황 불러옴
            form1.AddField("order", "questGet");
            form1.AddField("player_nickname", GameManager.NickName);
            form1.AddField("quest", Quest[i][0]);

            StartCoroutine(Post(form1));
        }
        for (int i = 0; i < 3; i++)
        {
            
        }


    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Response(www.downloadHandler.text);  
                                                                        //else print("웹의 응답이 없습니다.");*/
        }

    }
    void Response(string json)                          //퀘스트 진행상황 불러오기
    {
        //List<QuestInfo> Questlist = new List<QuestInfo>();
        Debug.Log(json);
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

        if (GetQuestList.Count>=3)
        {
            QuestArray=GetQuestList.ToArray();
            for (int i = 0; i < QuestArray.Length; i++)
            {
                Debug.Log(QuestArray[i].quest + ": " + QuestArray[i].count);
                QuestText[i].text = Quest[i][1] + "    (" + QuestArray[i].count + "/" + Quest[i][2] + ")";                    //퀘스트 내용 뜨게함

            }
        }
        

    }
}
