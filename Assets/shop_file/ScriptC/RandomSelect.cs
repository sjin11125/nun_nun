using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class RandomSelect : MonoBehaviour
{
    public List<Card> deck;  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    public static int isTuto;
    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform parent;
    public GameObject cardprefab;

    void Start()
    {
        deck = new List<Card>();
        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            total += int.Parse(GameManager.AllNuniArray[i].weight);
        }

        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            // deck[i] = GameManager.AllNuniArray[i];
            Card c = new Card(GameManager.AllNuniArray[i]);
            c.SetValue(GameManager.AllNuniArray[i]);
            deck.Add(c);
        }
        ShopBuyScript.isfirst = true;
    }

    public void ResultSelect()
    {
        //셀력결과
        // 가중치 랜덤을 돌리면서 결과 리스트에 넣어줍니다.
        result.Add(RandomCard());
        // 비어 있는 카드를 생성하고
        CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
        // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
        Card Nuni = cardUI.CardUISet(RandomCard());
        Nuni.isLock = "T";          //누니 잠금 품
        Nuni.Id = GameManager.Instance.IDGenerator();
        GameManager.Instance.CharacterList.Add(Nuni.Id, Nuni);     //나온 결과를 리스트에 반영
                                                          //전체 누니 배열을 수정
        Cardsave cardsave = new Cardsave(GameManager.Instance.PlayerUserInfo.Uid, Nuni.cardImage, Nuni.isLock, Nuni.Id);

        cardsave.Uid = GameManager.Instance.PlayerUserInfo.Uid;

        FirebaseLogin.Instance.SetNuni(cardsave);//파이어베이스에 업데이트

        ShopBuyScript.isfirst = false;
    }
   /* IEnumerator NuniSave(Card nuni)                //누니 구글 스크립트에 저장
    {
        
        WWWForm form1 = new WWWForm();
        form1.AddField("order", "nuniSave");
        form1.AddField("player_nickname", GameManager.NickName);
        form1.AddField("nuni", nuni.cardName+":T") ;



        yield return StartCoroutine(Post(form1));                        //구글 스크립트로 초기화했는지 물어볼때까지 대기


    }
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            if (www.isDone) NuniResponse(www.downloadHandler.text);
            //else print("웹의 응답이 없습니다.");
        }

    }*/
    void NuniResponse(string json)                          //누니 불러오기
    {
        //List<QuestInfo> Questlist = new List<QuestInfo>();

        if (json .Equals( "null"))
        {
            return;
        }
        if (string.IsNullOrEmpty(json))
        {
            
            return;
        }
                       //누니 이름 받아서 겜메 모든 누니 배열에서 누니 정보 받아서 넣기

      
    }

    // 가중치 랜덤의 설명은 영상을 참고.
    public Card RandomCard()
    {
        // 이렇게하면 가중치 랜덤함수 (확률이 다름)

        if (isTuto .Equals( 0))
        {
            return GameManager.AllNuniArray[6];
        }
        else
        {
            int weight = 0;
            int selectNum = 0;
            selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

            for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
            {
                weight += int.Parse(GameManager.AllNuniArray[i].weight);
                if (selectNum <= weight)
                {
                    Card temp = new Card(GameManager.AllNuniArray[i]);
                    return temp;
                }

            }
            return null;
            // 이렇게하면 가중치 랜덤함수 (확률이 다름)
            //원래는 return deck[Random.Range(0,deck.Count)];
        }
    }

}

//동일확률로
//1. 눈(지우개효과) 2. 폭탄눈(자기기준 세로로 3개 터짐) 3. 폭탄눈2(자기기준 가로로3개 터짐)
// 4. 장로눈( 타이머 줄어드는 속도감소), 5. 무지개눈 ( 아무대나 놓아도 인정) 
// 이 친구들은 강화를 할때마다 사용할 수 있는 횟수가 증가함 ex) 1레벨 : 1번 > 이친구를 10번 뽑으면 레벨업 > 2레벨 : 2번 사용가능

