using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject CharacterPrefab;
    public static Card[] NuNiInformation;
    public static int ItemIndex;
    public static int ChaIndex;
    public GameObject Scroll;       //스크롤에 content 넣기

    public static bool isParsing = false;

    GameObject DogamCha;
    Sprite[] ChaImage;
    Character Cha;
    public static Sprite ChaImage_;
    GameObject Window;

   public static List<int> itemList = new List<int>();
    public Sprite[] ItemImages;
    string[] ItemInfo = {"지우개다","킵이다","쓰레기통이다","미리보기다","새로고침이다" };
    /* 아이템 목록
 * 0: 지우개
 * 1: 킵
 * 2: 쓰레기통
 * 3: 미리보기
 * 4: 새로고침
 */
    public static Button[] LockButton;

    public static GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (isParsing == false)
        {
            DicParsingManager DPManager = new DicParsingManager();
            NuNiInformation = DPManager.Parse_character(1);    //도감 정보 파싱
            isParsing = true;
        }
        Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CharacterOpen()
    {
    
        float p = 0;

        //RefreshButtonArray(NuNiInformation);         //소유하지 않은 건물들 새로고침

        List<Button> LockButtonList = new List<Button>();       //잠긴 캐릭터들 버튼 들어있는 리스트

        Debug.Log(NuNiInformation.Length);
        for (int j = 0; j < NuNiInformation.Length; j++)         //시작하기 전 캐릭터 나타내기
        {
            if (NuNiInformation[j].Item != 10)
            {
                DogamCha = Instantiate(CharacterPrefab);
                DogamCha.transform.SetParent(Scroll.transform);

                Transform[] ChaPrefabChilds = DogamCha.GetComponentsInChildren<Transform>();

                //도감 캐릭터 버튼 
                DogamCha.GetComponent<RectTransform>().name = j.ToString();

                Button DogamChaButton = DogamCha.GetComponent<Button>();
                Image[] image = DogamChaButton.GetComponentsInChildren<Image>();

                Text CharacterName = ChaPrefabChilds[1].GetComponent<Text>();
                if (NuNiInformation[j].isLock == "F")      //캐릭터가 잠겨있지 않음
                {
                    //Debug.Log(NuNiInformation[j].GetCharacter("Name"));
                    string ChaName;

                    //BuildingPrefabChilds[4].tag = "unLock";
                    ChaName = NuNiInformation[j].cardImage;

                    CharacterName.text = NuNiInformation[j].cardName;

                    NuNiInformation[j].SetChaImage(GameManager.GetCharacterImage(ChaName));

                    image[1].sprite = ItemImages[NuNiInformation[j].Item];//NuNiInformation[j].GetChaImange();   //캐릭터 이름 값 받아와서 이미지 찾기

                }

                else                            //잠겼으면 잠금 이미지 넣기
                {
                    //BuildingPrefabChilds[4].tag = "Lock";
                    CharacterName.text = "잠김누니";
                    NuNiInformation[j].SetChaImage(GameManager.DogamChaImageData["Lock"]);
                    image[1].sprite = GameManager.DogamChaImageData["Lock"];

                    // DogamChaButton.enabled = false;
                    //DogamChaButton.GetComponent<Image>().sprite = GameManager.DogamChaImageData["Lock"];

                    LockButtonList.Add(DogamChaButton);


                }
            }
        

            
            p += 100f;
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
