using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory_prefab;     //인벤토리 버튼 프리팹
    public GameObject inventory_nuni_prefab;     //인벤토리 버튼 프리팹
    public Transform Content;

    public GridBuildingSystem gridBuildingSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inventory_Building_Open()            //건물 인벤 버튼 눌렀을 때
    {
        Transform[] Content_Child= Content.GetComponentsInChildren<Transform>();
        for (int i = 1; i < Content_Child.Length; i++)
        {
            Destroy(Content_Child[i].gameObject);
        }

        for (int i = 0; i < GameManager.BuildingList.Count; i++)
        {
            
            GameObject inven=Instantiate(inventory_prefab,Content) as GameObject;         //인벤 버튼 프리팹 생성

            inven.gameObject.name = GameManager.BuildingList[i].Building_name;
            inven.gameObject.tag = "Inven_Building";            //인벤 버튼 태그 설정

            

            Image ButtonImage = inven.GetComponent<Image>();
            Debug.Log("building image: "+GameManager.BuildingList[i].Building_Image);
         

            Image PrefabImage;// = GameManager.GetDogamChaImage(GameManager.BuildingList[i].Building_Image);
            ButtonImage.sprite = GameManager.GetDogamChaImage(GameManager.BuildingList[i].Building_Image);

            if (GameManager.BuildingList[i].isLock == "T")
            {
                Button Button = inven.GetComponent<Button>();
                //Button.enabled= false;              //이미 설치되어 있으면 버튼 클릭 못하고 X 뜸

            }

        }
    }
    public void Inventory_Nuni_Open()            //누니 인벤 버튼 눌렀을 때
    {
        Transform[] Content_Child = Content.GetComponentsInChildren<Transform>();       //버튼 다 삭제
        for (int i = 1; i < Content_Child.Length; i++)
        {
            Destroy(Content_Child[i].gameObject);
        }

        for (int i = 0; i < GameManager.CharacterList.Count; i++)
        {

            GameObject inven = Instantiate(inventory_nuni_prefab, Content) as GameObject;         //인벤 버튼 프리팹 생성

            inven.gameObject.name = GameManager.CharacterList[i].cardImage;
            inven.gameObject.tag = "Inven_Nuni";            //인벤 버튼 태그 설정

            Image ButtonImage = inven.GetComponent<Image>();
            Debug.Log("building image: " + GameManager.CharacterList[i].cardImage);


            Image PrefabImage;// = GameManager.GetDogamChaImage(GameManager.BuildingList[i].Building_Image);
            ButtonImage.sprite = GameManager.GetCharacterImage(GameManager.CharacterList[i].cardImage);

            if (GameManager.CharacterList[i].isLock == "T")
            {
                Button Button = inven.GetComponent<Button>();
                //Button.enabled= false;              //이미 설치되어 있으면 버튼 클릭 못하고 X 뜸

            }

        }
    }
}
