
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;
using UniRx;

[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value,nickname,state, profile_image,isUpdate;
    public string LoginResult;
}


public class GoogleSheetManager : MonoBehaviour
{
    public GoogleData GD;
    public InputField IDInput, PassInput, NicknameInput;
    string id, pass, nickname, statemessage;
    public QuestManager QuestManager;
    public NuniManager NuniManager;
    public BuildingSave MyBuildingLoad;

    public GameObject WarningPannel;
    public bool ifISign;

    public Button LogInBtn;
    public Button SignInBtn;
    Text WarningTxt;
    public string bestScoreKey_ = "bsdat";
    private BestScoreData bestScores_ = new BestScoreData();

    public GameObject VersionCheckPrefab;

    public GameObject UpdatePanel;
    private void Awake()
    {
        /*  VersionCheckPrefab.SetActive(true);
          WWWForm form = new WWWForm();
          form.AddField("order", "isUpdate");

          StartCoroutine(VersionPost(form));*/
        GameManager.isUpdateDone = true;
        if (BinaryDataStream.Exist(bestScoreKey_))
        {
            //StartCoroutine(ReadDataFile());
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
                /*WWWForm form1 = new WWWForm();                          //자원 부르기
                form1.AddField("order", "getMoney");
                form1.AddField("player_nickname", GameManager.NickName);
                StartCoroutine(MoneyPost(form1));*/
                TutorialsManager.itemIndex = PlayerPrefs.GetInt("TutorialsDone");
            }
        }
    }
   
    void Start()
    {
        /* WWWForm form = new WWWForm();
         form.AddField("order", "isUpdate");

         StartCoroutine(VersionPost(form)); //최신 버전 불러오기*/
        WarningTxt = WarningPannel.GetComponentInChildren<Text>();
       //구글 로그인 
       //게스트 로그인 넣기
      /*  LogInBtn.onClick.AsObservable().Subscribe(_
            =>
            {
                LogInBtn.gameObject.SetActive(false);
                WarningTxt.text = "로딩중";
                Login();
            }).AddTo(LogInBtn.gameObject);

        SignInBtn.onClick.AsObservable().Subscribe(_
            =>
            {
                SignInBtn.gameObject.SetActive(false);
                WarningTxt.text = "로딩중";
                Register();
            }).AddTo(SignInBtn.gameObject);*/
    }
    /*IEnumerator VersionPost(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            
            VersionResponse(www.downloadHandler.text);
            //StartCoroutine(Quest());
            //SceneManager.LoadScene("Main");
        }
    }
    */
    void VersionResponse(string json)
    {
        GameManager.NewVersion = json;                  //최신버전 확인
        Debug.Log(GameManager.NewVersion);
        if (GameManager.CurVersion!=json)           //최신버전이 아니면 업데이트 패널뜨게
        {
            VersionCheckPrefab.SetActive(false);
            GameManager.isUpdateDone = false;
            UpdatePanel.SetActive(true);
        }
        else
        {
            VersionCheckPrefab.SetActive(false);
            GameManager.isUpdateDone = true;
        }
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

  /*  public void Register()//회원가입
    {
       

        if (!SetSignPass())
        {
            SignInBtn.gameObject.SetActive(true);
            WarningTxt.text = "아이디 또는 비밀번호 또는 닉네임이 비어있습니다";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);
        form.AddField("player_nickname", nickname);
        form.AddField("version", GameManager.CurVersion);


       // StartCoroutine(SignPost(form));

        PlayerPrefs.SetString("Id", id);//아이디비번 저장
        PlayerPrefs.SetString("Pass", pass);

        TutorialsManager.itemIndex = 0;//초기화

        WWWForm form2 = new WWWForm();                      //돈 저장
        //isMe = true;                 
        form2.AddField("order", "setMoney");
        form2.AddField("player_nickname", GameManager.NickName);

        form.AddField("version", GameManager.CurVersion);
        GameManager.Money = 2000;
        GameManager.ShinMoney = 0;

        
        form2.AddField("money", GameManager.Money.ToString() + "@" + GameManager.ShinMoney.ToString()+ "@" + TutorialsManager.itemIndex + "@" + GameManager.BestScore.ToString() + "@" + GameManager.Zem.ToString());
        form.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
        form.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
        form.AddField("count", string.Join(",", CanvasManger.achieveCount));

        form.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
        form.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
        form.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
        form.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
        form.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));
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

   /* public void Login()
    {
        if (GameManager.isUpdateDone == true)
        {
            WarningPannel.SetActive(true);
            Text t = WarningPannel.GetComponentInChildren<Text>();
            t.text = "로딩중";
            if (!SetIDPass())
            {
                t.text = "아이디 또는 비밀번호가 비어있습니다";
                return;
            }
            WWWForm form = new WWWForm();
            form.AddField("order", "login");
            form.AddField("id", IDInput.text);
            form.AddField("pass", PassInput.text);
            form.AddField("version", GameManager.CurVersion);

            PlayerPrefs.SetString("Id", id);//아이디비번 저장
            PlayerPrefs.SetString("Pass", pass);

            StartCoroutine(Post(form));
        }
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
   */


  /*  IEnumerator SignPost(WWWForm form)
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
  */

    void Response(string json)
    {
        Debug.Log(json);
        

        Text t = WarningPannel.GetComponentInChildren<Text>();
        if (string.IsNullOrEmpty(json))
        {
            return;
        }
        GD = JsonUtility.FromJson<GoogleData>(json);
        //System.Text.Encoding.UTF8.GetString(GD, 3, GD.Length - 3);
        switch (GD.LoginResult)
        {
            case nameof(LoginResult.ERROR):
                t.text = "아이디 혹은 비밀번호가 유효하지 않습니다.";
                LogInBtn.gameObject.SetActive(true);
                break;

            case nameof(LoginResult.NickNameERROR):
                t.text = GD.msg;
                SignInBtn.gameObject.SetActive(true);
                break;

            case nameof(LoginResult.SignUpOK):

                t.text = "회원가입 완료!" + nickname + "(" + id + ")님 환영합니다!! " +
                    "\n잠시만 기다려 주세요.";
                LoginProccess();
                break;

            case nameof(LoginResult.LoginOK):

                nickname = GD.nickname;
                GameManager.StateMessage = GD.state;
                t.text = "로그인 완료!" + nickname + "(" + id + ")님 환영합니다!! " +
                    "\n잠시만 기다려 주세요 ";
                LoginProccess();

                break;
            default:
                break;
        }
        #region 예전 코드
        /*
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
                form.AddField("version", GameManager.CurVersion);


                string tempMoney = PlayerPrefs.GetInt("Money").ToString() + "@" + PlayerPrefs.GetInt("ShinMoney").ToString() + "@" + PlayerPrefs.GetInt("TutorialsDone").ToString() + "@" + bestScores_.score.ToString() + "@" + GameManager.Zem.ToString();
                form.AddField("money", tempMoney);
                form.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
                form.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
                form.AddField("count", string.Join(",", CanvasManger.achieveCount));

                form.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
                form.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
                form.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
                form.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
                form.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));
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
        }*/
        #endregion

    }

    public void LoginProccess()
    {
        GameManager.NickName = nickname;
       // GameManager.Id = id;

        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            if (GameManager.AllNuniArray[i].Image.name != GD.profile_image)
                continue;
            //GameManager.Instance.ProfileImage = GameManager.AllNuniArray[i].Image;
        }

        if (GD.isUpdate == "null")            //업데이트를 안한 상태인가
        {
            Debug.Log("업뎃안함");
            WWWForm form = new WWWForm();
            form.AddField("order", "setMoney");
            form.AddField("player_nickname", GameManager.NickName);
            form.AddField("version", GameManager.CurVersion);


            string tempMoney = PlayerPrefs.GetInt("Money").ToString() + "@" + PlayerPrefs.GetInt("ShinMoney").ToString() + "@" + PlayerPrefs.GetInt("TutorialsDone").ToString() + "@" + bestScores_.score.ToString() + "@" + GameManager.Zem.ToString();
            form.AddField("money", tempMoney);
            form.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
            form.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
            form.AddField("count", string.Join(",", CanvasManger.achieveCount));

            form.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
            form.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
            form.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
            form.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
            form.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));
            form.AddField("isUpdate", "true");


           // StartCoroutine(SetPost(form));
        }
        else if (GD.isUpdate == "true")  //최고기록 저장을 아직 안했는가
        {
            WWWForm form1 = new WWWForm();                          //자원을 먼저 부르고 저장한 다음 최고점수 저장
            form1.AddField("order", "getMoney");
            form1.AddField("player_nickname", GameManager.NickName);
           // StartCoroutine(MoneyPost(form1));

        }
        else
        {
            WWWForm form1 = new WWWForm();                          //자원 부르기
            form1.AddField("order", "getMoney");
            form1.AddField("player_nickname", GameManager.NickName);
           // StartCoroutine(MoneyPost(form1));
        }
    }
  /*  IEnumerator MoneyPost(WWWForm form)
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
    }*/
    public void BestScoreResponse(string json)
    {
        Debug.Log("최고기록 업뎃안함");                                  //서버에 최고기록 저장
        WWWForm form = new WWWForm();
        form.AddField("order", "setMoney");
        form.AddField("player_nickname", GameManager.NickName);
        form.AddField("version", GameManager.CurVersion);

        string tempMoney = GameManager.Money.ToString() + "@" + GameManager.ShinMoney.ToString() + "@" + TutorialsManager.itemIndex.ToString() + "@" + GameManager.BestScore.ToString() + "@" + GameManager.Zem.ToString();
        form.AddField("money", tempMoney);
        form.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
        form.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
        form.AddField("count", string.Join(",", CanvasManger.achieveCount));

        form.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
        form.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
        form.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
        form.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
        form.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));
        form.AddField("isUpdate", "true");

        GD.isUpdate = "null";
     //   StartCoroutine(SetPost(form));


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
            form.AddField("version", GameManager.CurVersion);

            string tempMoney = GameManager.Money.ToString() + "@" + GameManager.ShinMoney.ToString() + "@" + TutorialsManager.itemIndex.ToString() + "@" + GameManager.BestScore.ToString() + "@" + GameManager.Zem.ToString();
            form.AddField("money", tempMoney);
            form.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
            form.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
            form.AddField("count", string.Join(",", CanvasManger.achieveCount));

            form.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
            form.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
            form.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
            form.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
            form.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));
            form.AddField("isUpdate", "true");


           // StartCoroutine(BestScorePost(form));
        }
        else if (GD.isUpdate == "1.4.5")
        {
             
        GameManager.BestScore = int.Parse(json.Split('@')[3]);          //점수설정
        //GameManager.Zem = int.Parse(json.Split('@')[4]);          //잼설정
            WWWForm form = new WWWForm();
            form.AddField("order", "getChallenge");
            form.AddField("player_nickname", GameManager.NickName);

          //  StartCoroutine(ChallPost(form));
        }  else if (GD.isUpdate == "1.4.6")
        {
             
        GameManager.BestScore = int.Parse(json.Split('@')[3]);          //점수설정
        GameManager.Zem = int.Parse(json.Split('@')[4]);          //잼설정
            WWWForm form = new WWWForm();
            form.AddField("order", "getChallenge");
            form.AddField("player_nickname", GameManager.NickName);

         //   StartCoroutine(ChallPost(form));
        }
        
        
        
    }
  
    void GetChallenge()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getChallenge");
        form.AddField("player_nickname", GameManager.NickName);

      // StartCoroutine(ChallPost(form));
    }
  /*  IEnumerator ChallPost(WWWForm form)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {

                ChallResponse(www.downloadHandler.text);
            }
            else print("웹의 응답이 없습니다.");
        }
    }*/
    public void ChallResponse(string json)
    {
        Debug.Log(json);
        if (json != "@@")
        {
            if (GD.isUpdate == "1.4.5")
            {


                for (int i = 0; i < CanvasManger.currentAchieveSuccess.Length; i++)
                {
                    if (i>11)
                    {
                        CanvasManger.currentAchieveSuccess[i] = false;
                    }
                    else
                        CanvasManger.currentAchieveSuccess[i] = System.Convert.ToBoolean(json.Split('@')[0].Split(',')[i]);


                    Debug.Log("CanvasManger.currentAchieveSuccess[" + i + "] : " + CanvasManger.currentAchieveSuccess[i]);

                }
                for (int j = 0; j < CanvasManger.achieveContNuniIndex.Length; j++)
                {
                    if (j > 11)
                    {
                        CanvasManger.achieveContNuniIndex[j] = 0;
                    }
                    else
                        CanvasManger.achieveContNuniIndex[j] = int.Parse(json.Split('@')[1].Split(',')[j]);
                    Debug.Log("CanvasManger.achieveContNuniIndex[" + j + "] : " + CanvasManger.achieveContNuniIndex[j]);
                }
                for (int k = 0; k < CanvasManger.achieveCount.Length; k++)
                {
                    if (k > 11)
                    {
                        CanvasManger.achieveCount[k] = 0;
                    }
                    else
                        CanvasManger.achieveCount[k] = int.Parse(json.Split('@')[2].Split(',')[k]);
                    Debug.Log("CanvasManger.achieveCount[" + k + "] : " + CanvasManger.achieveCount[k]);
                }
            }
            else                //1.4.6
            {
                Debug.Log("버전 1.4.6 입니다");
                for (int i = 0; i < CanvasManger.currentAchieveSuccess.Length; i++)
                {
                   
                        CanvasManger.currentAchieveSuccess[i] = System.Convert.ToBoolean(json.Split('@')[0].Split(',')[i]);


                    Debug.Log("CanvasManger.currentAchieveSuccess[" + i + "] : " + CanvasManger.currentAchieveSuccess[i]);

                }
                for (int j = 0; j < CanvasManger.achieveContNuniIndex.Length; j++)
                {
                   
                        CanvasManger.achieveContNuniIndex[j] = int.Parse(json.Split('@')[1].Split(',')[j]);
                    Debug.Log("CanvasManger.achieveContNuniIndex[" + j + "] : " + CanvasManger.achieveContNuniIndex[j]);
                }
                for (int k = 0; k < CanvasManger.achieveCount.Length; k++)
                {
                   
                        CanvasManger.achieveCount[k] = int.Parse(json.Split('@')[2].Split(',')[k]);
                    Debug.Log("CanvasManger.achieveCount[" + k + "] : " + CanvasManger.achieveCount[k]);
                }
                ShopBuyScript.Achieve12 =int.Parse( json.Split('@')[3]);
                CanvasManger.AchieveMoney= int.Parse(json.Split('@')[4]);
                CanvasManger.AchieveShinMoney = int.Parse(json.Split('@')[5]);
                for (int k = 0; k < CardUI.AchieveNuniName.Count; k++)
                {
                    string[] TempArray=new string[CardUI.AchieveNuniName.Count];
                    TempArray[k]= json.Split('@')[6].Split(',')[k];


                    CardUI.AchieveNuniName = TempArray.ToList();
                }
                CanvasManger.AchieveFriendCount = int.Parse(json.Split('@')[7]);
            }
        }
            GetNotice();
        
        //StartCoroutine(Quest());

    }
    void GetNotice()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getNotice");

        form.AddField("player_nickname", GameManager.NickName);

       // StartCoroutine(NoticePost(form));
    }
   /* IEnumerator NoticePost(WWWForm form)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {

                NoticeResponse(www.downloadHandler.text);
            }
            else print("웹의 응답이 없습니다.");
        }
    }*/

}
