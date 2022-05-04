using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class FriendInfo
{
    public string f_nickname;      //플레이어 닉네임
    //public string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public string f_info;          //상태메세지
    public string f_id;
    public string f_image;

    public FriendInfo(string nickname,string id,string info)
    {
        this.f_nickname = nickname;
        this.f_id = id;   
        this.f_info = info;   
        
    }
}
public class FriendManager : MonoBehaviour
{
    public GameObject Content;
    //FriendInfo[] ;
     string URL = GameManager.URL;
    public FriendInfo Fr;

    public GameObject FriendPrefab;
    public void FriendWindowOpen()
    {
        Content.SetActive(true);
        GetFriendLsit();
    }
    public void GetFriendLsit()         //친구 정보 불러오기
    {


        WWWForm form = new WWWForm();
        form.AddField("order", "getFriend");
        form.AddField("id", "1234");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("info", "1234");
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        Debug.Log(json);
        
        Newtonsoft.Json.Linq.JArray j= Newtonsoft.Json.Linq.JArray.Parse(json);
        FriendInfo[] friendInfos=new FriendInfo[j.Count];
        for (int i = 0; i < j.Count; i++)
        {
            friendInfos[i] = JsonUtility.FromJson<FriendInfo>(j[i].ToString());
            Debug.Log(friendInfos[i].f_nickname);
        }
        GameManager.Friends = friendInfos;
        FriendsList();              //친구 목록 세팅

    }

    public void FriendsList()
    {
        for (int i = 0; i < GameManager.Friends.Length; i++)
        {
            GameObject friendprefab = Instantiate(FriendPrefab, Content.transform) as GameObject;  //친구 프리팹 생성
            Transform friendPrefabChilds = friendprefab.GetComponent<Transform>();
            friendPrefabChilds.name = GameManager.Friends[i].f_nickname;
            string[] friendRequest = GameManager.Friends[i].f_nickname.Split(':');
            if (friendRequest.Length>1)     //아직 요청 못 받았나
            {
                Button[] friendButton= friendprefab.GetComponentsInChildren<Button>();
                friendButton[1].GetComponent<Button>().interactable = false;
                friendButton[1].GetComponentInChildren<Text>().text = "요청됨";

            }
           
            Text[] friendButtonText = friendprefab.GetComponentsInChildren<Text>();
            friendButtonText[0].text = friendRequest[0];
            friendButtonText[1].text = GameManager.Friends[i].f_info;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
