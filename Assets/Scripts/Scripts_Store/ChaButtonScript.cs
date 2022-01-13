using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChaButtonScript : MonoBehaviour
{
    public GameObject ChaPanel2;
    public GameObject LockPanel;
    public GameObject Check;

    public Sprite[] ItemImages;

    public bool isCheck = false;

    public Text ButtonText;
    /* 아이템 목록
* 0: 지우개(배치되어있는거 버림)
* 1: 킵
* 2: 쓰레기통(배치할거 버림)
* 3: 미리보기
* 4: 새로고침(색깔바꾸기)
*/
    public static bool isEdit;

    public Building DowngradeBuilding;

    public bool isUpgrade = false;
    static GameObject[] buildings;
    static int level;

    GameObject Grid;

    public GameObject NuniInfoPannel;

    public GameObject WindowClose;
    // Start is called before the first frame update
    void Start()
    {
        Grid = GameObject.Find("back_down");
    }
    public void NuniInfoClick()
    {
        GameObject NuniInfo = Instantiate(NuniInfoPannel) as GameObject;        //누니 정보 패널 Instantiate
        NuniInfo.transform.SetParent(StartManager.Canvas.transform);        //캔버스 부모설정
        NuniInfo.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            // Debug.Log(transform.parent.name);
            if (transform.name == GameManager.AllNuniArray[i].cardImage)
            {
                Card nuni = GameManager.AllNuniArray[i];

                Text[] InfoTexts = NuniInfo.GetComponentsInChildren<Text>();
                Image[] InfoImage = NuniInfo.GetComponentsInChildren<Image>();

                InfoImage[2].sprite = nuni.GetChaImange();

                InfoTexts[0].text = nuni.cardName;      //누니 이름 넣기
                //InfoTexts[1].text=nuni.                  //누니 설명
                //누니 보유 효과


            }
        }


    }
    public void IsUpgrade()         //건물 업그레이드 할 것 인가?
    {
        Building building = buildings[0].transform.parent.GetComponent<Building>();
        string building_name = buildings[0].transform.parent.name;
        Debug.Log(building_name);
        if (level == 0)
        {
            Debug.Log("000");
            /* for (int i = 6; i < buildingTrans.Length; i++)
             {
                 if (buildingTrans[i].name=="building2")
                 {
                     buildingTrans[i].gameObject.SetActive(true);
                     return;
                 }
             }*/
        }
        else if (level == 1)
        {
            Debug.Log("111");
            /* for (int i = 6; i < buildingTrans.Length; i++)
             {
                 if (buildingTrans[i].name == "building3")
                 {
                     buildingTrans[i].gameObject.SetActive(true);
                     return;
                 }
             }*/

            Debug.Log(buildings.Length);

            buildings[1].SetActive(true);
            building.level += 1;
        }
        else
        {
            Debug.Log("222");
            buildings[2].SetActive(true);
            building.level += 1;
        }
        Debug.Log(building.level);
        building.Type = BuildType.Empty;
        building.RefreshBuildingList();     //빌딩 리스트 새로고침
        /*for (int i = 0; i < GameManager.BuildingList.Count; i++)
        {
            Debug.Log(GameManager.BuildingList[i].Building_name);
            if (GameManager.BuildingList[i].Building_name == building_name)
            {
                GameManager.BuildingList[i].level = building.level;
                Debug.Log(GameManager.BuildingList[i].level);
            }
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Lock")         //잠겼을때
        {
            //LockChaButtonClick();
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
    public void Upgrade(GameObject[] buildings, int level, Building building)              //건물 업그레이드
    {                                                                   //현재 가지고 있는 건물 리스트에서 해당 건물 찾아서 레벨 수정
        ChaButtonScript.buildings = buildings;
        ChaButtonScript.level = level;
        Debug.Log("this.buildings: " + ChaButtonScript.buildings.Length);
        Transform UPPannelTrans = gameObject.GetComponent<Transform>();

        transform.parent = GameObject.Find("O").transform;
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


        // Transform[] buildingTrans = buildings.GetComponentsInChildren<Transform>();

    }

    public void CloseButtonClick()          //닫기 버튼
    {
        Transform[] Window = transform.parent.GetComponentsInChildren<Transform>();

        if (gameObject.tag == "Building")
        {
            for (int i = 5; i < Window.Length - 2; i++)
            {
                Destroy(Window[i].gameObject);
            }
        }
        else
        {
            for (int i = 6; i < Window.Length - 1; i++)
            {
                Destroy(Window[i].gameObject);
            }
        }

        GameManager.isStore = false;
    }
    public void CloseClick()
    {
        Transform[] WindowChilds = WindowClose.GetComponentsInChildren<Transform>();

        for (int i = 1; i < WindowChilds.Length; i++)
        {
            Destroy(WindowChilds[i].gameObject);
        }
    }

    public void CloseButtonClick2()
    {
        Transform[] Windows= transform.parent.GetComponentsInChildren<Transform>();
        Transform[] child= Windows[1].GetComponentsInChildren<Transform>();
        for (int i = 0; i < child.Length; i++)
        {
            Destroy(child[i].gameObject);
            
        }
    }
    public void ButtonClick()
    {
        if (gameObject.tag == "Lock")         //잠겼을때
        {
            //LockChaButtonClick();
            gameObject.GetComponent<Button>().enabled = false;
        }
        else                                //안잠겼을때
        {
            //ChaButtonClick();
        }
    }
  
    public void ChaButtonClick()        //잠겨있지 않은 캐릭터 버튼 클릭
    {



        StartManager.ChaIndex = int.Parse(gameObject.transform.name);


        int item = StartManager.NuNiInformation[StartManager.ChaIndex].Item;




        Transform[] ChaChild;
        ChaChild = ChaPanel2.GetComponentsInChildren<Transform>();      //캐릭터패널2 자식 오브젝트

        if (item != 10)
        {
            //도감 캐릭터 정보 대입
            Image chaimage = ChaChild[2].GetComponent<Image>();            //캐릭터 이미지
            Text iteminfo = ChaChild[1].GetComponent<Text>();      //아이템 설명



            //chaimage.sprite = StartManager.NuNiInformation[StartManager.ChaIndex].GetChaImange();
            Debug.Log(GameManager.items);
            if (GameManager.Items[item] != true)
            {
                if (GameManager.items > 4)
                {
                    return;
                }
                GameManager.Items[item] = true;
                GameManager.items += 1;
                Check.SetActive(true);
            }
            else
            {
                GameManager.Items[item] = false;
                GameManager.items -= 1;
                Check.SetActive(false);
            }
        }
        /* if (StartManager.NuNiInformation[StartManager.ChaIndex].isLock == "F")      //안잠겼냐
     {

         //isCheck = true;     //선택됫단다
         //Debug.Log("ununlock");
         switch (item)       //아이템 설명 넣기
         {
             case 0:
                 if (GameManager.Items[0] != true)
                 {
                     Debug.Log("safsfd");
                     if (GameManager.items >= 5)
                     {
                         break;
                     }
                     GameManager.Items[0] = true;

                     GameManager.items += 1;
                     Check.SetActive(true);
                     break;
                 }
                 if (GameManager.Items[0] == true)
                 {
                     Debug.Log("hgjgj");
                     GameManager.Items[0] = false;
                     GameManager.items -= 1;
                     Check.SetActive(false);
                     break;
                 }

                 break;
             case 1:
                 if (GameManager.Items[1] != true)
                 {
                     if (GameManager.items >= 5)
                     {
                         break;
                     }
                     GameManager.Items[1] = true;
                     GameManager.items += 1;
                     Check.SetActive(true);
                 }
                 else
                 {
                     GameManager.Items[1] = false;
                     GameManager.items -= 1;
                     Check.SetActive(false);
                 }
                 break;
             case 2:
                 if (GameManager.Items[2] != true)
                 {
                     if (GameManager.items >= 5)
                     {
                         break;
                     }
                     GameManager.Items[2] = true;
                     GameManager.items += 1;
                     Check.SetActive(true);
                 }
                 else
                 {
                     GameManager.Items[2] = false;
                     GameManager.items -= 1;
                     Check.SetActive(false);
                 }
                 break;
             case 3:
                 if (GameManager.Items[3] != true)
                 {
                     if (GameManager.items >= 5)
                     {
                         break;
                     }
                     GameManager.Items[3] = true;
                     GameManager.items += 1;
                     Check.SetActive(true);
                 }
                 else
                 {
                     GameManager.Items[3] = false;
                     GameManager.items -= 1;
                     Check.SetActive(false);
                 }
                 break;
             case 4:
                 if (GameManager.Items[4] != true)
                 {
                     if (GameManager.items > 6)
                     {
                         break;
                     }
                     GameManager.Items[4] = true;
                     GameManager.items += 1;
                     Check.SetActive(true);
                 }
                 else
                 {
                     GameManager.Items[4] = false;
                     GameManager.items -= 1;
                     Check.SetActive(false);
                 }
                 break;

                 /* 아이템 목록
                 * 0: 지우개
                 * 1: 킵
                 * 2: 쓰레기통
                 * 3: 미리보기
                 * 4: 새로고침
                 */
    }
    
    public void LockChaButtonClick()        //캐릭터 살려고 할 때 클릭하는
    {
        DogamManager.ChaIndex = int.Parse(gameObject.name);

    
        GameObject DogamCha = Instantiate(LockPanel);
        DogamCha.transform.SetParent(StartManager.Canvas.transform);
        DogamCha.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }

    public void LockChaButtonClick2()       //빌딩 살려고 구매버튼 클릭할 때
    {
        DogamManager.ChaIndex = int.Parse(gameObject.transform.parent.name);


        if (gameObject.tag != "Lock")
        {
            Debug.Log(DogamManager.ChaInformation[DogamManager.ChaIndex].GetCharacter("Money"));
            int pay = int.Parse(DogamManager.ChaInformation[DogamManager.ChaIndex].GetCharacter("Money"));

            
           
            if (GameManager.Money < pay)      //돈이 모잘라 거절 메세지 띄움
            {
                UIManager.isSetMoney = -1;


            }
            else                    // 결제함
            {

                Grid.GetComponent<SpriteRenderer>().sortingOrder = -50;

                Character ChaInformation = DogamManager.ChaInformation[DogamManager.ChaIndex];

                GameManager.Money -= pay;
                UIManager.isSetMoney = 1;
                ChaInformation.SetCharacter("isLock", "F");      //안잠김으로 바꿈
                                                                 // ChaInformation.SetCharImage(GameManager.GetDogamChaImage(ChaInformation.GetCharacter("ImageName")));        //이미지 다시 바꿈


                

                DogamManager.ChaInformation[DogamManager.ChaIndex] = ChaInformation;

                Transform[] trans = transform.parent.parent.parent.GetComponentsInChildren<Transform>();
                //GridBuildingSystem[] grid = trans.GetComponentsInChildren<GridBuildingSystem>();
                
                Debug.Log("DogamManager.ChaIndex: "+DogamManager.ChaIndex);
                Debug.Log(GameManager.BuildingPrefabData.Count);

                //게임매니저에 잇는 건물 프리팹 배열에서 같은 이름을 가진 프리팹을 찾아 Instantiate하고 상점 창 닫기
                string buildingname = DogamManager.ChaInformation[DogamManager.ChaIndex].GetCharacter("ImageName");
                GameObject buildingprefab = GameManager.BuildingPrefabData[buildingname+"(Clone)"];
            

                Transform parent = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent;

                Transform[] Window = parent.GetComponentsInChildren<Transform>();  //StoreWindow
                //parent.gameObject.SetActive(false);

                GameManager.CurrentBuilding = buildingprefab;
                Building b = buildingprefab.GetComponent<Building>();
                Building c = GameManager.CurrentBuilding.GetComponent<Building>();
                c.Building_Image = buildingname;
                c = b.GetComponent<Building>().DeepCopy();

                
                for (int i = 5; i < Window.Length-1; i++)
                {
                    Destroy(Window[i].gameObject);
                }
                parent.gameObject.SetActive(false);
                isEdit = true;
            }
       
        }

        GameManager.isStore = false;


        
        
    }
}


