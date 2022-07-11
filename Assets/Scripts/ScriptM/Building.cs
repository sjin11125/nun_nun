using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Rendering;
//using UnityEngine.EventSystems;

[Serializable]
public class BuildingParse
{
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public string Info;                 //건물 설명
    public int[] Reward = new int[3] { 0, 0, 0 };            //획득자원
    public string Building_Image;          //빌딩 이미지 이름 *
    public int[] Cost = new int[3] { 0, 0, 0 };          //건물 비용
    public int[] ShinCost = new int[3] { 0, 0, 0 };          //건물 비용
    public int Level = 1;       //건물 레벨
    public string isFliped = "F";
    public string BuildingPosiiton_x;
    public string BuildingPosiiton_y;
    public string Id;
    public string str;      //설치물인지
    //-----------------------------------------------------------

}
public class Building : MonoBehaviour
{
    
        //*
    public bool Placed = false;    //*
    public BoundsInt area;


    public Transform Coin_Button;      //*

    public float currentTime = 0;      //*
    public float startingTime = 60f;   //*

    public Transform Button_Pannel;    //*
    public Transform Rotation_Pannel;
    public Transform Remove_Pannel;

    public bool isCoin = false;        //*
    public bool isCountCoin = false;   //*
    public int CountCoin = 0;      //*

    public Vector2 BuildingPosition;                //건물 위치
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    public int[] Reward =new int[3] { 0, 0, 0 };               //획득자원
    public string Info;                 //건물 설명
    public string Building_Image;          //빌딩 이미지 이름 *
    public int[] Cost = new int[3] { 0, 0, 0 };        //건물비용
    public int[] ShinCost = new int[3] { 0, 0, 0 };
    public int Level = 1;       //건물 레벨
    public string isFliped = "F";
    public string BuildingPosiiton_x;
    public string BuildingPosiiton_y;
    public string Id;
    public string str;      //설치물인지
    //-----------------------------------------------------------

    public int layer_y;   // 건물 레이어
    Transform[] child;
    
    public GameObject UpgradePannel;
    public GameObject UpgradePannel2;
    public GameObject RemovePannel;

    GameObject Parent;

    public GameObject[] buildings;     // 레벨별 건물
    public Sprite[] buildings_image;    //레벨별 건물 이미지(2,3레벨)

    
    public BuildType Type;

    public BuildingSave save;

    bool isUp;
    public Building()
    {
    }
    public Building(string islock, string buildingname, string info, string image, string cost,string cost2,string cost3, string Reward, string Reward2, string Reward3, string isStr)           //파싱할 때 쓰는 생성자
    {//잠금 유무     // 이름     //설명     //이미지    //가격1       //가격2      //가격3        //생성재화1         //생성재화2        //생성재화3

        isLock = islock;
        Building_name = buildingname;

        this.Reward[0] =int.Parse(Reward) ;                 //생성재화
        this.Reward[1] = int.Parse(Reward2);
        this.Reward[2] = int.Parse(Reward3);

        Info = info;                //건물 설명

        Building_Image = image;     //건물 이미지

        string[] Cost=cost.ToString().Split('*');           
        string[] Cost2=cost2.ToString().Split('*');
        string[] Cost3=cost3.ToString().Split('*');

        this.Cost[0] = int.Parse(Cost[0]);              //비용(골드)
        this.Cost[1] = int.Parse(Cost2[0]);
        this.Cost[2] = int.Parse(Cost3[0]);



        this.ShinCost[0] = int.Parse(Cost[1]);                //비용(발광석)
        this.ShinCost[1] = int.Parse(Cost2[1]);
        this.ShinCost[2] = int.Parse(Cost3[1]);

        this.str = isStr;
        Level = 1;

    }
  /*  public Building(string islock, string buildingname,string reward,string info,string image,string cost, string shinCost, string level,string isfliped,string building_x,string building_y)           //파싱할 때 쓰는 생성자
    {
        isLock = islock;
        Building_name = buildingname;
        Reward = reward;
        Info = info;
        Building_Image = image;
        Cost =int.Parse(cost);
        ShinCost = int.Parse(shinCost);
        Level =int.Parse(level);
        isFliped = isfliped;
        BuildingPosiiton_x = building_x;
        BuildingPosiiton_y= building_y;


    }*/
    public void SetValue(Building getBuilding)
    {
        isLock = getBuilding.isLock;
        Building_name = getBuilding.Building_name;
        Building_Image = getBuilding.Building_Image;
        BuildingPosition = getBuilding.BuildingPosition;
        Placed = getBuilding.Placed;
        //area = getBuilding.area;
        currentTime = getBuilding.currentTime;
        startingTime = getBuilding.startingTime;
        isCoin = getBuilding.isCoin;
        isCountCoin = getBuilding.isCountCoin;
        CountCoin = getBuilding.CountCoin;
        Cost = getBuilding.Cost;
        ShinCost = getBuilding.ShinCost;
        layer_y = getBuilding.layer_y;
        Level = getBuilding.Level;
        isFliped = getBuilding.isFliped;
       BuildingPosiiton_x = getBuilding.BuildingPosiiton_x;
        BuildingPosiiton_y = getBuilding.BuildingPosiiton_y;
        Reward = getBuilding.Reward;
        Id = getBuilding.Id;
    }
    public void SetValueParse(BuildingParse parse)
    {
        isLock = parse.isLock;               //잠금 유무
        Building_name = parse.Building_name;            //건물 이름
        Reward = parse.Reward;               //획득자원
        Info = parse.Info;                 //건물 설명
        Building_Image = parse.Building_Image;          //빌딩 이미지 이름 *
        Cost = parse.Cost;        //건물비용
        ShinCost = parse.ShinCost;
        Level = parse.Level;       //건물 레벨
        isFliped = parse.isFliped;
        BuildingPosiiton_x = parse.BuildingPosiiton_x;
        BuildingPosiiton_y = parse.BuildingPosiiton_y;
        Id = parse.Id;
    }
    public Building DeepCopy()
    {
        Building BuildingCopy = new Building();
        BuildingCopy.isLock = isLock;
        BuildingCopy.Building_name = this.Building_name;
        BuildingCopy.Building_Image = this.Building_Image;
        //Debug.Log(BuildingCopy.Building_Image.name);
        BuildingCopy.BuildingPosition = this.BuildingPosition;
        BuildingCopy.Placed = this.Placed;
        BuildingCopy.area = this.area;
        BuildingCopy.currentTime = this.currentTime;
        BuildingCopy.startingTime = this.startingTime;
        BuildingCopy.isCoin = this.isCoin;
        BuildingCopy.isCountCoin = this.isCountCoin;
        BuildingCopy.CountCoin = this.CountCoin;
        
        BuildingCopy.layer_y = this.layer_y;
        BuildingCopy.Level = this.Level;

        BuildingCopy.Cost = this.Cost;
        BuildingCopy.ShinCost = this.ShinCost;
        BuildingCopy.Id = this.Id;
        BuildingCopy.isFliped = isFliped;
        return BuildingCopy;
    }
    public void RefreshBuildingList()               //빌딩 리스트 새로고침
    {
        for (int i = 0; i < GameManager.BuildingList.Count; i++)
        {
            if (GameManager.BuildingList[i].Id.Equals(Id))
            {
                GameManager.BuildingList[i] = this.DeepCopy();
            }
        }
        GridBuildingSystem.isSave = true;

    }
    public void Rotation()          //건물 회전
    {
        bool isflip_bool;

        if (isFliped .Equals( "F"))
            isflip_bool = false;
        else
            isflip_bool = true;


        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null)
            {
                SpriteRenderer[] spriterenderer = buildings[i].GetComponentsInChildren<SpriteRenderer>();
                Transform[] transform = buildings[i].GetComponentsInChildren<Transform>();

               
                for (int j = 0; j < spriterenderer.Length; j++)
                {
                    spriterenderer[j].flipX = isflip_bool;
                }
                for (int k = 0; k < transform.Length; k++)
                {
                    transform[k].localPosition = new Vector3(-transform[k].localPosition.x, transform[k].localPosition.y, 0);
                }
                if (isFliped .Equals( "T"))
                {
                    isFliped = "F";
                }
                else
                    isFliped = "T";
            }
            else
            {
                Debug.Log("Rotation_No");
            }
        }
        RefreshBuildingList();          //건물 리스트 새로고침
    }

    void Awake()
    {
        Parent = GameObject.Find("buildings");
    }
    void Start()
    {
        bool isflip_bool;

        if (isFliped .Equals( "F"))
            isflip_bool = false;
        else
            isflip_bool = true;
        
        buildings = new GameObject[2];
        currentTime = (int)startingTime;
        save = GetComponent<BuildingSave>();
        //TimeText = GameObject.Find("Canvas/TimeText"); //게임오브젝트 = 캔버스에 있는 TimeText로 설정
        if (Type .Equals( BuildType.Make))
        {
            Building_Image = gameObject.name;       //이름 설정
        }

        //Placed = false;

       // child = GetComponentsInChildren<Transform>();

        // Debug.Log(child[6].name);
        //Coin_Button= child[6];
        //Button_Pannel = child[2];

        Coin_Button.gameObject.SetActive(false);

        //Text countdownText = GetComponent<Text>();

        //layer_y = 10;
        //child[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;


        //-------------레벨 별 건물--------------------
        GameObject Level1building, Level2building, Level3building;
        if (Level <= 3)
        {
            //GameObject UPPannel = Instantiate(UpgradePannel);
            Level1building = gameObject.transform.Find("building").gameObject;
            if (gameObject.transform.Find("building2").gameObject!=null)
            {

            }
            Level2building = gameObject.transform.Find("building2").gameObject;
            //Level3building = gameObject.transform.Find("building3").gameObject;
            buildings[0] = Level1building;
            buildings[1] = Level2building;
           // buildings[2] = Level3building;
        }

        switch (Level)
        {
            case 1:
                buildings[0].SetActive(true);
                //buildings[1].SetActive(false);
                //buildings[2].SetActive(false);
                //buildings[0].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
                child = GetComponentsInChildren<Transform>();
                //child[1].GetComponent<SpriteRenderer>().sortingOrder = (int)transform.position.y;
               
                    buildings[0].GetComponent<SortingGroup>().sortingOrder = -(int)transform.position.y ;
               
                //buildings[0].GetComponent<SortingGroup>().sortingOrder = -((int)transform.position.y + (int)transform.position.x);
                /*if((int)transform.position.x<0)
                    buildings[0].GetComponent<SortingGroup>().sortingOrder = -((int)transform.position.y + (int)transform.position.x);
                else
                {
                    buildings[0].GetComponent<SortingGroup>().sortingOrder = ((int)transform.position.y - (int)transform.position.x);

                }*/
                // SpriteRenderer[] buildingsSprite= gameObject.GetComponentsInChildren<SpriteRenderer>();
                //buildingsSprite[1].sortingOrder = layer_y;

                break;
            case 2:
                buildings[0].SetActive(true);
                //buildings[1].SetActive(true);
                //buildings[2].SetActive(false);
                buildings[0].GetComponent<SpriteRenderer>().sprite = buildings_image[Level-2];
                buildings[0].GetComponent<SortingGroup>().sortingOrder = -(int)transform.position.y;
               
                    buildings[1].GetComponentInChildren<SortingGroup>().sortingOrder = (-buildings[0].GetComponent<SortingGroup>().sortingOrder)+1;
                Debug.Log(" buildings[0]:  " + buildings[0].transform.parent.gameObject.name);
                Debug.Log(" buildings[0] layer:  " + buildings[0].GetComponent<SortingGroup>().sortingOrder);

                break;
            case 3:
                buildings[0].SetActive(false);
                buildings[1].SetActive(true);
               // buildings[2].SetActive(true);
                //buildings[2].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
                break;
            default:
                break;
        }
        if (isflip_bool .Equals( true))
        {
            Rotation();
        }
    }

    void Update()
    {
        // layer_y = 1;             //레이어 설정



        // text.text = currentTime.ToString("0.0");
        //TimeText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.30f, 1.4f, 0)); //Timer위치

        //이제 추가해야할 것은 건물을 눌렀을때 시간이 뜨도록 하기 (이거는 나중에)
        //건물이 생성되면 시간도 생성되어야 함 (이것도 나중에)


        // 시간이 흐르는 것이 계속 저장되도록 하기


        // 아이콘을 누르면 재화 + 
        // current Time이 일정시간 밑으로 떨어졌을 때 수확 아이콘 생성


        if (Placed .Equals( true))       // 건물 배치가 확정
        {
            Button_Pannel.gameObject.SetActive(false);     // 배치하는 버튼 사라지게
            Rotation_Pannel.gameObject.SetActive(false);        //회전 버튼 사라지게
            UpgradePannel.gameObject.SetActive(false);
            Remove_Pannel.gameObject.SetActive(false);
            /*if (Building_Image != "bunsu_level(Clone)")
            {
               ( if (isCoin .Equals( false)      //코인 아직 안먹었으면
                {
                    Coin();     //재화 생성되게
                }*
            }*/
        }
        else                            //확정 아닐 때
        {

            Button_Pannel.gameObject.SetActive(true);               //확정 패널 뜨게
            Rotation_Pannel.gameObject.SetActive(true);               //회전 패널 뜨게
            if (Building_Image != "bunsu_level(Clone)")
            {
                
                if (Type != BuildType.Make)
                {
                    UpgradePannel.gameObject.SetActive(true);

                }
            }
            if (Building_Image != "village_level(Clone)")
            {
                Remove_Pannel.gameObject.SetActive(true);
            }
        }
       



    }
    public void Coin() //재화부분
    {

        //float currentTime_1 = currentTime;
        //currentTime_1 -= 1 * Time.deltaTime;
        currentTime -= 1 * Time.deltaTime;
        //currentTime = (int)currentTime_1;

        if ((int)currentTime <= 0)
        {
            currentTime = 0;
        }

        if (((int)currentTime % 5 ).Equals( 0) && (int)currentTime != startingTime && isCountCoin .Equals( false) )     //생성되고 5초 마다 재화생성 (건물마다 다르다!)
        {
            isCountCoin = true;
            //CountCoin += 1;

            Coin_Button.gameObject.SetActive(true);
        }
        else if ((int)currentTime % 5 != 0)
        {
            isCountCoin = false;
        }

        // 재화를 누르면 current Time 초기화 or 0이 되면 이미지 MAX coin으로 변환 후 수확하면 currentTime = startingTime




    }

    public void Coin_OK()       //재화 버튼 누르는 함수
    {
        //currentTime =  startingTime;
        isCoin = true;      //코인 먹었음
        GameManager.Money += Reward[Level-1];

        currentTime = (int)startingTime;

        isCoin = true;

        if (currentTime .Equals( 0))
        {
            //수정필요
            currentTime = (int)startingTime;
            //Max 이미지로 바뀜
        }
        Coin_Button.gameObject.SetActive(false);
    }







    #region Build Methods
    public bool CanBePlaced()           //건물이 놓여질 수 있는지 체크
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);     //현재위치
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;


        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }
    public void Sell_Pannel()
    {

        RemovePannel.gameObject.SetActive(true);
        RemovePannel.transform.parent = GameObject.Find("O").transform;
        RemovePannel.GetComponent<RectTransform>().localPosition = new Vector3(1, 1, 0);
        RemovePannel.GetComponent<ChaButtonScript>().DowngradeBuilding = this;

    }
    public void Remove(Building building)
    {

        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        GridBuildingSystem.current.RemoveArea(areaTemp);

     
       

        
        if (Type .Equals( BuildType.Make) )     //상점에서 사고 설치X 바로 제거
        {
            GameManager.Money += building.Cost[building.Level - 1];          //자원 되돌리기
            GameManager.ShinMoney += building.ShinCost[building.Level - 1];
            Destroy(gameObject);
        }
        else                                //설치하고 제거
        {
            GameManager.Money += building.Cost[building.Level - 1] / 10;          //자원 되돌리기
            GameManager.ShinMoney += building.ShinCost[building.Level - 1] / 3;

            BuildingListRemove();
            save.RemoveValue(Id);
            Destroy(gameObject);
        }
        GameManager.isUpdate = true;

    }
    public void Place_Initial(BuildType buildtype)
    {
        Vector3 vec = new Vector3(float.Parse(BuildingPosiiton_x), float.Parse(BuildingPosiiton_y), 0);
        area.position = GridBuildingSystem.current.gridLayout.WorldToCell(vec);
        BoundsInt areaTemp = area;
        //areaTemp.position = positionInt;
        Placed = true;      // 배치 했니? 네
        GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정
        transform.position = vec;
        //Debug.Log("index: "+ (-((int)transform.position.y - (int)transform.position.x)));
       /* buildings[0].GetComponent<SortingGroup>().sortingOrder = -((int)transform.position.y- (int)transform.position.x);
        if (Level .Equals( 2)
        {
            buildings[1].GetComponentInChildren<SortingGroup>().sortingOrder = -((int)transform.position.y-(int)transform.position.x);
        }*/
    }
    public void Place(BuildType buildtype)         //건물 배치
    {

        Vector3 vec = transform.position;
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(vec);
        BoundsInt areaTemp = area;
        //areaTemp.position = positionInt;
        //Debug.Log(areaTemp.position);
        Placed = true;      // 배치 했니? 네
        Debug.Log(buildings.Length);
        buildings[0].GetComponent<SortingGroup>().sortingOrder = -(int)transform.position.y;
        /*if (Level.Equals(2)
        {
            buildings[1].GetComponentInChildren<SortingGroup>().sortingOrder = -(int)transform.position.y;
        }*/
        GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정

        //currentTime = startingTime;
        //원래 업데이트 부분
        BuildingPosition = transform.position;          //위치 저장
        layer_y = (int)-transform.position.y;      //레이어 설정
        isLock = "T";           //배치했다

       /* for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null)
            {
                buildings[i].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
            }
        }*/

        if (buildtype .Equals( BuildType.Make)  )                     //새로 만드는 건가?
        {
            
            Building_name = gameObject.name;
            Debug.Log("BuildingPosiiton_x: " + BuildingPosiiton_x);
            GameManager.BuildingNumber[Building_Image]++; //해당 건물의 갯수 추가
            Id = GameManager.IDGenerator();
            gameObject.name = Id;      //이름 재설정
            BuildingListAdd();      //현재 가지고 있는 건물 리스트에 추가
            buildtype = BuildType.Empty;
            Debug.Log("새로만듬");
            
        }
        else if (buildtype .Equals( BuildType.Load)  )                  //로드할때
        {
            buildtype = BuildType.Empty;
        }
        else if (buildtype .Equals( BuildType.Move)   )            //이동할 때
        {
            Debug.Log("Move");
            gameObject.name = GameManager.CurrentBuilding_Script.Id;
            Id = GameManager.CurrentBuilding_Script.Id;
            Building_name = GameManager.CurrentBuilding_Script.Building_name;
            isLock = "T";
            RefreshBuildingList();

            buildtype = BuildType.Empty;

            save.UpdateValue(this);
        }
        else
        {
            save.UpdateValue(this);
        }

        gameObject.transform.parent = Parent.transform;
        GridBuildingSystem.current.temp_gameObject = null;
    }
    public void BuildingListRemove()
    {
        for (int i = GameManager.BuildingList.Count - 1; i >=0; i--)
        {
            if (GameManager.BuildingList[i].Building_name .Equals( Building_name))
            {
                Debug.Log("Remove: "+GameManager.BuildingList[i].Building_name);
                GameManager.BuildingList.RemoveAt(i);
                for (int p = 0; p < GameManager.BuildingList.Count; p++)
                {
                    Debug.Log("Current: " + GameManager.BuildingList[p].Building_name);
                }
                return;
            }
            
        }

        GridBuildingSystem.isSave = true;
        
    }
    public void BuildingListAdd()
    {
        GameManager.BuildingList.Add(this.DeepCopy());      //현재 가지고 있는 빌딩 리스트에 추가

        //GameManager.BuildingArray = GameManager.BuildingList.ToArray();
        Debug.Log("GameManager.BuildingArray: "+ GameManager.BuildingArray.Length);

        GameManager.CurrentBuilding = null;
        //

        save.AddValue();
        //GameManager.isUpdate = true;
    }
    #endregion
    // Update is called once per frame
  
    
    public bool Upgrade()
    { //GameObject Level1building, Level2building, Level3building;
        Debug.Log("내 빌딩 이미지: " +Building_Image);
        if (Level < 2)
        {
            if (Building_Image == "building_level(Clone)" ||
                   Building_Image == "village_level(Clone)" ||
                   Building_Image == "flower_level(Clone)")
            {
                Debug.Log("해당 건물마자");
                for (int i = 0; i < GameManager.CharacterList.Count; i++)
                {
                    if (GameManager.CharacterList[i].cardName == "수리공누니")
                    {
                        Debug.Log("해당 누니이써");
                        isUp = true;

                    }
                }
            }
            //GameObject UPPannel = Instantiate(UpgradePannel);
            if (Building_Image == "syrup_level(Clone)" ||
             Building_Image == "fashion_level(Clone)" ||
             Building_Image == "school_level(Clone)")
            {
                Debug.Log("해당 건물마자22");
                for (int i = 0; i < GameManager.CharacterList.Count; i++)
                {
                    if (GameManager.CharacterList[i].cardName == "페인트누니")
                    {
                        Debug.Log("해당 누니이써222");
                        isUp = true;

                    }
                }
            }
            if (isUp == true)
            {


                UpgradePannel2.GetComponent<ChaButtonScript>().Upgrade(buildings, Level, this);
                UpgradePannel2.gameObject.SetActive(true);


                Text[] upgradeText = UpgradePannel2.GetComponentsInChildren<Text>();
                Debug.Log("업그레이드 아이디: " + Id);

            


                for (int i = 0; i < GameManager.BuildingList.Count; i++)
                {
                    for (int j = 0; j < GameManager.BuildingArray.Length; j++)
                    {
                        if (Building_Image == GameManager.BuildingArray[j].Building_Image)
                        {

                            upgradeText[3].text = GameManager.BuildingArray[j].Reward[Level - 1].ToString();     //업글 전 획득 재화
                            Debug.Log("업글전: " + GameManager.BuildingArray[j].Reward[Level - 1]);
                            upgradeText[4].text = GameManager.BuildingArray[j].Reward[Level].ToString();                       //업글 후 획득 재화
                            Debug.Log("업글전: " + GameManager.BuildingArray[j].Reward[Level - 1]);
                            upgradeText[6].text = "얼음: " + GameManager.BuildingArray[j].Cost[Level].ToString() + ",   빛나는 얼음: " + GameManager.BuildingArray[j].ShinCost[Level].ToString() + " 이 소모됩니다.";
                            return true;

                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        return isUp;
    }

 


}
public enum BuildType
{
    Empty,
    Load,
    Move,
    Rotation,
    Make,
    Upgrade
}
