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
    static int Level;

    GameObject Grid;

    public GameObject NuniInfoPannel;

    public GameObject WindowClose;

    public GameObject NuniUpgradeButton;

    private GameObject settigPanel;

    void Start()
    {
        Grid = GameObject.Find("back_down");
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }
    public void Islockfalse()
    {
        GameManager.isMoveLock = false;
        Building building = buildings[0].transform.parent.GetComponent<Building>();
        string building_name = buildings[0].transform.parent.name;
        building.Type = BuildType.Empty;

      
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
                Image[] stars = NuniInfo.transform.Find("Stars").GetComponentsInChildren<Image>();

                if (nuni.Star == "1")
                {
                    Debug.Log("이 누니는 현재 " + nuni.Star + "성 입니다.");
                    InfoImage[1].color = new Color(244 / 255f, 255 / 255f, 162 / 255f);

                    Debug.Log(InfoImage[1].name);


                  /*  Debug.Log("이 누니는 현재 " + nuni.Star + "성 입니다.");
                    InfoImage[1].color = new Color(210 / 255f, 150 / 255f, 255 / 255f);*/
                    
                }
                else if (nuni.Star == "3")
                {
                    Debug.Log("이 누니는 현재 " + nuni.Star + "성 입니다.");
                    InfoImage[1].color = new Color(255 / 255f, 168 / 255f, 255 / 255f);
                    
                    Debug.Log(InfoImage[1].name);
                }
                else
                {
                    Debug.Log("이 누니는 현재 " + nuni.Star + "성 입니다.");
                   // InfoImage[1].color = new Color(210 / 255f, 150 / 255f, 255 / 255f);

                }
                /* for (int j = 0; j < int.Parse(GameManager.AllNuniArray[i].Star); j++)   //별 넣기
                 {
                     stars[j].color = new Color(1, 1, 1);
                 }*/
                InfoImage[2].sprite = nuni.GetChaImange();

                InfoTexts[0].text = nuni.cardName;      //누니 이름 넣기
                InfoTexts[1].text = nuni.Info;                  //누니 설명
                InfoTexts[2].text = nuni.Effect; //누니 보유 효과

                
               

            }
        }


    }
    public void IsSell()            //건물 제거한다고 했을 때
    {
        //.gameObject.SetActive(true);

        // Building remove_building = DowngradeBuilding;
        // Debug.Log(gameObject.transform.parent.parent.gameObject.name);
        DowngradeBuilding.Remove(DowngradeBuilding);
    }
    public void IsUpgrade()         //건물 업그레이드 한다고 했을 때
    {
        GameManager.isMoveLock = false;
        Building building = buildings[0].transform.parent.GetComponent<Building>();
        string building_name = buildings[0].transform.parent.name;
        Debug.Log(building_name);
        if (Level == 0)
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
        else if (Level == 1)
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

           // buildings[1].SetActive(true);
            building.Level += 1;

            buildings[0].GetComponent<SpriteRenderer>().sprite = building.buildings_image[building.Level - 2];
            SpriteRenderer spriteRenderer = buildings[1].GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sortingOrder = buildings[0].GetComponentInChildren<SpriteRenderer>().sortingOrder + 1;
        }
        else
        {
            Debug.Log("222");
            //buildings[2].SetActive(true);
            buildings[0].GetComponent<SpriteRenderer>().sprite = building.buildings_image[building.Level -2];
            building.Level += 1;
        }
        GameManager.Money -= building.Cost[building.Level-1];
        GameManager.ShinMoney -= building.ShinCost[building.Level-1];
        Debug.Log("building.level:"+building.Level+ "      building.cost: "+ building.Cost[building.Level]);
        Debug.Log(building.Level);
        building.Type = BuildType.Empty;
        building.RefreshBuildingList();     //빌딩 리스트 새로고침

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
    public void Upgrade(GameObject[] buildings, int Level, Building building)              //건물 업그레이드
    {                                                                   //현재 가지고 있는 건물 리스트에서 해당 건물 찾아서 레벨 수정
        ChaButtonScript.buildings = buildings;
        ChaButtonScript.Level = Level;
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
            for (int i = 8; i < Window.Length - 2; i++)
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
        GameManager.isMoveLock = false;
    }
    public void CloseClick()
    {
        Transform[] WindowChilds = WindowClose.GetComponentsInChildren<Transform>();

        for (int i = 1; i < WindowChilds.Length; i++)
        {
            Destroy(WindowChilds[i].gameObject);
        }
        GameManager.isMoveLock = false;
    }

    public void CloseButtonClick2()
    {
        Transform[] Windows = WindowClose.transform.GetComponentsInChildren<Transform>();
        Transform[] child = Windows[1].GetComponentsInChildren<Transform>();
        for (int i = 0; i < child.Length; i++)
        {
            Destroy(child[i].gameObject);

        }
        GameManager.isMoveLock = false;
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

        if (gameObject.tag == "Lock")
        {
            return;
        }

        StartManager.ChaIndex = int.Parse(gameObject.transform.name);


        int item = StartManager.ChaIndex;





       

            //chaimage.sprite = StartManager.NuNiInformation[StartManager.ChaIndex].GetChaImange();
            Debug.Log("StartManager.ChaIndex: "+item);

        if (GameManager.Items[item] != true)
        {
            if (GameManager.items <= 2)
            {
                GameManager.Items[item] = true;
                GameManager.items += 1;
                Check.SetActive(true);
            }

        }
        else
        {
            GameManager.Items[item] = false;
            GameManager.items -= 1;
            Check.SetActive(false);
        }

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





        settigPanel.GetComponent<AudioController>().Sound[0].Play();
    }
    public void LockChaButtonClick()        //캐릭터 살려고 할 때 클릭하는
    {
        DogamManager.ChaIndex = int.Parse(gameObject.name);


        GameObject DogamCha = Instantiate(LockPanel);
        DogamCha.transform.SetParent(StartManager.Canvas.transform);
        DogamCha.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        GameManager.isMoveLock = false;
    }

    public void LockChaButtonClick2()       //빌딩 살려고 구매버튼 클릭할 때
    {
       string buildingName = gameObject.transform.parent.name;
        Building building=new Building();

        if (gameObject.tag != "Lock")       //건물이 안잠겨있고
        {
            for (int i = 0; i < DogamManager.BuildingInformation.Length; i++)
            {
                if (DogamManager.BuildingInformation[i].Building_name == buildingName)
                {
                    building.SetValue(DogamManager.BuildingInformation[i]);
                    Debug.Log("빌딩은 현재 "+ building.Level+"레벨");
                }
            }
            for (int i = 0; i < GameManager.StrArray.Length; i++)
            {
                if (GameManager.StrArray[i].Building_name == buildingName)
                {
                    building.SetValue(GameManager.StrArray[i]);
                }

            }
            int pay = building.Cost[0];
            int shinPay = building.ShinCost[0];

            if (GameManager.Money < pay || GameManager.ShinMoney < shinPay)      //돈이나 자원이 모자르면 거절 메세지 띄움
            {
                UIManager.isSetMoney = -1;
                Debug.Log("결제못함");
            }
            else                    // 결제함
            {

                Grid.GetComponent<SpriteRenderer>().sortingOrder = -50;
                

                GameManager.Money -= pay;       //자원빼기
                GameManager.ShinMoney -= shinPay;

                UIManager.isSetMoney = 1;
                //BuildingInformation.isLock = "F";      //안잠김으로 바꿈
                // BuildingInformation.SetCharImage(GameManager.GetDogamChaImage(BuildingInformation.GetCharacter("ImageName")));        //이미지 다시 바꿈

                //GameManager.BuildingArray[DogamManager.ChaIndex] = BuildingInformation;           //건물 설명

                Transform[] trans = transform.parent.parent.parent.GetComponentsInChildren<Transform>();
                //GridBuildingSystem[] grid = trans.GetComponentsInChildren<GridBuildingSystem>();

                Debug.Log("DogamManager.ChaIndex: " + DogamManager.ChaIndex);
                Debug.Log(GameManager.BuildingPrefabData.Count);

                //게임매니저에 잇는 건물 프리팹 배열에서 같은 이름을 가진 프리팹을 찾아 Instantiate하고 상점 창 닫기
           
                string buildingname = building.Building_Image;
                GameObject buildingprefab = GameManager.BuildingPrefabData[buildingname];


                Transform parent = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent;

                Transform[] Window = parent.GetComponentsInChildren<Transform>();  //StoreWindow
                                                                                   //parent.gameObject.SetActive(false);

                GameManager.CurrentBuilding = buildingprefab;
                Building b = buildingprefab.GetComponent<Building>();
                Building c = GameManager.CurrentBuilding.GetComponent<Building>();
                c.Building_Image = buildingname;
                c = b.GetComponent<Building>().DeepCopy();
                b.Level = 1;
                c.SetValue(b);
                Debug.Log("빌딩은 현재 " +b.Level + "레벨");
                parent.gameObject.SetActive(false);
                isEdit = true;
                
            }

        }

        GameManager.isStore = false;
        GameManager.isMoveLock = false;


        settigPanel.GetComponent<AudioController>().Sound[1].Play();
    }
}


