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
    
    public IEnumerator NuniStart()          //시작할 때 구글 스크립트에서 누니 목록 불러옴
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "nuniGet");
        form1.AddField("player_nickname", GameManager.NickName);




        yield return StartCoroutine(NuniPost(form1));                        //구글 스크립트로 초기화했는지 물어볼때까지 대기

    }
    IEnumerator NuniPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
           /* if (www.isDone) ResponseNuni(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");*/
        }

    }
    /*void ResponseNuni(string json)                          
    {
       
        if (json .Equals( "null"))
        {
            return;
        }
        if (string.IsNullOrEmpty(json))
        {
            return;
        }
        
        string[] Nunis = json.Split(',');//게임매니저에 있는 모든 누니배열에서 해당 누니 찾아서 가지고 있는 누니 배열에 넣기

        
        for (int j = 0; j < Nunis.Length; j++)
        {
            
            string[] Nunis_nuni = Nunis[j].Split(':');
            for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
            {
                if (GameManager.AllNuniArray[i].cardName .Equals( Nunis_nuni[0]))
                {
                    Card nuni = new Card();
                    nuni.SetValue( GameManager.AllNuniArray[i]);
                    if (Nunis_nuni[1] .Equals( "T"))
                    {
                        nuni.isLock = "T";

                    }
                    else
                        nuni.isLock = "F";
                    GameManager.Instance.CharacterList.Add(nuni);
                    break;
                  
                }
            }
           

        }
 
        SceneManager.LoadScene("Main");
    }*/

}
