using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class FriendInfo
{
    public string f_nickname;      //巴傾戚嬢 莞革績
    //public string SheetsNum;     //巴傾戚嬢 闇弘 舛左 級嬢赤澗 什覗傾球 獣闘 id
    public string f_info;          //雌殿五室走
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

    public GameObject LoadingObjcet;
    public void FriendWindowOpen()
    {
        Content.SetActive(true);
        GetFriendLsit();
    }
    public void GetFriendLsit()         //庁姥 舛左 災君神奄
    {

        LoadingObjcet.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("order", "getFriend");
        form.AddField("id", "1234");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("info", "1234");
        StartCoroutine(ListPost(form));
    }
    public void GetRecFriendLsit()         //蓄探庁姥 舛左 災君神奄
    {

        LoadingObjcet.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("order", "RecoommendFriend");
        form.AddField("id", "1234");
        form.AddField("player_nickname", GameManager.NickName);
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 鋼球獣 using聖 潤醤廃陥
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) Response(www.downloadHandler.text);
            else print("瀬税 誓岩戚 蒸柔艦陥.");
        }
    }
    IEnumerator ListPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 鋼球獣 using聖 潤醤廃陥
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) ListResponse(www.downloadHandler.text);
            else print("瀬税 誓岩戚 蒸柔艦陥.");
        }
    }
    void ListResponse(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        Debug.Log(json);
        if (json == "")
        {
            LoadingObjcet.SetActive(false);
            return;
        }
        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
        FriendInfo[] friendInfos = new FriendInfo[j.Count];
        for (int i = 0; i < j.Count; i++)
        {
            friendInfos[i] = JsonUtility.FromJson<FriendInfo>(j[i].ToString());
            if (friendInfos[i].f_nickname=="")      //庁姥亜 蒸陥
            {
                LoadingObjcet.SetActive(false);
                return;
            }
            Debug.Log(friendInfos[i].f_nickname);
        }
        GameManager.Friends = friendInfos;

        Transform[] child = Content.GetComponentsInChildren<Transform>();           //析舘 段奄鉢
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }
        for (int i = 0; i < GameManager.Friends.Length; i++)
        {
            string[] friend = GameManager.Friends[i].f_nickname.Split(':');
            Debug.Log(friend.Length);
            if (friend.Length>=2)
            {
                continue;
            }
          
          
            GameObject friendprefab = Instantiate(FriendPrefab, Content.transform) as GameObject;  //庁姥 覗軒噸 持失
            Transform friendPrefabChilds = friendprefab.GetComponent<Transform>();
            friendPrefabChilds.name = GameManager.Friends[i].f_nickname;
            Text[] friendButtonText = friendprefab.GetComponentsInChildren<Text>();
            friendButtonText[0].text = friend[0];
            friendButtonText[1].text = GameManager.Friends[i].f_info;

            Image[] friendImage = friendprefab.GetComponentsInChildren<Image>();
            Debug.Log("戚耕走醤ちちちちちちちち     " + friendImage.Length);
            for (int k = 0; k < GameManager.AllNuniArray.Length; k++)
            {
                if (GameManager.AllNuniArray[k].Image.name != GameManager.Friends[i].f_image)
                    continue;
                friendImage[1].sprite = GameManager.AllNuniArray[k].Image;
            }
        }           //庁姥 鯉系 室特
        LoadingObjcet.SetActive(false);
    }
    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        Debug.Log(json);

        Transform[] child = Content.GetComponentsInChildren<Transform>();           //析舘 段奄鉢
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }

        Newtonsoft.Json.Linq.JArray j= Newtonsoft.Json.Linq.JArray.Parse(json);
        FriendInfo[] friendInfos=new FriendInfo[j.Count];
        for (int i = 0; i < j.Count; i++)
        {
            friendInfos[i] = JsonUtility.FromJson<FriendInfo>(j[i].ToString());
            Debug.Log(friendInfos[i].f_nickname);
        }
        GameManager.Friends = friendInfos;
        FriendsList();              //庁姥 鯉系 室特

    }

    public void FriendsList()
    {
        Transform[] child = Content.GetComponentsInChildren<Transform>();           //析舘 段奄鉢
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }
        for (int i = 0; i < GameManager.Friends.Length; i++)
        {
            GameObject friendprefab = Instantiate(FriendPrefab, Content.transform) as GameObject;  //庁姥 覗軒噸 持失
            Transform friendPrefabChilds = friendprefab.GetComponent<Transform>();
            friendPrefabChilds.name = GameManager.Friends[i].f_nickname;
            Text[] friendButtonText = friendprefab.GetComponentsInChildren<Text>();
            friendButtonText[0].text = GameManager.Friends[i].f_nickname;
            friendButtonText[1].text = GameManager.Friends[i].f_info;

            Image[] friendImage= friendprefab.GetComponentsInChildren<Image>();
            Debug.Log("戚耕走醤ちちちちちちちち     "+ friendImage.Length);
            for (int j   = 0; j < GameManager.AllNuniArray.Length; j++)
            {
                if (GameManager.AllNuniArray[j].Image.name != GameManager.Friends[i].f_image)
                    continue;
                friendImage[1].sprite = GameManager.AllNuniArray[j].Image;
            }
             
        }
        LoadingObjcet.SetActive(false);
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
