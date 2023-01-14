using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    public GameObject Scroll;

    public GameObject NuniUpgradePrefab;            //강화할 누니들 프리팹
    GameObject DogamCha;

    public GameObject Gauge;            //게이지 프리팹
    public GameObject GaugePannel;      //게이지 판넥

    public Image NuniImage;
    public  Text NuniText;
    public Text NuniLevel;
    public Text Level1;
    public Text Level2;
    public Text Level3;

    List<Card> Star3Nuni;

    public bool isSelect;

    public Card SelectedNuni;           //현재 선택된 누니
    // Start is called before the first frame update
    void Start()
    { Star3Nuni = new List<Card>();
        isSelect = false;
        foreach (var item in GameManager.Instance.CharacterList)
        {
            if (int.Parse(item.Value.Star) == 3)
            { //3성이고 현재 얻은 누니인가



                if (item.Value.isLock == "F")
                {
                    Star3Nuni.Add(item.Value);

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
