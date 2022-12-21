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
    Remove
}

public enum LoginResult
{
    ERROR,
    NickNameERROR,
    OK,
    SignUpOK,
    LoginOK
}