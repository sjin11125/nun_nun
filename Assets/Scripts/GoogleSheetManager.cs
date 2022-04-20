
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value;
}


public class GoogleSheetManager : MonoBehaviour
{
    string URL = GameManager.URL;
        public GoogleData GD;
    public InputField IDInput, PassInput, NicknameInput;
    string id, pass,nickname;
    public QuestManager QuestManager;


    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();
        if (id == "" || pass == "") return false;
        else return true;
    }

    bool SetSignPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();
        nickname = NicknameInput.text.Trim();
        if (id == "" || pass == "" || nickname == "") return false;
        else return true;
    }
    public void Register()
    {
        if (!SetSignPass())
        {
            print("아이디 or 비번 or 닉네임이 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);
        form.AddField("player_nickname", nickname);

        StartCoroutine(Post(form));
    }


    public void Login()
    {
        if (!SetIDPass())
        {
            print("아이디 또는 비밀번호가 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }


    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        StartCoroutine(Post(form));
    }


    public void SetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("value", NicknameInput.text);

        StartCoroutine(Post(form));
    }


    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }





    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone)
            {
                Response(www.downloadHandler.text);
                Debug.Log("응답잇다");
            }
            else print("웹의 응답이 없습니다.");
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log(json);
            return;
        }
        Debug.Log(json);
        GD = JsonUtility.FromJson<GoogleData>(json);
        //System.Text.Encoding.UTF8.GetString(GD, 3, GD.Length - 3);
        

        if (GD.result == "ERROR")
        {
            print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
            return;
        }
        else if(GD.result == "NickNameERROR")
        {
            print("닉네임이 중복됩니다.");
        }
        if (GD.result == "OK")
        {
            if (GD.msg=="회원가입 완료")
            {
                Debug.Log("회원가입 완료!");
            }
            else
            {
                nickname= GD.msg;
                Debug.Log("로그인 완료!");
            }
            print(nickname + "(" + id + ")님 환영합니다!! ");

            GameManager.NickName = nickname;
            GameManager.Id = id;

            QuestManager.QuestStart();                                  //퀘스트 설정

            gameObject.GetComponent<BuildingSave>().BuildingLoad();         //내 건물 불러와
                                                                            //퀘스트 진행상황 불러

            
            //MyBuildingLoad.BuildingLoad();          

            return;
        }
        if (GD.order == "getValue")
        {
            NicknameInput.text = GD.value;
        }

        Debug.Log("");
    }
}
