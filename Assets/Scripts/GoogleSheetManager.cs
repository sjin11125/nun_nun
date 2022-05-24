
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value,nickname,state, profile_image;
}


public class GoogleSheetManager : MonoBehaviour
{
    string URL = GameManager.URL;
    public GoogleData GD;
    public InputField IDInput, PassInput, NicknameInput;
    string id, pass, nickname,statemessage;
    public QuestManager QuestManager;
    public NuniManager NuniManager;
    public BuildingSave MyBuildingLoad;

    public GameObject WarningPannel;

    private void Awake()
    {        
        TutorialsManager.itemIndex = PlayerPrefs.GetInt("TutorialsDone");
        if (TutorialsManager.itemIndex > 13)
        {
            WWWForm form = new WWWForm();
            form.AddField("order", "login");
            form.AddField("id", PlayerPrefs.GetString("Id"));
            form.AddField("pass", PlayerPrefs.GetString("Pass"));

            StartCoroutine(Post(form));

        }
        else
        {
            Debug.Log("튜토함: "+ TutorialsManager.itemIndex);
            GameManager.Money = 2000;
            GameManager.ShinMoney = 0;
            
        }

    }

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
            WarningPannel.SetActive(true);
            Text t = WarningPannel.GetComponentInChildren<Text>();
            t.text = "아이디 또는 비밀번호 또는 닉네임이 비어있습니다";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);
        form.AddField("player_nickname", nickname);

        StartCoroutine(SignPost(form));
    }


    public void Login()
    {
        if (!SetIDPass())
        {
            WarningPannel.SetActive(true);
            Text t = WarningPannel.GetComponentInChildren<Text>();
            t.text = "아이디 또는 비밀번호가 비어있습니다";
           // print("아이디 또는 비밀번호가 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);
        //form.AddField("player_nickname", nickname);

        StartCoroutine(Post(form));

        PlayerPrefs.SetString("Id", id);
        PlayerPrefs.SetString("Pass", pass);
        PlayerPrefs.SetString("Nickname", nickname);
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



    IEnumerator SignPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);

            GameManager.NickName = nickname;
            GameManager.Id = id;
            Response(www.downloadHandler.text);
            //StartCoroutine(Quest());
            //SceneManager.LoadScene("Main");
        }
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
        WarningPannel.SetActive(true);

        Text t = WarningPannel.GetComponentInChildren<Text>();
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
            t.text=GD.msg;
            return;
        }
        else if (GD.result == "NickNameERROR")
        {
            t.text = "닉네임이 중복됩니다.";
        }
        if (GD.result == "OK")
        {
            if (GD.msg == "회원가입 완료")
            {
                t.text = "회원가입 완료!"+ nickname + "(" + id + ")님 환영합니다!! " +
                    "\n잠시만 기다려 주세요.";
            }
            else
            {
                nickname = GD.nickname;
               GameManager.StateMessage= GD.state;
                t.text = "로그인 완료!"+ nickname + "(" + id + ")님 환영합니다!! " +
                    "\n잠시만 기다려 주세요 ";
            }

            GameManager.NickName = nickname;
            GameManager.Id = id;

            Debug.Log("프로필 이미지: "+ GD.profile_image);
            for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
            {
                if (GameManager.AllNuniArray[i].Image.name != GD.profile_image)
                    continue;
                GameManager.ProfileImage = GameManager.AllNuniArray[i].Image;
            }
             
            StartCoroutine(Quest());
            


               

            return;
        }
        if (GD.order == "getValue")
        {
            NicknameInput.text = GD.value;
        }

        Debug.Log("");
    }
    IEnumerator Quest()
    {
       // gameObject.GetComponent<BuildingSave>().BuildingLoad();         //내 건물 불러와
        //yield return StartCoroutine( QuestManager.QuestStart()); //퀘스트 설정할 때까지 대기
        yield return StartCoroutine(NuniManager.NuniStart()); //누니 설정할 때까지 대기
       // yield return StartCoroutine(NuniManager.RewardStart()); //보상 일괄수령 설정할 때까지 대기
MyBuildingLoad.BuildingLoad();
    }
}
