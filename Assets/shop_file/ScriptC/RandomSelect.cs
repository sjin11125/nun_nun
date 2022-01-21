using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck;  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    void Start()
    {
        deck = new List<Card>();
        Debug.Log(GameManager.BuildingList.Count);
        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            // deck[i] = GameManager.AllNuniArray[i];
            Card c=new Card(GameManager.AllNuniArray[i]);
            c.SetValue(GameManager.AllNuniArray[i]);
            deck.Add(c);
        }
    }
    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform parent;
    public GameObject cardprefab;
    public AudioSource buttonsound;

    public UIManager2 UiManager2;

    public void click()
    {
        ResultSelect();
        buttonsound.Play();     //효과음 재생
        
        GameManager.Money -= 500;       //500원 빼기

        UiManager2.Click();
    }

    public void ResultSelect()
    {
        // 가중치 랜덤을 돌리면서 결과 리스트에 넣어줍니다.
        result.Add(RandomCard());
        // 비어 있는 카드를 생성하고
        CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
        // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
        Card Nuni = cardUI.CardUISet(RandomCard());

        for (int j = 0; j <= GameManager.CharacterList.Count; j++)
        {
            if (j == GameManager.CharacterList.Count)        //리스트 다 돌았는데 없으면
            {
                Debug.Log("Nuni level: " + Nuni.Level);
                Nuni.isLock = "F";          //누니 잠금 품
                GameManager.CharacterList.Add(Nuni);     //나온 결과를 리스트에 반영
                                                         //전체 누니 배열을 수정
                for (int i  = 0; i < GameManager.AllNuniArray.Length; i++)
                {
                    if (GameManager.AllNuniArray[i].cardImage== Nuni.cardImage)
                    {
                        GameManager.AllNuniArray[i].isLock = "F";
                    }
                }
                return;
            }
            if (Nuni.cardImage == GameManager.CharacterList[j].cardImage)        //현재 가지고 있는 누니 중 있으면
            {

                GameManager.Gem += int.Parse(Nuni.Level);  //근데 현재 가지고 있는 누니가 1성이면 1젬, 2성이면 2젬
                Debug.Log("Gem: " + GameManager.Gem);

            }

        }


        for (int i = 0; i < GameManager.CharacterList.Count; i++)
        {
            Debug.Log(GameManager.CharacterList[i].cardName);
        }

    }
    // 가중치 랜덤의 설명은 영상을 참고.
    public Card RandomCard()
    {
        // 이렇게하면 가중치 랜덤함수 (확률이 다름)

        return GameManager.AllNuniArray[Random.Range(0, GameManager.AllNuniArray.Length)];
    }

}

//동일확률로
//1. 눈(지우개효과) 2. 폭탄눈(자기기준 세로로 3개 터짐) 3. 폭탄눈2(자기기준 가로로3개 터짐)
// 4. 장로눈( 타이머 줄어드는 속도감소), 5. 무지개눈 ( 아무대나 놓아도 인정) 
// 이 친구들은 강화를 할때마다 사용할 수 있는 횟수가 증가함 ex) 1레벨 : 1번 > 이친구를 10번 뽑으면 레벨업 > 2레벨 : 2번 사용가능

