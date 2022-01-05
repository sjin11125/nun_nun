using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    void Start()
    {
        /*for (int i = 0; i < deck.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
        */
        // 실행


        //ResultSelect();

        //깊은 복사 잘 됐는지 확인용

    }
    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform parent;
    public GameObject cardprefab;
    public AudioSource buttonsound;

    public void click()
    {
        ResultSelect();
        buttonsound.Play();     //효과음 재생

        for (int i = 0; i < result.Count; i++)
        {
            Debug.Log(result[i].cardName);
           // Debug.Log(result[i].cardImage.name);
        }
        GameManager.Money -= 500;       //500원 빼기
    }

    public void ResultSelect()
    {
        for (int i = 0; i < 1; i++)
        {
            // 가중치 랜덤을 돌리면서 결과 리스트에 넣어줍니다.
            result.Add(RandomCard()); 
            // 비어 있는 카드를 생성하고
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
            GameManager.CharacterList.Add(cardUI.CardUISet(result[i]));     //나온 결과를 리스트에 반영


        }

        for (int i = 0; i < GameManager.CharacterList.Count; i++)
        {
            Debug.Log(GameManager.CharacterList[i].cardName);
        }

    }
    // 가중치 랜덤의 설명은 영상을 참고.
    public Card RandomCard()
    {
        /*int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return null;
    }
     */
        // 이렇게하면 가중치 랜덤함수 (확률이 다름)

        return deck[Random.Range(0, deck.Count)];
    }

}

//동일확률로
//1. 눈(지우개효과) 2. 폭탄눈(자기기준 세로로 3개 터짐) 3. 폭탄눈2(자기기준 가로로3개 터짐)
// 4. 장로눈( 타이머 줄어드는 속도감소), 5. 무지개눈 ( 아무대나 놓아도 인정) 
// 이 친구들은 강화를 할때마다 사용할 수 있는 횟수가 증가함 ex) 1레벨 : 1번 > 이친구를 10번 뽑으면 레벨업 > 2레벨 : 2번 사용가능

