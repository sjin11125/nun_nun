using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class VisitorBook
{
    public string f_nickname;      //친구
    public string f_massage;        //친구가 보낸 메세지
    public VisitorBook(string nickname, string message)
    {
        this.f_nickname = nickname;
        this.f_massage = message;

    }
}
public class VisitorBookManager : MonoBehaviour
{
    public GameObject VBWindow;
    public GameObject Content;
    //FriendInfo[] ;
    string URL = GameManager.URL;
    public VisitorBook VB;

    public GameObject VBPrefab;             //방명록 목록 프리팹

    public InputField VBInput;          

    public void VBWindowOpen()              //방명록 창 오픈했을 때
    {
        VBWindow.SetActive(true);           
        VisitorBookList();              //방명록 있나 확인
    }
    // Start is called before the first frame update
     public void VisitorBookList()  //방명록 불러옴
    {

    }

    public void VisitorBookWrite()          //방명록 쓰기        (보내기 버튼에 넣기)
    {
        Debug.Log("방명록 쓰기");
        WWWForm form = new WWWForm();
        form.AddField("order", "enrollMessage");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("friend_nickname", GameManager.friend_nickname);
        form.AddField("message", VBInput.text);
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            
        }
    }
}
