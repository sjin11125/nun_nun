using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuniParseManager : MonoBehaviour
{   //GameManager에서 파싱한 누니 정보들을 받아 누니 도감 패널에 넣기

    public GameObject NuniPannelPrefab;           //누니 패널 프리팹
    public GameObject Scroll;

    public  GameObject NuniInfoPanel;

    public static Text[] NuniTexts;
    public static Image[] NuniImages;

    public static bool Info;


    public static Card SelectedNuni;
    // Start is called before the first frame update
    void Start()
    {
        NuniTexts=NuniInfoPanel.GetComponentsInChildren<Text>();
        NuniImages=NuniInfoPanel.GetComponentsInChildren<Image>();
        SelectedNuni=gameObject.AddComponent<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Info)
        {
            Info = false;
            NuniInfoPanel.SetActive(true);
        }
    }

    public void NuniDogamOpen()             //누니 도감 오픈했을 때
    {

        GameManager.isMoveLock = true;
        //GM에 있는 모든 누니 정보 불러서 패널에 넣기
        Transform[] child=Scroll.GetComponentsInChildren<Transform>();
        for (int j = 1; j < child.Length; j++)
        {
            Destroy(child[j].gameObject);
        }
        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            GameObject NuniPannel = Instantiate(NuniPannelPrefab) as GameObject;
            NuniPannel.transform.SetParent(Scroll.transform);
            

            Button NuniButton = NuniPannel.GetComponentInChildren<Button>();
            Image[] image = NuniPannel.GetComponentsInChildren<Image>();
            Text NuniName = NuniPannel.GetComponentInChildren<Text>();

            Card nuni = GameManager.AllNuniArray[i];
            NuniButton.enabled = true;
            Debug.Log(image.Length);
            image[1].sprite = nuni.GetChaImange();   //누니 이미지 넣기
            NuniPannel.name = nuni.cardImage;        //누니 이름 넣기
            NuniName.text = nuni.cardName;
            NuniPannel.GetComponent<RectTransform>().localScale = new Vector3(2.8f, 2.8f, 0);

           


        }
    }

    public static void NuniInfoOpen()
    {
        NuniImages[3].sprite = SelectedNuni.GetChaImange();
        NuniTexts[0].text = SelectedNuni.cardName;
        NuniTexts[2].text = SelectedNuni.Info;
        NuniTexts[4].text = SelectedNuni.Effect;

        Info = true;

    }
}
