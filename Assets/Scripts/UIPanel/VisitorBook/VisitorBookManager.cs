using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

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
    public VisitorBook VB;

    public GameObject VBPrefab;             //방명록 목록 프리팹

    public InputField VBInput;
    public GameObject LoadingNuni;      //로딩 누니 프리팹
    public Button CloseBtn;

    public void Start()
    {
        LoadingNuni = Instantiate(GameManager.Instance.TopCanvas);


    }
 
   

    void Response(string json)                          
    {
       
        if (string.IsNullOrEmpty(json))
        {
         
            return;
        }
        if (json.Equals("null"))                          //방명록에 아무것도 없다
        {
            Destroy(LoadingNuni);
            return;
        }

        Newtonsoft.Json.Linq.JArray j = Newtonsoft.Json.Linq.JArray.Parse(json);
        Debug.Log("j길이: "+j.Count);
      
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
            Debug.Log("friendBuildings.f_image: "+ friendBuildings.f_image);
            for (int k = 0; k < GameManager.AllNuniArray.Length; k++)
            {
                if (GameManager.AllNuniArray[k].cardImage != friendBuildings.f_image)
                    continue;
                else
                {
                    Images[1].sprite = GameManager.AllNuniArray[k].Image;
                    
                }
               
            }
        }
        Debug.Log("The End");
        Destroy(LoadingNuni);
    }
}
