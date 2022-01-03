using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card2
{
    public string cardName;
    public Sprite cardImage;
    //public int weight;

    public Card2(Card2 card)
    {
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        //this.weight = card.weight;
    }
}
