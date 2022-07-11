using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class FriendInfo
{
    public string f_nickname;      //�÷��̾� �г���
    //public string SheetsNum;     //�÷��̾� �ǹ� ���� ����ִ� �������� ��Ʈ id
    public string f_info;          //���¸޼���
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

    FriendInfo[] AllFriends;       //친구 전체 목록(닉네임)
    public void FriendWindowOpen()
    {
        Content.SetActive(true);
        GetFriendLsit();
    }
    public void GetFriendLsit()         //ģ�� ���� �ҷ�����
    {

        LoadingObjcet.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("order", "getFriend");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("info", "1234");
        StartCoroutine(ListPost(form));
    }
    public void GetRecFriendLsit()         //��õģ�� ���� �ҷ�����
    {

        LoadingObjcet.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("order", "RecoommendFriend");
        form.AddField("player_nickname", GameManager.NickName);
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            if (www.isDone) Response(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
        }
    }
    IEnumerator ListPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            if (www.isDone) ListResponse(www.downloadHandler.text);
            else print("실행되지않음");
        }
    }
    IEnumerator TempListPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            if (www.isDone) TempListResponse(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
        }
    }
    void TempListResponse(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        if (json.Equals(""))
        {
            LoadingObjcet.SetActive(false);
            return;
        }
        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
        FriendInfo[] friendInfos = new FriendInfo[j.Count];
        for (int i = 0; i < j.Count; i++)
        {
            friendInfos[i] = JsonUtility.FromJson<FriendInfo>(j[i].ToString());
            if (friendInfos[i].f_nickname.Equals(""))  //ģ���� ����
            {
                LoadingObjcet.SetActive(false);
                return;
            }
        }
        GameManager.Friends = friendInfos;

        LoadingObjcet.SetActive(false);
    }
    void ListResponse(string json)
    {
        if (string.IsNullOrEmpty(json)) return;
        Debug.Log("json: "+json);
        if (json.Equals("null"))
        {
            LoadingObjcet.SetActive(false);
            return;
        }
        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
        FriendInfo[] friendInfos = new FriendInfo[j.Count];
        for (int i = 0; i < j.Count; i++)
        {
            friendInfos[i] = JsonUtility.FromJson<FriendInfo>(j[i].ToString());
            if (friendInfos[i].f_nickname.Equals("")    )  //ģ���� ����
            {
                LoadingObjcet.SetActive(false);
                return;
            }
        }
        GameManager.Friends = friendInfos;

        Transform[] child = Content.GetComponentsInChildren<Transform>();           //�ϴ� �ʱ�ȭ
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }
        for (int i = 0; i < GameManager.Friends.Length; i++)
        {
            string[] friend = GameManager.Friends[i].f_nickname.Split(':');
            if (friend.Length>=2)
            {
                continue;
            }
          
          
            GameObject friendprefab = Instantiate(FriendPrefab, Content.transform) as GameObject;  //ģ�� ������ ����
            Transform friendPrefabChilds = friendprefab.GetComponent<Transform>();
            friendPrefabChilds.name = GameManager.Friends[i].f_nickname;
            Text[] friendButtonText = friendprefab.GetComponentsInChildren<Text>();
            friendButtonText[0].text = friend[0];
            friendButtonText[1].text = GameManager.Friends[i].f_info;

            Image[] friendImage = friendprefab.GetComponentsInChildren<Image>();
            for (int k = 0; k < GameManager.AllNuniArray.Length; k++)
            {
                if (GameManager.AllNuniArray[k].Image.name != GameManager.Friends[i].f_image)
                    continue;
                friendImage[1].sprite = GameManager.AllNuniArray[k].Image;
            }
        }           //ģ�� ��� ����
        LoadingObjcet.SetActive(false);
    }
    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;



        Transform[] child = Content.GetComponentsInChildren<Transform>();           //�ϴ� �ʱ�ȭ
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }

        Newtonsoft.Json.Linq.JArray j= Newtonsoft.Json.Linq.JArray.Parse(json);
        FriendInfo[] friendInfos=new FriendInfo[j.Count];
        for (int i = 0; i < j.Count; i++)
        {
            friendInfos[i] = JsonUtility.FromJson<FriendInfo>(j[i].ToString());

        }
        AllFriends = friendInfos;
        LoadingObjcet.SetActive(true);

        WWWForm form = new WWWForm();                   //친구목록 업데이트
        form.AddField("order", "getFriend");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("info", "1234");
        StartCoroutine(TempListPost(form));

        FriendsList();              //ģ�� ��� ����

    }

    public void FriendsList()
    {
        Transform[] child = Content.GetComponentsInChildren<Transform>();           //�ϴ� �ʱ�ȭ
        for (int k = 1; k < child.Length; k++)
        {
            Destroy(child[k].gameObject);
        }
        for (int i = 0; i < AllFriends.Length; i++)
        {
            bool alreadyFriend = false;
            GameObject friendprefab = Instantiate(FriendPrefab, Content.transform) as GameObject;  //ģ�� ������ ����
            Transform friendPrefabChilds = friendprefab.GetComponent<Transform>();
            friendPrefabChilds.name = AllFriends[i].f_nickname;
            Text[] friendButtonText = friendprefab.GetComponentsInChildren<Text>();
            friendButtonText[0].text = AllFriends[i].f_nickname;
            friendButtonText[1].text = AllFriends[i].f_info;

            Image[] friendImage= friendprefab.GetComponentsInChildren<Image>();
            if (GameManager.Friends!=null)
            {
                for (int j = 0; j < GameManager.Friends.Length; j++)

                {
                    if (AllFriends[i].f_nickname == GameManager.Friends[j].f_nickname)      //이미 친구로 되어있다.
                    {

                        Button[] btnn = friendprefab.GetComponentsInChildren<Button>();
                        btnn[1].interactable = false;               //Ŭ�����ϰ�
                        Text[] ButtonText = friendprefab.GetComponentsInChildren<Text>();
                        ButtonText[2].gameObject.SetActive(false);
                        ButtonText[3].gameObject.SetActive(true);
                        ButtonText[3].text = "추가됨";
                        alreadyFriend = true;
                    }
                    if (GameManager.Friends[j].f_nickname.Split(':').Length >= 2)
                    {

                        if (AllFriends[i].f_nickname == GameManager.Friends[j].f_nickname.Split(':')[0])                           //이미 요청보냈었다.
                        {

                            Button[] btnn = friendprefab.GetComponentsInChildren<Button>();
                            btnn[1].interactable = false;               //Ŭ�����ϰ�
                            Text[] ButtonText = friendprefab.GetComponentsInChildren<Text>();
                            Debug.Log("버튼 텍스트 길이: "+ButtonText.Length);
                            ButtonText[2].gameObject.SetActive(false);
                            ButtonText[3].gameObject.SetActive(true);
                            ButtonText[3].text = "요청됨";
                            alreadyFriend = true;
                        }
                    }
                }
               
            }

            if (alreadyFriend == false)
            {

                Button[] btnn = friendprefab.GetComponentsInChildren<Button>();
                btnn[1].interactable = true;               //Ŭ�����ϰ�
                Text[] ButtonText = friendprefab.GetComponentsInChildren<Text>();
                ButtonText[2].gameObject.SetActive(true);
                ButtonText[3].gameObject.SetActive(false);
            }
            for (int j = 0; j < GameManager.AllNuniArray.Length; j++)               //친구 프사 설정
            {
                if (GameManager.AllNuniArray[j].Image.name != AllFriends[i].f_image)
                    continue;
                friendImage[1].sprite = GameManager.AllNuniArray[j].Image;
            }
        }

        LoadingObjcet.SetActive(false);
    }
}
