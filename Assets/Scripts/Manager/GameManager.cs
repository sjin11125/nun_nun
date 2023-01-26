using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using UniRx;

public class GameManager : MonoBehaviour
{
   public static string NewVersion = "";                   //최신버전
    public static string CurVersion = "1.4.6";                    //현재버전
    public static bool isUpdateDone = false;                    //업데이트를 완료했냐

    public Queue<string> Queue;

    public static bool isStart = false;
    public static GameManager _Instance;
    public static bool parse = false;
    public Sprite[] DogamChaImageInspector;     //인스펙터에서 받아 온 건물 이미지

    public static Sprite[] DogamChaImage;       //
    public static Dictionary<string, Sprite> DogamChaImageData;     //건물 이미지 딕셔너리


    public static List<Building> FriendBuildingList;          //친구가 가지고 있는 빌딩들
    public static Building[] BuildingArray;         //모든 빌딩들 정보
    public static Building[] StrArray;         //모든 구조물들 정보

    public GameObject[] BuildingPrefabInspector;    //인스펙터에서 받아 온 건물 프리팹 배열    

    public static Dictionary<string, GameObject> BuildingPrefabData;    //모든 빌딩 프리팹 딕셔너리


    public static bool isEdit = false;
    public static bool isInvenEdit = false;
    public static Button InvenButton;

    public static List<string> IDs;        //건물 아이디
    public static bool isUpdate = false;        //건물 목록 강제로 업데이트

    public static bool isLoading = false;           //건물 다 로딩됐는지
    //----------------------------------------------------이까지 건물----------------------------------------------------


    //----------------------------------------------------여기서부터 누니--------------------------------------------------
    [Header("누니")]
    public Sprite[] CharacterImageInspector;            // 인스펙터에서 받아 온 모든 누니들의 이미지
    public static Dictionary<string, Sprite> CharacterImageData;

    public GameObject[] CharaterPrefabInspector;        // 인스펙터에서 받아 온 모든 누니들의 프리팹
    public static Dictionary<string, GameObject> CharacterPrefab;       //모든 캐릭터 누니 딕셔너리

    public static Card[] AllNuniArray;              //엑셀에서 받아 온 모든 누니 정보 배열

    public Dictionary<string,Card> CharacterList;      //현재가지고 있는 누니 리스트
    //public static Card[] CharacterArray;               //현재 가지고 있는 캐릭터 배열
    
    
    public static bool[] Items=new bool[10];     //현재 가지고 잇는 아이템 유무
    public static int items=0;
    public static bool isStore = false;

    public GameObject Dont;
    public static bool nuniDialogParse = false;

    public static List<NuniDialog> NuniDialog;          //누니 상호작용 대화 
    public Card CurrentNuni;
    //-----------------------------------여기서부터 재화---------------------------------
    public static int Money;            //재화
    public static int ShinMoney;
    public static int Zem;


    //---------------------------------------------------------------------------------------------
    //--------------------------------여기서부터 플레이어 정보-------------------------------------
    [Header("유저정보")]
    [SerializeField]
    public UserInfo PlayerUserInfo;         //플레이어 유저 정보

    public static string NickName;      //플레이어 닉네임
    public static string StateMessage;      //플레이어 상태메세지
    public static string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public ReactiveProperty< Sprite> ProfileImage=new ReactiveProperty<Sprite>();       //플레이어 프로필 이미지
    public ReactiveProperty<int> sadfa=new ReactiveProperty<int>();       //플레이어 프로필 이미지

    public static FriendInfo[] Friends;       //친구 목록(닉네임)

    public static string friend_nickname;       //현재 들어가있는 친구닉넴


    public static bool isReward;        //일괄수확 가능한지

    //----------------------------------------------------------------------------------------------
    //------------------------------여기서부터 게임 정보----------------------------------------------
    [Header("게임정보")]
    public static int BestScore;                //불러온 최고점수
    public static bool isBScore;                    //스코어 업데이트

    public static bool isMoveLock = false;      //창 떴을 때 이동 못하게하는 변수
    /* 아이템 목록
     * 0: 지우개               (황제)
     * 1: 킵                   (비서)
     * 2: 쓰레기통             (청소부)
     * 3: 미리보기             (탐정)
     * 4: 새로고침             (개발자)
     * 5: <=>                  (과학자)
     * 6: 가로3개              (팡팡)
     * 7: 세로3개              (펑펑)
     * 8: 모든 대체할수 있는 말(유니콘)
     * 9: 말의 색깔을 바꾼다   (마법사)
     */
    // Start is called before the first frame update

    //--------------------------------------------------------------------퀘스트---------------------------------------------------
    [Header("퀘스트")]

    public static bool isStrEdit = false;

    public static bool gameMusicOn = true;
    public static bool mainMusicOn = true;
    public static bool gameSoundOn = true;
    public static bool mainSoundOn = true;

    //--------------------------------------------공지----------------------------------------
    [Header("공지")]
    public static Notice[] Notice;

    //--------------------------------------------업적---------------------------------------
    public Dictionary<string, AchieveInfo> AchieveInfos = new Dictionary<string, AchieveInfo>();      //업적 정보 딕셔너리
    public Dictionary<string, MyAchieveInfo> MyAchieveInfos=new Dictionary<string, MyAchieveInfo>();      //내 업적 정보 딕셔너리


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (_Instance == null)
        {
            _Instance = this;
            isStart = true;
        }
        else if (_Instance != this) // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.

    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    void Start()
    {

        //
        DogamChaImageData = new Dictionary<string, Sprite>();       //전체 캐릭터 리스트(가지고 있지 않은것도 포함)
        BuildingPrefabData = new Dictionary<string, GameObject>();      //전체 빌딩 프리팹 리스트 (가지고 있지 않은 것도 포함)

        CharacterPrefab = new Dictionary<string, GameObject>();
        CharacterImageData = new Dictionary<string, Sprite>();
        CharacterList = new Dictionary<string, Card>();
        IDs = new List<string>();                   //퀘스트 
        NuniDialog = new List<NuniDialog>();


        for (int i = 0; i < BuildingPrefabInspector.Length; i++)        //빌딩 프리팹 정보 불러오기
        {
            BuildingPrefabData.Add(BuildingPrefabInspector[i].name + "(Clone)", BuildingPrefabInspector[i]);


        }


        for (int i = 0; i < DogamChaImageInspector.Length; i++)     // 빌딩 이미지 불러오기
        {
            DogamChaImageData.Add(DogamChaImageInspector[i].name, DogamChaImageInspector[i]);
        }
        for (int i = 0; i < CharacterImageInspector.Length; i++)        //캐릭터 
        {
            CharacterImageData.Add(CharacterImageInspector[i].name, CharacterImageInspector[i]);
        }
        for (int i = 0; i < CharaterPrefabInspector.Length; i++)
        {
            CharacterPrefab.Add(CharaterPrefabInspector[i].name, CharaterPrefabInspector[i]);
        }

        if (SceneManager.GetActiveScene().name.Equals("Main"))
        {
            //Dont.SetActive(true);
        }

        //게임 시작했을 때 엑셀에서 모든 누니 정보들 파싱해 배열에 넣기


        DicParsingManager DPManager = new DicParsingManager();
        AllNuniArray = DPManager.Parse_character(1);            //누니 정보 파싱
        BuildingArray = DPManager.Parse(0);    //도감 정보 파싱


    }

    public static GameManager Instance
    {
        get
        {
            if (_Instance ==null)
            {
                return null;
            }
            return _Instance;
        }
    }
    
    public static Sprite GetDogamChaImage(string ImageName)
    {
        return DogamChaImageData[ImageName.Trim()];

    }
    public static Sprite GetCharacterImage(string ImageName)
    {
        
        return CharacterImageData[ImageName.Trim()];
    }

    public string IDGenerator()
    {
        string alpha= "qwertyuipoasdfjkl123456789!@#$%^&*()";
        string id="";

        bool isCount = false;
        do
        {

            for (int i = 0; i < 5; i++)
            {
                id += alpha[Random.Range(0, 24)];
            }
            for (int i = 0; i < IDs.Count; i++)
            {
                if (IDs[i] .Equals( id))
                {
                    isCount = false;
                }
                else
                {
                    isCount = true;
                    IDs.Add(id);
                }
            }
        } while (isCount .Equals( true));
        return id;
    }

    

    public void GameSave()
    {
        /* PlayerPrefs.SetInt("Money", Money);
         PlayerPrefs.SetInt("ShinMoney", ShinMoney);
         PlayerPrefs.Save();
         print("save");

         WWWForm form2 = new WWWForm();
         //isMe = true;                 
         form2.AddField("order", "setMoney");
         form2.AddField("version", GameManager.CurVersion);

         form2.AddField("achieve", string.Join(",", CanvasManger.currentAchieveSuccess));
         form2.AddField("index", string.Join(",", CanvasManger.achieveContNuniIndex));
         form2.AddField("count", string.Join(",", CanvasManger.achieveCount));

         form2.AddField("shopbuy", string.Join(",", ShopBuyScript.Achieve12));
         form2.AddField("achieveMoney", string.Join(",", CanvasManger.AchieveMoney));
         form2.AddField("achieveShinMoney", string.Join(",", CanvasManger.AchieveShinMoney));
         form2.AddField("achieveNuniName", string.Join(",", CardUI.AchieveNuniName));
         form2.AddField("achieveFriendCount", string.Join(",", CanvasManger.AchieveFriendCount));

         form2.AddField("player_nickname", NickName);  */
        FirebaseLogin.Instance.SetUserInfo(GameManager.Instance.PlayerUserInfo);
    }
 public void UpdateMyAchieveInfo(string id,int count)
    {
        if (GameManager.Instance.MyAchieveInfos.ContainsKey(id))      //클리어한 업적 중에 해당 업적 Id가 있으면
        {
            GameManager.Instance.MyAchieveInfos[id].Count += count;
            if (GameManager.Instance.MyAchieveInfos[id].Count 
                >= GameManager.Instance.AchieveInfos[id].Count[GameManager.Instance.MyAchieveInfos[id].Index] )
            {
                GameManager.Instance.MyAchieveInfos[id].isReward[GameManager.Instance.MyAchieveInfos[id].Index] = "true";
            }  
        }
        else
        {
            MyAchieveInfo NewMyAchieveInfo = new MyAchieveInfo(new string[] { "false", "false", "false", "false", "false" },
                                                                id, 0, count,GameManager.Instance.PlayerUserInfo.Uid);

            GameManager.Instance.MyAchieveInfos.Add(id, NewMyAchieveInfo);

            // GameManager.Instance.MyAchieveInfos[achieveId[0][i]].CountRP.Value += 5;
        }
    }
   
    void OnApplicationPause(bool pause)
    {
        if (pause)//비활성화
        {
            GameSave();
        }
    }
}
