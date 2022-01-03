using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
//using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    public string Building_Image;          //빌딩 이미지 이름 *
    public Vector2 BuildingPosition;    //*
    public bool Placed=false;    //*
    public BoundsInt area;
    

    public Transform Coin_Button;      //*

    public float currentTime = 0;      //*
    public float startingTime = 60f;   //*

    public Transform Button_Pannel;    //*

    public bool isCoin = false;        //*
    public bool isCountCoin = false;   //*
    public int CountCoin = 0;      //*
    public int Cost;        //건물비용
    public int Deco;        //데코

    public int layer_y;   // 건물 레이어
    Transform[] child;
    public int level=0;       //건물 레벨
    public GameObject UpgradePannel;
    public GameObject Level1building;
    public GameObject Level2building;
    public GameObject Level3building;

    GameObject Parent;
    public Building DeepCopy()
    {
        Building BuildingCopy = new Building();
        BuildingCopy.Building_Image = this.Building_Image;
        //Debug.Log(BuildingCopy.Building_Image.name);
        BuildingCopy.BuildingPosition = this.BuildingPosition;
        BuildingCopy.Placed = this.Placed;
        BuildingCopy.area = this.area;
        BuildingCopy.Coin_Button = this.Coin_Button;
        BuildingCopy.currentTime = this.currentTime;
        BuildingCopy.startingTime = this.startingTime;
        BuildingCopy.Button_Pannel= Button_Pannel;
        BuildingCopy.isCoin = this.isCoin;
       BuildingCopy.isCountCoin = this.isCountCoin;
        BuildingCopy.CountCoin = this.CountCoin;
        BuildingCopy.Cost = this.Cost;
        BuildingCopy.layer_y = this.layer_y;
        BuildingCopy.level = this.level;
        BuildingCopy.Level1building = Level1building;
        BuildingCopy.Level2building = Level2building;
        BuildingCopy.Level3building = Level3building;
        BuildingCopy.Deco = Deco;

        return BuildingCopy;
    }
    void Start()
    {
        currentTime = (int)startingTime;

        //TimeText = GameObject.Find("Canvas/TimeText"); //게임오브젝트 = 캔버스에 있는 TimeText로 설정
        Building_Image = gameObject.name;       //이름 설정
        //Placed = false;

        child = GetComponentsInChildren<Transform> ();

       // Debug.Log(child[6].name);
        //Coin_Button= child[6];
        //Button_Pannel = child[2];

        Coin_Button.gameObject.SetActive(false);
        //Text countdownText = GetComponent<Text>();
        Parent = GameObject.Find("buildings");
        layer_y = 10;
        child[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
    }

    void Update()
    {
       // layer_y = 1;             //레이어 설정
       

        child[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;

        // text.text = currentTime.ToString("0.0");
        //TimeText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.30f, 1.4f, 0)); //Timer위치

        //이제 추가해야할 것은 건물을 눌렀을때 시간이 뜨도록 하기 (이거는 나중에)
        //건물이 생성되면 시간도 생성되어야 함 (이것도 나중에)


        // 시간이 흐르는 것이 계속 저장되도록 하기


        // 아이콘을 누르면 재화 + 
        // current Time이 일정시간 밑으로 떨어졌을 때 수확 아이콘 생성


        if (Placed==true)       // 건물 배치가 확정
        {
            Button_Pannel.gameObject.SetActive(false);     // 배치하는 버튼 사라지게

            if (isCoin==false)      //코인 아직 안먹었으면
            {
                Coin();     //재화 생성되게
            }
        }
        else
        {
            Button_Pannel.gameObject.SetActive(true);
        }



    }
    public void Coin() //재화부분
    {
        
        //float currentTime_1 = currentTime;
        //currentTime_1 -= 1 * Time.deltaTime;
        currentTime -= 1 * Time.deltaTime;
        //currentTime = (int)currentTime_1;

        if ((int)currentTime  <= 0)
        {
            currentTime = 0;
        }
        
        if ((int)currentTime % 5 == 0 && (int)currentTime != startingTime&&isCountCoin==false)      //생성되고 5초 마다 재화생성 (건물마다 다르다!)
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


    //눌렀을때 슬라이드 막대 setactive
    //마우스 raycast 추가
    /*void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                sliderManager.SetActive(true);
            }

        }
    }
   */




    #region Build Methods
    public bool CanBePlaced()
    {
        if (GridBuildingSystem.current.gridLayout == null)
        {
            Debug.Log("subak");
        }
        Debug.Log(transform.position);
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
      

        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        
        GridBuildingSystem.current.TakeArea(areaTemp);

        //currentTime = startingTime;
        //원래 업데이트 부분
        BuildingPosition =transform.position;          //위치 저장
        layer_y = (int)(-transform.position.y/0.6);             //레이어 설정
       
        if (layer_y == 0|| layer_y == 1)
        {
            layer_y = 2;
        }

        child[1].GetComponent<SpriteRenderer>().sortingOrder = layer_y;
        Building BuildingCurrent = gameObject.GetComponent<Building>();


        GameManager.BuildingList.Add(gameObject);      //현재 가지고 있는 빌딩 리스트에 추가

        GameManager.BuildingArray = GameManager.BuildingList.ToArray();
        

        GameManager.CurrentBuilding = null;
        gameObject.transform.parent = Parent.transform;
    }

    #endregion 
    // Update is called once per frame
    public void Upgrade()
    {
        if (level<3)
        {
            //GameObject UPPannel = Instantiate(UpgradePannel);
            UpgradePannel.gameObject.SetActive(true);

            GameObject[] buildings = { Level1building, Level2building, Level3building };
            UpgradePannel.GetComponent<ChaButtonScript>().Upgrade(buildings, level);

        }

    }

    public GameObject[] Buildings()
    {
        GameObject[] buildings = { Level1building, Level2building, Level3building };
        return buildings;
    }



}
