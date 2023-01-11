using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

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