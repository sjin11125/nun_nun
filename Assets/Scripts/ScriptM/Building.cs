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
    #region BuildingProperties
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
    protected Transform[] child;
    
    public GameObject UpgradePannel;
    public GameObject UpgradePannel2;
    public GameObject RemovePannel;

    protected GameObject Parent;

    public GameObject[] buildings;     // 레벨별 건물
    public Sprite[] buildings_image;    //레벨별 건물 이미지(2,3레벨)

    
    public BuildType Type;

    public BuildingSave save;

    protected bool isUp;

    #endregion
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

}

