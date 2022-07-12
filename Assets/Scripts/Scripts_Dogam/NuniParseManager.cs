using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuniParseManager : MonoBehaviour
{   //GameManager���� �Ľ��� ���� �������� �޾� ���� ���� �гο� �ֱ�

    public GameObject NuniPannelPrefab;           //���� �г� ������
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

    public void NuniDogamOpen()             //���� ���� �������� ��
    {

        GameManager.isMoveLock = true;
        //GM�� �ִ� ��� ���� ���� �ҷ��� �гο� �ֱ�
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
            image[1].sprite = nuni.GetChaImange();   //���� �̹��� �ֱ�
            NuniPannel.name = nuni.cardImage;        //���� �̸� �ֱ�
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

   
        switch (SelectedNuni.Star)
        {
            case "1":
                NuniImages[5].gameObject.SetActive(true);
                NuniImages[6].gameObject.SetActive(false);
                NuniImages[7].gameObject.SetActive(false);
                break;
            case "2":
                NuniImages[5].gameObject.SetActive(true);
                NuniImages[6].gameObject.SetActive(true);
                NuniImages[7].gameObject.SetActive(false);
                break;
            case "3":
                NuniImages[5].gameObject.SetActive(true);
                NuniImages[6].gameObject.SetActive(true);
                NuniImages[7].gameObject.SetActive(true);
                break;
        }
        Info = true;

    }
}
