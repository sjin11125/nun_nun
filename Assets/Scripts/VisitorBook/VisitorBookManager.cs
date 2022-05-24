using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VisitorBook
{
    public string f_nickname;      //친구
    public string f_message;        //친구가 보낸 메세지
    public string f_time;        //친구가 보낸 시간
    public string f_image;        //친구프사
    public VisitorBook(string nickname, string message,string time)
    {
        this.f_nickname = nickname;
        this.f_message = message;
        this.f_time = time;
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
        //VBWindow.SetActive(true);
        
    }

    public void Start()
    {
        VBWindowOpen();
        if (SceneManager.GetActiveScene().name.Equals("FriendMain"))    //친구 씬이냐
        {
            VBInput.gameObject.SetActive(true);
            if (gameObject.tag.Equals("VisitorBook"))
            {
                FriendVisitorBookList();              //친구 방명록 있나 확인
            }
            
        }
        else                                                        //내 씬이냐
        {
          
            VBInput.gameObject.SetActive(false);
            if (gameObject.tag .Equals( "VisitorBook"))
            {
                VisitorBookList();              //방명록 있나 확인
            }
        }
    }
    // Start is called before the first frame update
    public void VisitorBookList()  //내 방명록 불러옴
    {

        WWWForm form = new WWWForm();
        form.AddField("order", "getMessage");
        form.AddField("player_nickname", GameManager.NickName);
        //form.AddField("message", VBInput.text);
        StartCoroutine(GetPost(form));
    }

    public void FriendVisitorBookList()         //친구 방명록 불러옴 
    {

        WWWForm form = new WWWForm();
        form.AddField("order", "getMessageFriend");
        form.AddField("friend_nickname", GameManager.friend_nickname);
        //form.AddField("message", VBInput.text);
        StartCoroutine(GetPost(form));
    }

    public void VisitorBookWrite()          //방명록 쓰기        (보내기 버튼에 넣기)
    {
   
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


        }
        GameObject VB = Instantiate(VBPrefab, Content.transform) as GameObject;

        Text[] VBtext = VB.GetComponentsInChildren<Text>();

        VBtext[0].text = GameManager.NickName;
        VBtext[1].text = VBInput.text;
        VBtext[2].text = DateTime.Now.ToString("yyyy.MM.dd");
        //GameManager.FriendBuildingList.Add(b);      //친구의 건물 리스트에 삽입
    }
    IEnumerator GetPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
        
            if (www.isDone) Response(www.downloadHandler.text);         //방명록 불러옴

        }
    }

    void Response(string json)                          
    {
       
        if (string.IsNullOrEmpty(json))
        {
         
            return;
        }
        if (json .Equals( "null")   )                          //방명록에 아무것도 없다
            return;

        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);

      
        for (int i = 0; i < j.Count; i++)
        {
 
            VisitorBook friendBuildings;
            friendBuildings = JsonUtility.FromJson<VisitorBook>(j[i].ToString());

            GameObject VB = Instantiate(VBPrefab, Content.transform)as GameObject;

            Text[] VBtext = VB.GetComponentsInChildren<Text>();

            VBtext[0].text =friendBuildings.f_nickname;
            VBtext[1].text = friendBuildings.f_message;
            VBtext[2].text = friendBuildings.f_time;

            Image[] Images= VB.GetComponentsInChildren<Image>();
            for (int k = 0; k < GameManager.AllNuniArray.Length; k++)
            {
                if (GameManager.AllNuniArray[k].Image.name != friendBuildings.f_image)
                    continue;
                else
                {
                    Images[1].sprite = GameManager.AllNuniArray[k].Image;
                    return;
                }
               
            }
        }
        

    }
}
