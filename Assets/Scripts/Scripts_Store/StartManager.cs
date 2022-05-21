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

    public Text ItemInfoText;
    //int[] itemList = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
   public static Dictionary<int, bool> itemList = new Dictionary<int, bool> {
                                                    {0,false},
                                                    { 1,false},
                                                    { 2,false},
                                                    { 3,false},
                                                    { 4,false},
                                                    { 5,false},
                                                    { 6,false},
                                                    { 7,false},
                                                    { 8,false},
                                                    { 9,false}
                                                        };
    public Sprite[] ItemImages;         //아이템 이미지들
    public Sprite LockImage;

    string[] ItemInfos = { "배치된 말 중 원하는 말을 하나 삭제한다. ",
                          "등장한 말을 다른 곳에 킵 하고 다시 사용할 수 있게 한다.",
                          "배치하기 전 등장한 말을 삭제할 수 있다.",
                          "다음에 나올 말을 미리 볼 수 있다.",
                          "등장한 말을 랜덤으로 교체 할 수 있다.",
                          "다음에 나올 말과 현재 배치해야 하는 말을 교체할 수 있다.",
                          "배치된 말의 색깔을 바꿀 수 있다.",//배치된 말의 색깔을 바꿀 수 있다.
                          "모든 종류의 말을 대체할 수 있는 말 하나 생성한다.",//모든 종류의 말을 대체할 수 있는 말 하나 생성한다.
                          "배치된 타일중 아이템 기준 위아래로 타일제거한다.",//배치된 타일중 아이템 기준 위아래로 타일제거한다.
                          "배치된 타일 중 아이템 기준 양옆으로 타일 제거한다."};//배치된 타일 중 아이템 기준 양옆으로 타일 제거한다.
    public Sprite[] ItenImage;
    public Text ItemInfo;
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
        StartManager.ChaIndex = 99;

    }

    // Update is called once per frame
    void Update()
    {
        if (ChaIndex==99)
        {
            ItemInfo.text = "";
        }
        else
        ItemInfo.text = ItemInfos[StartManager.ChaIndex];
    }

    public void CharacterOpen()
    {
        Transform[] child = Scroll.GetComponentsInChildren<Transform>();
        for (int i = 1; i < child.Length; i++)
        {
            Destroy(child[i].gameObject);
        }



        for (int j = 0; j < itemList.Count; j++)         //시작하기 전 캐릭터 나타내기
        {
            Card[] NuniArray = GameManager.CharacterList.ToArray();
            for (int i = 0; i < NuniArray.Length; i++)
            {
                if (NuniArray[i].Item == j)
                {

                    itemList[j] = true;

                 
                }
            }
           



        }
        for (int j = 0; j < itemList.Count; j++)
        {
            DogamCha = Instantiate(CharacterPrefab);
            DogamCha.transform.SetParent(Scroll.transform);

            Transform[] ChaPrefabChilds = DogamCha.GetComponentsInChildren<Transform>();

            //도감 캐릭터 버튼 
            DogamCha.GetComponent<RectTransform>().name = j.ToString();

            Button DogamChaButton = DogamCha.GetComponent<Button>();
            Image[] image = DogamChaButton.GetComponentsInChildren<Image>();

            Text CharacterName = ChaPrefabChilds[1].GetComponent<Text>();

            if (itemList[j] == true)
            {
                image[1].sprite = ItenImage[j];//NuNiInformation[j].GetChaImange();   //캐릭터 이름 값 받아와서 이미지 찾기
                //Instantiate(ItemImages[j], DogamCha.transform);
                //GameManager.Items[j] = true;
            }
            else
            {
                image[1].sprite = LockImage;//NuNiInformation[j].GetChaImange();   //캐릭터 이름 값 받아와서 이미지 찾기
                //Instantiate(ItemImages[10], DogamCha.transform);
                DogamCha.tag = "Lock";
                //GameManager.Items[j] = false;
            }

           /* string ChaName  = NuNiInformation[j].cardImage;

            CharacterName.text = NuNiInformation[j].cardName;

            NuNiInformation[j].SetChaImage(GameManager.GetCharacterImage(ChaName));
            Debug.Log(NuNiInformation[j].Item);*/

            
        }
        //LockButton = LockButtonList.ToArray();      //잠긴 버튼 리스트 배열로 만들어서 넣기
        GridScript.EraserItemTurn = 10;
        GridScript.ReloadItemTurn = 15;
        GridScript.NextExchangeItemTurn = 15;
        GridScript.KeepItemTurn = 30;
        GridScript.TrashItemTurn = 20;
        GridScript.RainbowItemTurn = 40;
        GridScript.ChangeShapeItem = 40;
        GridScript.ThreeVerticalItem = 30;
        GridScript.ThreeHorizontalItem = 30;
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
