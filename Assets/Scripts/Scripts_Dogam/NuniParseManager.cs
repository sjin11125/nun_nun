using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuniParseManager : MonoBehaviour
{   //GameManager에서 파싱한 누니 정보들을 받아 누니 도감 패널에 넣기

    public GameObject NuniPannelPrefab;           //누니 패널 프리팹
    public GameObject Scroll;

    public GameObject NuniInfoPanel;
    public static Image[] NuniInfoImages;
    public static Text[] NuniInfoTexts;
    public static bool isNuniButtonClick;

    public static Card SelectedNuni;                //클릭된 누니

    public Text ZemText;
    // Start is called before the first frame update
    void Start()
    {
        NuniInfoImages=NuniInfoPanel.GetComponentsInChildren<Image>();
        NuniInfoTexts=NuniInfoPanel.GetComponentsInChildren<Text>();

        SelectedNuni = this.gameObject.AddComponent<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNuniButtonClick)              //누니버튼 클릭했나
        {
            isNuniButtonClick = false;
            NuniInfoPanel.SetActive(true);
        }
    }

    public void NuniDogamOpen()             //누니 도감 오픈했을 때
    {
        ZemText.text = "보유 잼: " + GameManager.Zem;
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
            image[1].sprite = nuni.GetChaImange();   //누니 이미지 넣기
            NuniPannel.name = nuni.cardImage;        //누니 이름 넣기
            NuniName.text = nuni.cardName;
            NuniPannel.GetComponent<RectTransform>().localScale = new Vector3(2.8f, 2.8f, 0);

            NuniPannel.GetComponent<Card>().SetValue(nuni);


            //OpenNuniInfoPanel();                //누니 정보 넣기
        }
    }

    public static void OpenNuniInfoPanel()         //누니 정보 패널에 누니 정보 넣기
    {
        
           NuniInfoImages[2].sprite = SelectedNuni.GetChaImange();             //누니 이미지 넣기
        NuniInfoTexts[0].text = SelectedNuni.cardName;              //누니 이름 넣기
        NuniInfoTexts[2].text = SelectedNuni.Info;          //누니 설명 넣기
        NuniInfoTexts[4].text = SelectedNuni.Effect;          //누니 보유효과 넣기

        isNuniButtonClick = true;



    }
}
