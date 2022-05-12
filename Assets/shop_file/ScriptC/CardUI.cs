using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour
{
    public Image chr; // 이미지생성
    public Text cardName; // 카드이름도 텍스트로

    // 카드의 정보를 초기화
    public Card CardUISet(Card card)  //변경
    {       
        Debug.Log(GameManager.AllNuniArray.Length);
        for (int i = 0; i <GameManager.AllNuniArray.Length; i++)
        {
            if (GameManager.AllNuniArray[i].cardName==card.cardName)
            {
                card = GameManager.AllNuniArray[i];
                chr.sprite = card.GetChaImange();
                cardName.text= card.cardName;
                
            }
        }
        Card result = new Card(card);
        return result;
    }
}
