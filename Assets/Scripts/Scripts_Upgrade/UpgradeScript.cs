using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    public GameObject Scroll;

    public GameObject NuniUpgradePrefab;            //��ȭ�� ���ϵ� ������
    GameObject DogamCha;

    public GameObject Gauge;            //������ ������
    public GameObject GaugePannel;      //������ �ǳ�

    public Image NuniImage;
    public  Text NuniText;
    public Text NuniLevel;
    public Text Level1;
    public Text Level2;
    public Text Level3;

    List<Card> Star3Nuni;

    public bool isSelect;

    public Card SelectedNuni;           //���� ���õ� ����
    // Start is called before the first frame update
    void Start()
    { Star3Nuni = new List<Card>();
        isSelect = false;
        for (int j = 0; j < GameManager.CharacterList.Count; j++)
        {
            if (int.Parse(GameManager.CharacterList[j].Star) == 3)
            { //3���̰� ���� ���� �����ΰ�



                if (GameManager.CharacterList[j].isLock == "F")
                {
                    Star3Nuni.Add(GameManager.CharacterList[j]);
                 
                }
            }
        }
        for (int i = 0; i < 10; i++)
        {
            DogamCha = Instantiate(NuniUpgradePrefab, Scroll.transform) as GameObject;


            DogamCha.transform.SetParent(Scroll.transform);
            if (Star3Nuni.Count>i)
            {
                Card nuni = DogamCha.AddComponent<Card>() as Card;
                nuni.SetValue(Star3Nuni[i]);
                DogamCha.GetComponent<Image>().sprite = Star3Nuni[i].GetChaImange();

            }

        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}