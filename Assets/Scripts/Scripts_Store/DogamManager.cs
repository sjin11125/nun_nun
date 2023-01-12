using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice 
{
    public string title;
    public string info;
    public string reward;

}
public class DogamManager : MonoBehaviour
{   //엑셀에 있는 모든 건물 정보 받아서 상점 패널에 넣기
    public GameObject DogamChaPrefab;
    public GameObject StrPrefab;
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
    public GameObject Scroll_str;

    public static Button[] LockButton;

    //-------------------------------------------공지-----------------------

    public GameObject NoticPrefab;
    public GameObject Notis_Content;
    /// </summary>
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
        Transform[] scroll_child = Scroll_str.GetComponentsInChildren<Transform>();
        for (int i = 1; i < scroll_child.Length; i++)
        {
            Destroy(scroll_child[i].gameObject);
        }
        GameManager.isMoveLock = true;

        GameManager.isStore = true;

        for (int j = 0; j < GameManager.StrArray.Length; j++)         //상점 나타내기
        {

            DogamCha = Instantiate(StrPrefab) as GameObject;
            DogamCha.transform.SetParent(Scroll_str.transform);

            Transform[] BuildingPrefabChilds = DogamCha.GetComponentsInChildren<Transform>();
            Text[] BuildingButtonText = DogamCha.GetComponentsInChildren<Text>();

            //도감 캐릭터 버튼 
            

            Button DogamChaButton = DogamCha.GetComponent<Button>();
            Image[] image = DogamChaButton.GetComponentsInChildren<Image>();
            // Text BuildingButtonText = BuildingPrefabChilds[5].GetComponent<Text>();
            DogamCha.GetComponent<RectTransform>().name = GameManager.StrArray[j].Building_name;
            Debug.Log(GameManager.StrArray[j].Building_name);
            if (GameManager.StrArray[j].isLock .Equals( "F"))      //건물이 잠겨있지 않음
            {
                string ChaName;

                BuildingPrefabChilds[4].tag = "unLock";
                ChaName = GameManager.StrArray[j].Building_Image;
                //BuildingInformation[j].SetCharImage(GameManager.GetDogamChaImage(ChaName));
                Debug.Log(ChaName);
                image[1].sprite = GameManager.GetDogamChaImage(ChaName);   //건물 이름 값 받아와서 이미지 찾기
                
                BuildingButtonText[0].text = GameManager.StrArray[j].Building_name;      //빌딩 이름 넣기
                //BuildingButtonText[1].text = GameManager.StrArray[j].Info;                //빌딩 설명 넣기
                BuildingButtonText[1].text = "구매";               //빌딩 가격 넣기

                //BuildingButtonText[3].text = GameManager.StrArray[j].ShinCost[0].ToString();          //발광석 가격 넣기   
                BuildingButtonText[2].text = GameManager.StrArray[j].Cost[0].ToString();          // 가격 넣기   
                Debug.Log(ChaName);
            }
            DogamCha.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 1);
            /* else                            //잠겼으면 잠금 이미지 넣기
             {
                 BuildingPrefabChilds[4].tag = "Lock";
                 image[1].sprite = GameManager.DogamChaImageData["Lock"];
                 //DogamChaButton.GetComponent<Image>().sprite = GameManager.DogamChaImageData["Lock"];

                 LockButtonList.Add(DogamChaButton);


             }*/
            Debug.Log("스프라이트 이름: "+image[1].sprite.name);
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

            DogamCha.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 1);
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
                //BuildingButtonText[1].text= BuildingInformation[j].Info;                //빌딩 설명 넣기
                BuildingButtonText[2].text ="구매";               //빌딩 가격 넣기

                BuildingButtonText[3].text = BuildingInformation[j].ShinCost[0].ToString();          //발광석 가격 넣기   
                BuildingButtonText[4].text = BuildingInformation[j].Cost[0].ToString();          //발광석 가격 넣기   

                 BuildingButtonText[6].text = BuildingInformation[j].Reward[0].ToString();          //발광석 가격 넣기   
                BuildingButtonText[8].text =  BuildingInformation[j].Reward[1].ToString();          //발광석 가격 넣기   


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

    public void NoticeOpen()                    //우편함 열기
    {
        Transform[] child=Notis_Content.GetComponentsInChildren<Transform>();   
        for (int i = 1; i < child.Length; i++)
        {
            Destroy(child[i].gameObject);

        }
        for (int i = 0; i < GameManager.Notice.Length; i++)
        {
            if (GameManager.Notice[i].reward=="notice")     //공지일 경우
            {
                GameObject Notice = Instantiate(NoticPrefab);
                Notice.transform.name = GameManager.Notice[i].title;
                Notice.transform.SetParent(Notis_Content.transform);

                Text text = Notice.GetComponentInChildren<Text>();
                text.text = GameManager.Notice[i].title;

                Notice.GetComponent<RectTransform>().localScale = new Vector3(1.55f, 2f, 0);
            }
            else                    //공지가 아니고 보상일 경우
            {
                GameObject Notice = Instantiate(NoticPrefab);
                Notice.transform.name = GameManager.Notice[i].title;
                Notice.transform.SetParent(Notis_Content.transform);

                Text text = Notice.GetComponentInChildren<Text>();
                text.text = GameManager.Notice[i].title;

                Notice.GetComponent<RectTransform>().localScale = new Vector3(1.55f, 2f, 0);

                
               
            }


          
        }
    }
    public void FriendOpen()                //친구 목록 열 때 친구 리스트 불러오기
    {
        for (int i = 0; i < GameManager.Friends.Length; i++)
        {
            FriendCha = Instantiate(FriendPrefab) as GameObject;
            FriendCha.transform.SetParent(Scroll.transform);

        }
    }

}

