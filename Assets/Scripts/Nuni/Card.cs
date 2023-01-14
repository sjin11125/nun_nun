using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Card : MonoBehaviour
{
    public string cardName;
    public string cardImage;
    public int Cost;
    public int Item;        //무슨 아이템인지(0~4)
    public string isLock;
    public string Level;        //레벨
    public string Star;     //별
    public string Gauge;        //게이지
    public string Info;     //누니설명
    public string Effect;   //보유효과
    public string Id;           //고유 Id

    public string[] Building;     //보유시 영향을 주는 건물
    public string Gold;   //보유효과
    public string weight;       //가중치

    public Sprite Image;

    public bool isDialog;               //대사 말하고 있나
    public Card(Cardsave cardSave)
    {
        cardImage = cardSave.cardImage;
        isLock = cardSave.isLock;
        Id = cardSave.Id;
    }
    public Card(Card c)
    {
        isLock = c.isLock;
        cardName = c.cardName;
        cardImage = c.cardImage;
        Cost = c.Cost;
        Item = c.Item;
        Level = c.Level;
        Star = c.Star;
        Gauge = c.Gauge;
        Info = c.Info;
        Effect = c.Effect;
        Building = c.Building;
        Gold = c.Gold;
        Image = c.Image;
        weight = c.weight;

    }
    public void SetValue(Card c)
    {
        isLock = c.isLock;
        cardName = c.cardName;
        cardImage = c.cardImage;
        Cost = c.Cost;
        Item = c.Item;
        Level = c.Level;
        Star = c.Star;
        Gauge = c.Gauge;
        Info = c.Info;
        Effect = c.Effect;
        Building = c.Building;
        Gold = c.Gold;
        Image = c.Image;
        weight = c.weight;
    }
    //public int weight;
    //잠금    /   이름  /  아이템 /   이미지 /  가격  /  레벨  /  별   /  게이지 /  설명  / 보유효과  / 건물  / 골드 획득량/가중치

    public Card(string islock, string cardname, string item, string cardimage, string cost, string level,
       string star, string gauge, string info, string effect, string building, string gold,string weight)
    {
        isLock = islock;
        this.cardName = cardname;
        this.cardImage = cardimage;
        Cost = int.Parse(cost);
        Item = int.Parse(item);
        Level = level;
        Star = star;
        Gauge = gauge;
        Info = info;
        Effect = effect;

        Building = building.Split( ' ' );


        Gold = gold;
        this.weight = weight;
    }

    public void SetChaImage(Sprite image)
    {
        Image = image;
    }
    public Sprite GetChaImange()
    {
        return Image;
    }
}
