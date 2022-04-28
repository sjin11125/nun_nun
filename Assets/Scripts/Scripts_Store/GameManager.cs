using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public static bool isStart = false;
    static GameManager _Instance;
    public static bool parse = false;
    public Sprite[] DogamChaImageInspector;     //인스펙터에서 받아 온 건물 이미지

    public static Sprite[] DogamChaImage;
    public static Dictionary<string, Sprite> DogamChaImageData;

    public static List<Building> BuildingList;          //가지고 있는 빌딩들
    public static List<Building> FriendBuildingList;          //친구가 가지고 있는 빌딩들
    public static Building[] BuildingArray;         //모든 빌딩들
    
    public GameObject[] BuildingPrefabInspector;    //인스펙터에서 받아 온 건물 프리팹 배열
    public static Dictionary<string, GameObject> BuildingPrefabData;    //모든 빌딩 프리팹 딕셔너리

    public static GameObject CurrentBuilding;       //현재 수정중인 건물
    public static Building CurrentBuilding_Script;       //현재 수정중인 건물

    public static Dictionary<string, int> BuildingNumber;            //건물이 종류별로 몇개 있는지(건물번호)

    public static bool isEdit = false;
    //----------------------------------------------------이까지 건물----------------------------------------------------


    //----------------------------------------------------여기서부터 캐릭터--------------------------------------------------
    public Sprite[] CharacterImageInspector;            // 인스펙터에서 받아 온 모든 캐릭터들의 이미지
    public static Dictionary<string, Sprite> CharacterImageData;

    public GameObject[] CharaterPrefabInspector;        // 인스펙터에서 받아 온 모든 캐릭터들의 프리팹
    public static Dictionary<string, GameObject> CharacterPrefab;       //모든 캐릭터 프리팹 딕셔너리

    public static Card[] AllNuniArray;              //엑셀에서 받아 온 모든 누니 정보 배열

    public static List<Card> CharacterList;      //현재가지고 있는 캐릭터 리스트
    //public static Card[] CharacterArray;               //현재 가지고 있는 캐릭터 배열
    

    public static bool[] Items=new bool[10];     //현재 가지고 잇는 아이템 유무
    public static int items=0;
    public static bool isStore = false;

    public GameObject Dont;


    //-----------------------------------여기서부터 재화---------------------------------
    public static int Money = 10000;            //재화
    public static int ShinMoney = 100000;

    //---------------------------------------------------------------------------------------------
    //--------------------------------여기서부터 플레이어 정보-------------------------------------

    public static string Id;            //플레이어 아이디
    public static string NickName;      //플레이어 닉네임
    public static string StateMessage;      //플레이어 상태메세지
    public static string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public static Sprite ProfileImage;       //플레이어 프로필 이미지

    public static FriendInfo[] Friends;       //친구 목록(닉네임)

    public static string URL = "https://script.google.com/macros/s/AKfycbzfVZroAqIv4t6fLITKFYAqHNK3nP5Y_jLto__m6O2YwePXhZe9r0lHQSjiCeTLYeXs/exec";
    //----------------------------------------------------------------------------------------------


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

   public static QuestInfo[] Quest;                 //퀘스트 목록
    public static QuestInfo[] QuestProgress;        //퀘스트 진행상황
    public static bool isReset;             //퀘스트 초기화 햇니?
    void Start()
    {
        BuildingList = new List<Building>();            //현재 가지고 있는 빌딩 리스트
        //
        DogamChaImageData = new Dictionary<string, Sprite>();       //전체 캐릭터 리스트(가지고 있지 않은것도 포함)
        BuildingPrefabData = new Dictionary<string, GameObject>();      //전체 빌딩 프리팹 리스트 (가지고 있지 않은 것도 포함)
        CharacterPrefab = new Dictionary<string, GameObject>();
        CharacterImageData = new Dictionary<string, Sprite>();
        CharacterList = new List<Card>();
        BuildingNumber = new Dictionary<string, int>();

        Quest = new QuestInfo[3];                     //퀘스트 

        Debug.Log("GameManager Start");
        for (int i = 0; i < BuildingPrefabInspector.Length; i++)        //빌딩 프리팹 정보 불러오기
        {
            BuildingPrefabData.Add(BuildingPrefabInspector[i].name+ "(Clone)", BuildingPrefabInspector[i]);
            if (BuildingPrefabInspector[i].GetComponent<Building>().Button_Pannel == null)
            {
                Debug.Log(i);
                Debug.Log("없");
            }
            else
            {
                Debug.Log(i);
                Debug.Log("있");
            }
           
        }
        //일단 시작하면 전체 빌딩 프리팹 리스트에서 이름 받아서 임시로 0으로 초기화
        for (int i = 0; i < BuildingPrefabInspector.Length; i++)
        {
            BuildingNumber.Add(BuildingPrefabInspector[i].name + "(Clone)",0);
        }

        for (int i = 0; i < DogamChaImageInspector.Length; i++)     // 빌딩 이미지 불러오기
        {
            Debug.Log(DogamChaImageInspector[i].name);
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

        if (SceneManager.GetActiveScene().name=="Main")
        {
            //Dont.SetActive(true);
        }

        //게임 시작했을 때 엑셀에서 모든 누니 정보들 파싱해 배열에 넣기


        DicParsingManager DPManager = new DicParsingManager();
        AllNuniArray = DPManager.Parse_character(1);            //누니 정보 파싱
        BuildingArray = DPManager.Parse(0);    //도감 정보 파싱

        // Friends=new string[1] {"Vicky"};            //일단 친구는 비키만 있는걸로
        //친구 목록 불러오기
        //GetComponent<BuildingSave>().GetFriendLsit();           //친구 목록 불러오기

        //GameLoad();
    }
    private void Awake()
    {
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

    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                return null;
            }
            return _Instance;
        }
    }
    
    public static Sprite GetDogamChaImage(string ImageName)
    {
        Debug.Log(ImageName.Trim());
        return GameManager.DogamChaImageData[ImageName.Trim()];

    }
    public static Sprite GetCharacterImage(string ImageName)
    {
        
        return GameManager.CharacterImageData[ImageName.Trim()];
    }

    /*public void GameSave()
    {
        PlayerPrefs.SetInt("Money", Money);//돈
        PlayerPrefs.SetInt("ShinMoney", ShinMoney);//돈
        PlayerPrefs.Save();
        print("save");
    }*/
    public void GameLoad()
    {
        ShinMoney = 10000;
        print("load");
    }
    public void GameExit()
    {
        //GameSave();
        print("exit");
        Application.Quit();
    }
}
