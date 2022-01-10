using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogamManager : MonoBehaviour
{   //엑셀에 있는 모든 건물 정보 받아서 상점 패널에 넣기
    public GameObject DogamChaPrefab;
    public static Character[] ChaInformation;
    public static int ChaIndex;
    
    public static bool isParsing = false;

    GameObject DogamCha;
    Sprite[] ChaImage;
    Character Cha;
    public static Sprite ChaImage_;

    public GameObject Scroll;

    public static Button[] LockButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DogamOpen()
    {
        if (isParsing == false)
        {
            DicParsingManager DPManager = new DicParsingManager();
            ChaInformation = DPManager.Parse(0);    //도감 정보 파싱
            isParsing = true;
        }
        GameManager.isStore = true;


        ChaImage = GameManager.DogamChaImage;       //빌딩 이미지를 받아옴

        //RefreshButtonArray(ChaInformation);         //소유하지 않은 건물들 새로고침

        List<Button> LockButtonList = new List<Button>();       //잠긴 건물들 버튼 들어있는 리스트


        for (int j = 0; j < ChaInformation.Length; j++)         //상점 나타내기
        {

            DogamCha = Instantiate(DogamChaPrefab) as GameObject;
            DogamCha.transform.SetParent(Scroll.transform);

            Transform[] BuildingPrefabChilds = DogamCha.GetComponentsInChildren<Transform>();
            Text[] BuildingButtonText = DogamCha.GetComponentsInChildren<Text>();

            for (int i = 0; i < BuildingButtonText.Length; i++)
            {
                Debug.Log(BuildingButtonText[i].name);
            }
            //도감 캐릭터 버튼 
            DogamCha.GetComponent<RectTransform>().name = j.ToString();

            Button DogamChaButton = DogamCha.GetComponent<Button>();
            Image[] image = DogamChaButton.GetComponentsInChildren<Image>();
           // Text BuildingButtonText = BuildingPrefabChilds[5].GetComponent<Text>();

            if (ChaInformation[j].GetCharacter("isLock") == "F")      //캐릭터가 잠겨있지 않음
            {
                string ChaName;

                BuildingPrefabChilds[4].tag = "unLock";
                ChaName = ChaInformation[j].GetCharacter("ImageName");
                ChaInformation[j].SetCharImage(GameManager.GetDogamChaImage(ChaName));
                image[1].sprite = ChaInformation[j].GetSprite();   //캐릭터 이름 값 받아와서 이미지 찾기

                BuildingButtonText[0].text = ChaInformation[j].GetCharacter("Name");
                BuildingButtonText[1].text= ChaInformation[j].GetCharacter("Information");
                BuildingButtonText[2].text ="$"+ ChaInformation[j].GetCharacter("Money");



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
        

    }





    public void RefreshButtonArray(Character[] CharactersArray)
    {
        List<Button> LockButtonList = new List<Button>();
        LockButton = new Button[CharactersArray.Length];        //
        for (int i = 0; i < CharactersArray.Length; i++)
        {
            if (CharactersArray[i].GetCharacter("isLock") == "F")
            {

            }
        }
    }

}

