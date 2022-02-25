using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
//using UnityEngine.EventSystems;

[Serializable]
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
    public string Reward;               //획득자원
    public string Info;                 //건물 설명
    public string Building_Image;          //빌딩 이미지 이름 *
    public int Cost;        //건물비용
    public int Level = 1;       //건물 레벨
    public int Tree;        //나무
    public int Ice;        //얼음
    public int Grass;        //풀
    public int Snow;        //눈
    public bool isFliped = false;
    //-----------------------------------------------------------
    public string buildingPosiiton_x;
    public string buildingPosiiton_y;


    public int layer_y;   // 건물 레이어
    Transform[] child;
    
    public GameObject UpgradePannel;
    public GameObject UpgradePannel2;

    GameObject Parent;

    public GameObject[] buildings;     // 레벨별 건물

    
    public BuildType Type;

    BuildingSave save;
    public Building()
    {
    }
    public Building(string islock, string buildingname,string reward,string info,string image,string cost,string level,string tree,string grass,string snow,string ice)           //파싱할 때 쓰는 생성자
    {
        isLock = islock;
        Building_name = buildingname;
        Reward = reward;
        Info = info;
        Building_Image = image;
        Cost =int.Parse( cost);
        Level =int.Parse( level);
        Tree = int.Parse(tree);
        Grass = int.Parse(grass);
        Snow = int.Parse(snow);
        Ice = int.Parse(ice);

    }
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
        layer_y = getBuilding.layer_y;
        Level = getBuilding.Level;
        Tree = getBuilding.Tree;
        Ice = getBuilding.Ice;
        Snow = getBuilding.Snow;
        Grass = getBuilding.Grass;
        isFliped = getBuilding.isFliped;
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
        BuildingCopy.Tree = Tree;
        BuildingCopy.Ice = Ice;
        BuildingCopy.Snow = Snow;
        BuildingCopy.Grass = Grass;

        BuildingCopy.isFliped = isFliped;
        return BuildingCopy;
    }
    public void RefreshBuildingList()               //빌딩 리스트 새로고침
    {
        for (int i = 0; i < GameManager.BuildingList.Count; i++)
        {
            if (GameManager.BuildingList[i].Building_name == Building_name)
            {
                GameManager.BuildingList[i] = this.DeepCopy();
            }
        }
        GridBuildingSystem.isSave = true;

    }
    public void Rotation()          //건물 회전
    {

        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null)
            {
                Debug.Log("Rotation");
                SpriteRenderer[] spriterenderer = buildings[i].GetComponentsInChildren<SpriteRenderer>();
                Transform[] transform = buildings[i].GetComponentsInChildren<Transform>();
                for (int j = 0; j < spriterenderer.Length; j++)
                {
                    spriterenderer[j].flipX = isFliped;

                }
                for (int k = 0; k < transform.Length; k++)
                {
                    transform[k].localPosition = new Vector3(-transform[k].localPosition.x, transform[k].localPosition.y, 0);
                }
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
        Debug.Log("Start Level: " + Level);
        buildings = new GameObject[3];
        currentTime = (int)startingTime;
        save = GetComponent<BuildingSave>();
        //TimeText = GameObject.Find("Canvas/TimeText"); //게임오브젝트 = 캔버스에 있는 TimeText로 설정
        if (Type == BuildType.Make)
        {
            Building_Image = gameObject.name;       //이름 설정
        }

        //Placed = false;

        child = GetComponentsInChildren<Transform>();

        // Debug.Log(child[6].name);
        //Coin_Button= child[6];
        //Button_Pannel = child[2];

        Coin_Button.gameObject.SetActive(false);

        //Text countdownText = GetComponent<Text>();

        layer_y = 10;
        child[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;


        //-------------레벨 별 건물--------------------
        GameObject Level1building, Level2building, Level3building;
        if (Level <= 3)
        {
            //GameObject UPPannel = Instantiate(UpgradePannel);
            Level1building = gameObject.transform.Find("building").gameObject;
            Level2building = gameObject.transform.Find("building2").gameObject;
            Level3building = gameObject.transform.Find("building3").gameObject;
            buildings[0] = Level1building;
            buildings[1] = Level2building;
            buildings[2] = Level3building;
        }

        switch (Level)
        {
            case 1:
                buildings[0].SetActive(true);
                buildings[1].SetActive(false);
                buildings[2].SetActive(false);
                buildings[0].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
                break;
            case 2:
                buildings[0].SetActive(false);
                buildings[1].SetActive(true);
                buildings[2].SetActive(false);
                buildings[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
                break;
            case 3:
                buildings[0].SetActive(false);
                buildings[1].SetActive(true);
                buildings[2].SetActive(true);
                buildings[2].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
                break;
            default:
                break;
        }
        if (isFliped == true)
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


        if (Placed == true)       // 건물 배치가 확정
        {
            Button_Pannel.gameObject.SetActive(false);     // 배치하는 버튼 사라지게
            Rotation_Pannel.gameObject.SetActive(false);        //회전 버튼 사라지게
            UpgradePannel.gameObject.SetActive(false);
            Remove_Pannel.gameObject.SetActive(false);
            if (isCoin == false)      //코인 아직 안먹었으면
            {
                Coin();     //재화 생성되게
            }
        }
        else
        {
            Button_Pannel.gameObject.SetActive(true);
            Rotation_Pannel.gameObject.SetActive(true);
            Remove_Pannel.gameObject.SetActive(true);
            if (Type != BuildType.Make)
            {
                UpgradePannel.gameObject.SetActive(true);

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

        if ((int)currentTime % 5 == 0 && (int)currentTime != startingTime && isCountCoin == false)      //생성되고 5초 마다 재화생성 (건물마다 다르다!)
        {
            isCountCoin = true;
            CountCoin += 1;

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
        Debug.Log("coco");
        GameManager.Money += CountCoin * 100;


        currentTime = (int)startingTime;

        isCoin = true;

        if (currentTime == 0)
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
    public void Remove(Building building)
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        //Debug.Log()
        GameManager.Money += building.Cost;          //자원 되돌리기
        GameManager.Tree += building.Tree;
        GameManager.Snow += building.Snow;
        GameManager.Grass += building.Grass;
        GameManager.Ice += building.Ice;

        GridBuildingSystem.current.RemoveArea(areaTemp);
        if (Type == BuildType.Make)      //상점에서 사고 설치X 바로 제거
        {
            Destroy(gameObject);
        }
        else                                //설치하고 제거
        {
            BuildingListRemove();
            save.RemoveValue(Building_name);
            Destroy(gameObject);
        }
    }
    public void Place(BuildType buildtype)         //건물 배치
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;      // 배치 했니? 네

        GridBuildingSystem.current.TakeArea(areaTemp);      //타일 맵 설정

        //currentTime = startingTime;
        //원래 업데이트 부분
        BuildingPosition = transform.position;          //위치 저장
        layer_y = (int)(-transform.position.y / 0.6);             //레이어 설정

        if (layer_y == 0 || layer_y == 1)
        {
            layer_y = 2;
        }
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null)
            {
                buildings[i].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
            }
        }
        Building BuildingCurrent = gameObject.GetComponent<Building>();


        if (buildtype == BuildType.Make)                       //새로 만드는 건가?
        {
            gameObject.name = Building_Image + GameManager.BuildingNumber[Building_Image];      //이름 재설정
            Building_name = gameObject.name;
            Debug.Log("Building_Image: " + Building_Image);
            GameManager.BuildingNumber[Building_Image]++; //해당 건물의 갯수 추가
            BuildingListAdd();      //현재 가지고 있는 건물 리스트에 추가
            buildtype = BuildType.Empty;

        }
        else if (buildtype == BuildType.Load)                    //로드할때
        {
            buildtype = BuildType.Empty;
        }
        else if (buildtype == BuildType.Move)               //이동할 때
        {
            Debug.Log("Move");
            RefreshBuildingList();

            buildtype = BuildType.Empty;

            save.UpdateValue(this);
        }
        else
        {
            save.UpdateValue(this);
        }

        gameObject.transform.parent = Parent.transform;

    }
    public void BuildingListRemove()
    {
        for (int i = GameManager.BuildingList.Count - 1; i >=0; i--)
        {
            if (GameManager.BuildingList[i].Building_name == Building_name)
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

        GameManager.BuildingArray = GameManager.BuildingList.ToArray();


        GameManager.CurrentBuilding = null;
        //

        save.AddValue();
    } 
    #endregion 
    // Update is called once per frame
    public void Upgrade()
    { //GameObject Level1building, Level2building, Level3building;
        if (Level < 3)
        {
            //GameObject UPPannel = Instantiate(UpgradePannel);
            UpgradePannel2.gameObject.SetActive(true);
            Debug.Log("buildings.length: "+buildings.Length);
            UpgradePannel2.GetComponent<ChaButtonScript>().Upgrade(buildings, Level, this);

        }
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
