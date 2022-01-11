using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card 
{
    public string cardName;
    public string cardImage;
    public int Cost;
    public int Item;        //무슨 아이템인지(0~4)
    public string isLock;
    public string Level;
    //별
    //게이지
    //설명
    //보유효과

    public Sprite Image;

    public Card(Card c)
    {
        isLock = c.isLock;
        cardName = c.cardName;
        cardImage = c.cardImage;
        Cost = c.Cost;
        Item = c.Item;
        Level = c.Level;
    }

    //public int weight;

    public Card(string islock,string cardname, string item, string cardimage, string cost,string level)
    {
        isLock = islock;
        this.cardName = cardname;
        this.cardImage = cardimage;
        Cost =int.Parse(cost);
        Item = int.Parse(item);
        Level = level;

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
