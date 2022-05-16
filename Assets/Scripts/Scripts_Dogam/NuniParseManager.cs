using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuniParseManager : MonoBehaviour
{   //GameManager에서 파싱한 누니 정보들을 받아 누니 도감 패널에 넣기

    public GameObject NuniPannelPrefab;           //누니 패널 프리팹
    public GameObject Scroll;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NuniDogamOpen()             //누니 도감 오픈했을 때
    {
        GameManager.isMoveLock = true;
        //GM에 있는 모든 누니 정보 불러서 패널에 넣기
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

            /* if (GameManager.AllNuniArray[i].isLock=="F")       // 누니를 현재 가지고 있을 때
             {
                 Card nuni = GameManager.AllNuniArray[i];
                 NuniButton.enabled = true;
                 image[2].sprite = nuni.GetChaImange();   //누니 이미지 넣기
                 NuniPannel.name = nuni.cardImage;        //누니 이름 넣기

             }
             else
             {
                 NuniButton.enabled = false;
                 NuniPannel.name = "?";
             }*/


        }
    }
}
