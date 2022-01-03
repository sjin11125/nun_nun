﻿using System.Collections;
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

    public static int Money = 10000;            //재화

    public static List<GameObject> BuildingList;          //가지고 있는 빌딩들
    public static GameObject[] BuildingArray;
    
    public GameObject[] BuildingPrefabInspector;    //인스펙터에서 받아 온 건물 프리팹 배열
    public static Dictionary<string, GameObject> BuildingPrefabData;    //모든 빌딩 프리팹 딕셔너리

    public static GameObject CurrentBuilding;       //현재 수정중인 건물
    //----------------------------------------------------이까지 건물----------------------------------------------------
    //----------------------------------------------------여기서부터 캐릭터--------------------------------------------------
    public Sprite[] CharacterImageInspector;            // 인스펙터에서 받아 온 모든 캐릭터들의 이미지
    public static Dictionary<string, Sprite> CharacterImageData;

    public GameObject[] CharaterPrefabInspector;        // 인스펙터에서 받아 온 모든 캐릭터들의 프리팹
    public static Dictionary<string, GameObject> CharacterPrefab;       //모든 캐릭터 프리팹 딕셔너리

    public static List<Card> CharacterList;      //현재가지고 있는 캐릭터 리스트
    public static Card[] CharacterArray;

    public static bool[] Items=new bool[5];     //현재 가지고 잇는 아이템 유무
    public static int items=0;
    public static bool isStore = false;

    public GameObject Dont;
    
    /* 아이템 목록
     * 0: 지우개
     * 1: 킵
     * 2: 쓰레기통
     * 3: 미리보기
     * 4: 새로고침
     */

    // Start is called before the first frame update
    void Start()
    {
        BuildingList = new List<GameObject>();            //현재 가지고 있는 빌딩 리스트

        DogamChaImageData = new Dictionary<string, Sprite>();       //전체 캐릭터 리스트(가지고 있지 않은것도 포함)
        BuildingPrefabData = new Dictionary<string, GameObject>();      //전체 빌딩 프리팹 리스트 (가지고 있지 않은 것도 포함)
        CharacterPrefab = new Dictionary<string, GameObject>();
        CharacterImageData = new Dictionary<string, Sprite>();
        CharacterList = new List<Card>();
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

        for (int i = 0; i < DogamChaImageInspector.Length; i++)     // 빌딩 이미지 불러오기
        {
            Debug.Log(DogamChaImageInspector[i].name);
            DogamChaImageData.Add(DogamChaImageInspector[i].name, DogamChaImageInspector[i]);
        }

        for (int i = 0; i < CharacterImageInspector.Length; i++)        //캐릭터 
        {
            Debug.Log(CharacterImageInspector[i].name);
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
        Debug.Log("ImageName: " + ImageName);

        return GameManager.DogamChaImageData[ImageName.Trim()];

    }
    public static Sprite GetCharacterImage(string ImageName)
    {
        
        return GameManager.CharacterImageData[ImageName.Trim()];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
