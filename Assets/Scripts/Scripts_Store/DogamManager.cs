using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogamManager : MonoBehaviour
{   //엑셀에 있는 모든 건물 정보 받아서 상점 패널에 넣기
    public GameObject DogamChaPrefab;
    public static Building[] BuildingInformation;
    public static int ChaIndex;
    
    public static bool isParsing = false;

    GameObject DogamCha;
    Sprite[] ChaImage;
    Character Cha;
    public static Sprite ChaImage_;
    //---------------------------------이까지 건물 정보-----------------------

    public GameObject FriendPrefab;         //친구 버튼 프리팹
    GameObject FriendCha;



    public GameObject Scroll;

    public static Button[] LockButton;
  
    // Start is called before the first frame update
    void Start()
    {
        DicParsingManager DPManager = new DicParsingManager();
        BuildingInformation = DPManager.Parse(0);    //건물 정보 파싱
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    public void StrOpen()
    {
        Transform[] scroll_child = Scroll.GetComponentsInChildren<Transform>();
        for (int i = 1; i < scroll_child.Length; i++)
        {
            Destroy(scroll_child[i].gameObject);
        }
        GameManager.isMoveLock = true;

        GameManager.isStore = true;

        for (int j = 0; j < GameManager.StrArray.Length; j++)         //상점 나타내기
        {

            DogamCha = Instantiate(DogamChaPrefab) as GameObject;
            DogamCha.transform.SetParent(Scroll.transform);

            Transform[] BuildingPrefabChilds = DogamCha.GetComponentsInChildren<Transform>();
            Text[] BuildingButtonText = DogamCha.GetComponentsInChildren<Text>();

            //도감 캐릭터 버튼 
            DogamCha.GetComponent<RectTransform>().name = GameManager.StrArray[j].Building_name;

            Button DogamChaButton = DogamCha.GetComponent<Button>();
            Image[] image = DogamChaButton.GetComponentsInChildren<Image>();
            // Text BuildingButtonText = BuildingPrefabChilds[5].GetComponent<Text>();

            if (GameManager.StrArray[j].isLock .Equals( "F"))      //건물이 잠겨있지 않음
            {
                string ChaName;

                BuildingPrefabChilds[4].tag = "unLock";
                ChaName = GameManager.StrArray[j].Building_Image;
                //BuildingInformation[j].SetCharImage(GameManager.GetDogamChaImage(ChaName));
                image[1].sprite = GameManager.GetDogamChaImage(ChaName);   //건물 이름 값 받아와서 이미지 찾기

                BuildingButtonText[0].text = GameManager.StrArray[j].Building_name;      //빌딩 이름 넣기
                BuildingButtonText[1].text = GameManager.StrArray[j].Info;                //빌딩 설명 넣기
                BuildingButtonText[2].text = "Buy";               //빌딩 가격 넣기

                BuildingButtonText[3].text = GameManager.StrArray[j].ShinCost[0].ToString();          //발광석 가격 넣기   
                BuildingButtonText[4].text = GameManager.StrArray[j].Cost[0].ToString();          //발광석 가격 넣기   

            }
           /* else                            //잠겼으면 잠금 이미지 넣기
            {
                BuildingPrefabChilds[4].tag = "Lock";
                image[1].sprite = GameManager.DogamChaImageData["Lock"];
                //DogamChaButton.GetComponent<Image>().sprite = GameManager.DogamChaImageData["Lock"];

                LockButtonList.Add(DogamChaButton);


            }*/

        }
    }
    public void DogamOpen()
    {
        Transform[] scroll_child = Scroll.GetComponentsInChildren<Transform>();
        for (int i = 1; i < scroll_child.Length; i++)
        {
            Destroy(scroll_child[i].gameObject);
        }
        GameManager.isMoveLock = true;
       
        GameManager.isStore = true;


        ChaImage = GameManager.DogamChaImage;       //빌딩 이미지를 받아옴
        

        List<Button> LockButtonList = new List<Button>();       //잠긴 건물들 버튼 들어있는 리스트


        for (int j = 0; j < BuildingInformation.Length; j++)         //상점 나타내기
        {
            if (BuildingInformation[j].Building_Image == "village_level(Clone)")
                continue;


            DogamCha = Instantiate(DogamChaPrefab) as GameObject;
            DogamCha.transform.SetParent(Scroll.transform);

            Transform[] BuildingPrefabChilds = DogamCha.GetComponentsInChildren<Transform>();
            Text[] BuildingButtonText = DogamCha.GetComponentsInChildren<Text>();

            //도감 캐릭터 버튼 
            DogamCha.GetComponent<RectTransform>().name = BuildingInformation[j].Building_name;

            Button DogamChaButton = DogamCha.GetComponent<Button>();
            Image[] image = DogamChaButton.GetComponentsInChildren<Image>();
           // Text BuildingButtonText = BuildingPrefabChilds[5].GetComponent<Text>();

            if (BuildingInformation[j].isLock .Equals( "F") )     //건물이 잠겨있지 않음
            {
                string ChaName;

                BuildingPrefabChilds[4].tag = "unLock";
                ChaName = BuildingInformation[j].Building_Image;
                //BuildingInformation[j].SetCharImage(GameManager.GetDogamChaImage(ChaName));
                image[1].sprite = GameManager.GetDogamChaImage(ChaName);   //건물 이름 값 받아와서 이미지 찾기

                BuildingButtonText[0].text = BuildingInformation[j].Building_name;      //빌딩 이름 넣기
                BuildingButtonText[1].text= BuildingInformation[j].Info;                //빌딩 설명 넣기
                BuildingButtonText[2].text ="Buy";               //빌딩 가격 넣기

                BuildingButtonText[3].text = BuildingInformation[j].ShinCost[0].ToString();          //발광석 가격 넣기   
                BuildingButtonText[4].text = BuildingInformation[j].Cost[0].ToString();          //발광석 가격 넣기   

            }
            else                            //잠겼으면 잠금 이미지 넣기
            {
                BuildingPrefabChilds[4].tag = "Lock";
                image[1].sprite = GameManager.DogamChaImageData["Lock"];
                //DogamChaButton.GetComponent<Image>().sprite = GameManager.DogamChaImageData["Lock"];

                LockButtonList.Add(DogamChaButton);


            }

        }


        //LockButton = LockButtonList.ToArray();      //잠긴 버튼 리스트 배열로 만들어서 넣기
        

    }          //건물 상점 열 때 건물 리스트 불러오기


    public void FriendOpen()                //친구 목록 열 때 친구 리스트 불러오기
    {
        for (int i = 0; i < GameManager.Friends.Length; i++)
        {
            FriendCha = Instantiate(FriendPrefab) as GameObject;
            FriendCha.transform.SetParent(Scroll.transform);

        }
    }

}

