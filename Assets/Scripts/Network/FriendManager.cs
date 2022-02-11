using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class FriendInfo
{
    public string NickName;      //플레이어 닉네임
    public string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public string Info;          //상태메세지

    public FriendInfo(string nickname,string sheetsnum,string info)
    {
        NickName = nickname;  
        Info = info;   
        
    }
}
public class FriendManager : MonoBehaviour
{
    public GameObject Content;
    FriendInfo[] friends;
    const string URL = "https://script.google.com/macros/s/AKfycbzAB-AkKdMEmqLtNZqCeFaaL-_ArToHC9Br61iumIcfivza9P6DKogculNXweUxTu0JbA/exec";
    public FriendInfo Fr;
    public void FriendWindowOpen()
    {
        Content.SetActive(true);
        GetFriendLsit();
    }
    public void GetFriendLsit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getFriend");
        form.AddField("id", "1234");
        form.AddField("nickname", "1234");
        form.AddField("info", "1234");
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            /*if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");*/
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        Fr = JsonUtility.FromJson<FriendInfo>(json);

        
    }
    // Start is called before the first frame update
    void Start()
    {
       /* WWWForm form = new WWWForm();

        form.AddField("order", "info");
        form.AddField("id", "1234");
        form.AddField("nickname", "1234");
        form.AddField("info", "1234");



        StartCoroutine(Post(form));*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
