using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Newtonsoft.Json;
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
    public string NickName;
    public string Money;
    public string Message;
    public string Image;    
    public string ShinMoney;
    public string Zem;
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
[Serializable]
public class MainUIBtn
{
    public UIBtn UiBtnName;
    public Button Btn;
    public GameObject UIPanelPrefab;
    public GameObject NewImagePrefab;

}

[Serializable]
public class VisitorBookInfo
{
    public string Uid;
    public string FriendName;      //친구
    public string FriendMessage;        //친구가 보낸 메세지
    public string FriendTime;        //친구가 보낸 시간
    public string FriendImage;        //친구프사

    public VisitorBookInfo(string frienName, string frienMessage, string friendTime, string friendImage)
    {
        FriendName = frienName;
        FriendMessage = frienMessage;
        FriendTime = friendTime;
        FriendImage = friendImage;
    }
}
[Serializable]
public class AchieveMenu
{
    public AchieveMenuType Type;
    public Button Btn;
    public GameObject Prefab;

}
[Serializable]
public class AchieveCount
{
  public  string CountType;
    public string Count;
}
[Serializable]
public class AchieveReward
{
    public string RewardType;
    public string Reward;
}
[Serializable]
public class AchieveInfo
{
    public string AchieveName,Context;
    public int[] Count;
    public string[] CountType;
    //public AchieveCountType[] CountType;
    // public string[] Count;
    public string[] Reward;
    public string[] RewardType;
    // public AchieveRewardType[] RewardType;
    //public string[] Reward;
    public string Id;
    public bool isClear;            //새로 클리어했는지 여부
}
[Serializable]
public class MyAchieveInfo
{   
    public string[] isReward;           //해당 인덱스 별 보상받았는지 
    public string Id;
    public int Index;
    public int Count;
    public ReactiveProperty<int> CountRP;            //새로 클리어했는지 여부
    public string Uid;

    public MyAchieveInfo(string[] isReward, string id, int index, int count,string uid)
    {
        this.isReward = isReward;
        Id = id;
        Index = index;
        Count = count;
        Uid = uid;
       // CountRP.Value =int.Parse( count);
    }
}