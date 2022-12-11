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
    Upgrade
}

public enum ErrorMessage
{
    ERROR,
    NickNameERROR,
    OK
}