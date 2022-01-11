using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Image chr; // 이미지생성
    public Text cardName; // 카드이름도 텍스트로
    Animator animator; //애니메이터도 생성
    public GameObject back;

    private void Start()
    {
        animator = GetComponent<Animator>(); //에니메이터 요소 가져오기
    }
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
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        // animator.SetTrigger("Flip");
        Destroy(back);
    }
}
