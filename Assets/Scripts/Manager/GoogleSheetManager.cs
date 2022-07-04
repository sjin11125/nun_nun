
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value,nickname,state, profile_image,isUpdate;
}


public class GoogleSheetManager : MonoBehaviour
{
    string URL = GameManager.URL;
    public GoogleData GD;
    public InputField IDInput, PassInput, NicknameInput;
    string id, pass, nickname, statemessage;
    public QuestManager QuestManager;
    public NuniManager NuniManager;
    public BuildingSave MyBuildingLoad;

    public GameObject WarningPannel;
    public GameObject retext;
    public bool ifISign;

    public GameObject loginBtn;
    public string bestScoreKey_ = "bsdat";
    private BestScoreData bestScores_ = new BestScoreData();

    private void Awake()
    {
        if (BinaryDataStream.Exist(bestScoreKey_))
        {
            StartCoroutine(ReadDataFile());
        }
        else
        {
            Debug.Log("존재하지않음");
        }
        if (!ifISign)
        {
            if (PlayerPrefs.GetString("Id") != null)//회원가입후에
            {
                IDInput.text = PlayerPrefs.GetString("Id");
                //IDInput.enabled = false;
                PassInput.text = PlayerPrefs.GetString("Pass");
                // PassInput.enabled = false;//인풋필드 못누르게하기

                //GameManager.Money = PlayerPrefs.GetInt("Money");
                //GameManager.ShinMoney = PlayerPrefs.GetInt("ShinMoney");
                WWWForm form1 = new WWWForm();                          //자원 부르기
                form1.AddField("order", "getMoney");
                form1.AddField("player_nickname", GameManager.NickName);
                StartCoroutine(MoneyPost(form1));
                TutorialsManager.itemIndex = PlayerPrefs.GetInt("TutorialsDone");
            }
        }
    }
    void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "isUpdate");

        StartCoroutine(VersionPost(form)); //최신 버전 불러오기
    }
    IEnumerator VersionPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            GameManager.NickName = nickname;
            GameManager.Id = id;
            VersionResponse(www.downloadHandler.text);
            //StartCoroutine(Quest());
            //SceneManager.LoadScene("Main");
        }
    }

    void VersionResponse(string json)
    {
        GameManager.NewVersion = json;                  //최신버전 확인
        Debug.Log(GameManager.NewVersion);
        
    }

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();
        if (id.Equals("") || pass.Equals("")) return false;
        else return true;
    }
    
    bool SetSignPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();
        nickname = NicknameInput.text.Trim();
        if (id.Equals("") || pass.Equals("") || nickname.Equals("")) return false;
        else return true;
    }

    public void SigninBtn()
    {
        if (PlayerPrefs.GetString("Id") != null)//다시 회원가입
        {
            retext.SetActive(true);
        }
    }

    public void Register()//회원가입
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

        PlayerPrefs.SetString("Id", id);//아이디비번 저장
        PlayerPrefs.SetString("Pass", pass);

        TutorialsManager.itemIndex = 0;//초기화

        WWWForm form2 = new WWWForm();                      //돈 저장
        //isMe = true;                 
        form2.AddField("order", "setMoney");
        form2.AddField("player_nickname", GameManager.NickName);

        GameManager.Money = 2000;
        GameManager.ShinMoney = 0;

        
        form2.AddField("money", GameManager.Money.ToString() + "@" + GameManager.ShinMoney.ToString()+ "@" + TutorialsManager.itemIndex);
        form2.AddField("isUpdate", "true");
        StartCoroutine(SetPost(form2));

        //PlayerPrefs.SetInt("TutorialsDone", TutorialsManager.itemIndex);

       

    }

    private IEnumerator ReadDataFile()
    {
        
        bestScores_ = BinaryDataStream.Read<BestScoreData>(bestScoreKey_);
        GameManager.BestScore = bestScores_.score;
        Debug.Log("최고기록: "+bestScores_.score);
        yield return new WaitForEndOfFrame();

    }

    public void Login()//자동 로그인
    {
        if (!SetIDPass())
        {
            WarningPannel.SetActive(true);
            Text t = WarningPannel.GetComponentInChildren<Text>();
            t.text = "아이디 또는 비밀번호가 비어있습니다";

            return;
        }
        else
        {
            loginBtn.SetActive(false);
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", IDInput.text);
        form.AddField("pass", PassInput.text);

   
        PlayerPrefs.SetString("Id", id);//아이디비번 저장
        PlayerPrefs.SetString("Pass", pass);

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



    IEnumerator SignPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

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
            if (www.isDone)
            {
                Response(www.downloadHandler.text);
            }
            else print("웹의 응답이 없습니다.");
        }
    }
    IEnumerator SetPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
            }
            else print("웹의 응답이 없습니다.");
            WWWForm form1 = new WWWForm();                          //자원 부르기
            form1.AddField("order", "getMoney");
            form1.AddField("player_nickname", GameManager.NickName);
            StartCoroutine(MoneyPost(form1));
        }
       
    }


    void Response(string json)
    {
        Debug.Log(json);
        WarningPannel.SetActive(true);

        Text t = WarningPannel.GetComponentInChildren<Text>();
        if (string.IsNullOrEmpty(json))
        {
            return;
        }
        GD = JsonUtility.FromJson<GoogleData>(json);
        //System.Text.Encoding.UTF8.GetString(GD, 3, GD.Length - 3);


        if (GD.result.Equals("ERROR"))
        {
            t.text = GD.msg;
            return;
        }
        else if (GD.result.Equals("NickNameERROR"))
        {
            t.text = "닉네임이 중복됩니다.";
        }
        if (GD.result.Equals("OK"))
        {
            if (GD.msg.Equals("회원가입 완료"))
            {
                t.text = "회원가입 완료!" + nickname + "(" + id + ")님 환영합니다!! " +
                    "\n잠시만 기다려 주세요.";
            }
            else
            {
                nickname = GD.nickname;
                GameManager.StateMessage = GD.state;
                t.text = "로그인 완료!" + nickname + "(" + id + ")님 환영합니다!! " +
                    "\n잠시만 기다려 주세요 ";
            }

            GameManager.NickName = nickname;
            GameManager.Id = id;

            for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
            {
                if (GameManager.AllNuniArray[i].Image.name != GD.profile_image)
                    continue;
                GameManager.ProfileImage = GameManager.AllNuniArray[i].Image;
            }

            if (GD.isUpdate == "null")            //업데이트를 안한 상태인가
            {
                Debug.Log("업뎃안함");
                WWWForm form = new WWWForm();
                form.AddField("order", "setMoney");
                form.AddField("player_nickname", GameManager.NickName);

               

                string tempMoney = PlayerPrefs.GetInt("Money").ToString() + "@" + PlayerPrefs.GetInt("ShinMoney").ToString() + "@" + PlayerPrefs.GetInt("TutorialsDone").ToString() + "@" + bestScores_.score.ToString();
                form.AddField("money", tempMoney);
                form.AddField("isUpdate", "true");


                StartCoroutine(SetPost(form));
            }
            else if(GD.isUpdate == "true")  //최고기록 저장을 아직 안했는가
            {
                WWWForm form1 = new WWWForm();                          //자원을 먼저 부르고 저장한 다음 최고점수 저장
                form1.AddField("order", "getMoney");
                form1.AddField("player_nickname", GameManager.NickName);
                StartCoroutine(MoneyPost(form1));

            }
            else {
                WWWForm form1 = new WWWForm();                          //자원 부르기
                form1.AddField("order", "getMoney");
                form1.AddField("player_nickname", GameManager.NickName);
                StartCoroutine(MoneyPost(form1));
            }
            return;
        }
        if (GD.order.Equals("getValue"))
        {
            NicknameInput.text = GD.value;
        }

    }
    IEnumerator MoneyPost(WWWForm form)
    {
       
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
               
                MoneyResponse(www.downloadHandler.text);
            }
            else print("웹의 응답이 없습니다.");
        }
    }
    IEnumerator BestScorePost(WWWForm form)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {

                BestScoreResponse(www.downloadHandler.text);
            }
            else print("웹의 응답이 없습니다.");
        }
    }
    public void BestScoreResponse(string json)
    {
        Debug.Log("최고기록 업뎃안함");                                  //서버에 최고기록 저장
        WWWForm form = new WWWForm();
        form.AddField("order", "setMoney");
        form.AddField("player_nickname", GameManager.NickName);
        string tempMoney = GameManager.Money.ToString() + "@" + GameManager.ShinMoney.ToString() + "@" + TutorialsManager.itemIndex.ToString() + "@" + GameManager.BestScore.ToString();
        form.AddField("money", tempMoney);
        form.AddField("isUpdate", "true");

        GD.isUpdate = "null";
        StartCoroutine(SetPost(form));


    }
    public void MoneyResponse(string json)
    {
        Debug.Log("돈: " + json);
        if (string.IsNullOrEmpty(json))
        {
            return;
        }

        GameManager.Money = int.Parse(json.Split('@')[0]);          //자원설정
        GameManager.ShinMoney = int.Parse(json.Split('@')[1]);
        TutorialsManager.itemIndex = int.Parse(json.Split('@')[2]);



        if (GD.isUpdate == "true")
        {
            WWWForm form = new WWWForm();
            form.AddField("order", "setMoney");
            form.AddField("player_nickname", GameManager.NickName);
            string tempMoney = GameManager.Money.ToString() + "@" + GameManager.ShinMoney.ToString() + "@" + TutorialsManager.itemIndex.ToString() + "@" + GameManager.BestScore.ToString();
            form.AddField("money", tempMoney);
            form.AddField("isUpdate", "true");


            StartCoroutine(BestScorePost(form));
        }
        else { 
        GameManager.BestScore = int.Parse(json.Split('@')[3]);          //점수설정
        StartCoroutine(Quest());
    }
        
        
        
    }
    IEnumerator Quest()
    {
        // gameObject.GetComponent<BuildingSave>().BuildingLoad();         //내 건물 불러와
        //yield return StartCoroutine( QuestManager.QuestStart()); //퀘스트 설정할 때까지 대기
        //yield return StartCoroutine(IsUpdate());
        yield return StartCoroutine(NuniManager.NuniStart()); //누니 설정할 때까지 대기
                                                              // yield return StartCoroutine(NuniManager.RewardStart()); //보상 일괄수령 설정할 때까지 대기
        MyBuildingLoad.BuildingLoad();
    }

    
}
