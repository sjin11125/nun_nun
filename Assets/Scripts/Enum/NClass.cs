using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

[Serializable]
public class Cardsave
{
    
    public string Uid;

    public string cardImage;
    public string isLock;
    public string Id;

    public Cardsave(string uid, string cardImage, string isLock, string id)
    {
        Uid = uid;
        this.cardImage = cardImage;
        this.isLock = isLock;
        Id = id;
    }
}
[Serializable]
public class UIEdit
{
    public BuildUIType buildUIType;
    public Button btn;
    public GameObject prefab;

}
[System.Serializable]
public class UserInfo               //유저정보
{
    public string Uid;
    public string Money;
    public string Message;
    public string Image;    
    public string ShinMoney;
    public string BestScore;
    public string Tuto;
    public string Version;
}
[System.Serializable]
public class SendMessage
{
    public SendMessage(string _name, string _message)
    {
        name = _name;
        message = _message;
    }
    public string name;
    public string message;
}
[System.Serializable]
public class Buildingsave
{
    public string order, result, msg;

    public string Uid;              //플레이어 UID

    public string BuildingPosition_x;                //건물 위치(x좌표)
    public string BuildingPosition_y;                //건물 위치(y좌표)
    //-------------------------파싱정보------------------------------
    public string isLock;               //잠금 유무
    public string Building_name;            //건물 이름
    //public string Reward;               //획득자원
    //public string Info;                 //건물 설명
    public string Building_Image;          //빌딩 이미지 이름 *
    public string Cost;        //건물비용
    public string ShinCost;
    public string Level;       //건물 레벨
    public string isFliped;        //뒤집어졌는지
    public string Id;

    public Buildingsave(string buildingPosition_x, string buildingPosition_y, string isLock, string building_name, string building_Image, string level, string isFlied, string id)
    {
        BuildingPosition_x = buildingPosition_x;
        BuildingPosition_y = buildingPosition_y;
        this.isLock = isLock;
        Building_name = building_name;
        Building_Image = building_Image;
        Level = level;
        this.isFliped = isFlied;
        Id = id;
    }
}
[Serializable]
public class FriendBtn
{
    //public string BtnName;
    public FriendDef FriendUIDef;
    public Button Btn;
    public GameObject Prefab;
}
[Serializable]
public class FriendRank
{
    public string f_nickname;      //�÷��̾� �г���
    //public string SheetsNum;     //�÷��̾� �ǹ� ���� ����ִ� �������� ��Ʈ id
    public string f_score;
    public string f_image;

    public FriendRank(string nickname, string score, string image)
    {
        this.f_nickname = nickname;
        f_score = score;
        f_image = image;

    }
}
[Serializable]
public class FriendInfo
{
    public string FriendName;      //�÷��̾� �г���
    //public string SheetsNum;     //�÷��̾� �ǹ� ���� ����ִ� �������� ��Ʈ id
    public string FriendMessage;          //���¸޼���
    public string FriendImage;

    public FriendInfo(string nickname, string message)
    {
        this.FriendName = nickname;
        this.FriendMessage = message;

    }
}
[Serializable]
public class FriendAddInfo
{
    public string Uid;

    public string FriendName;

    public FriendAddInfo(string uid, string friendName)
    {
        Uid = uid;
        FriendName = friendName;
    }
}