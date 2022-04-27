using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory_prefab;     //인벤토리 버튼 프리팹
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
}
