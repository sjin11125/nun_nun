using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum LoginState
{
    GOOGLE_ACCOUNT,
    APPLE_ACCOUNT,
    EMAIL_ACCOUNT
}
public enum BuildingDef
{
    updateValue,
    addValue,
    removeValue,
    removeValueStr,
    getFriendBuilding,
    getMyBuilding

}
public enum AccountDef
{
    register,
    login,
    logout,
    setmoney,
    getmoney,
    getNotice,
    getChallenge,
    isUpdate
}
public enum FriendDef
{
    GetFriend,
    RequestFriend,
    SearchFriend,
    RecommendFriend
}
public enum FirebaseDef
{
    Register,
    Login,
    Logout,
    Setmoney,
    Getmoney,
    GetNotice,
    GetChallenge,
    IsUpdate


}
public enum BuildType
{
    Empty,
    Load,
    Move,
    Rotation,
    Make,
    Upgrade,
    Remove
}
public enum BuildingName
{
    House,
    Fashion,
    School,
    Flower,
    Syrup,
    Village,
    NuniTree
}
public enum StrName
{
    Fence,
    Flower1,
    Flower2,
    StreetLamp1,
    StreetLamp2,
    Tree1
}
public enum BuildUIType
{
  
    Rotation,
    Make,
    Upgrade,
    Remove,
    VisitorBook
}

public enum LoginResult
{
    ERROR,
    NickNameERROR,
    OK,
    SignUpOK,
    LoginOK
}
public enum TileType
{
    Empty,
    White,
    Green,
    Red
}
public enum SceneName
{
    Login,
    Main,
    Shop,
    FriendMain,
    Game
}
public enum UIBtn
{
    ProfilBtn,
    SettingBtn,
    MessageBtn,
    FriendBtn,
    StoreBtn,
    InventoryBtn,
    RankingBtn,
    AchievementBtn,
    StartBtn,
}

public enum AchieveMenuType
{
    Color,
    Ect,
    Shape
}
public enum AchieveCountType
{
    Int,
    Float
}
public enum AchieveRewardType
{
    Nuni,
    Money,
    ShinMoney,
    Zem,
}
public enum AchieveID
{
    C1,C2,C3,C4,C5,C6,
    E1,E2,E3,E4,E5,E6,
    S1,S2,S3,S4,S5,S6,

}