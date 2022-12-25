using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory_prefab;     //인벤토리 버튼 프리팹
    public GameObject inventory_nuni_prefab;     //인벤토리 버튼 프리팹
    public Transform Content;

    public Button InvenBuildingBtn;
    public Button InvenNuniBtn;

    public GridBuildingSystem gridBuildingSystem;
    void Start()
    {
        if (LoadManager.Instance == null)
            return;
       
        InvenBuildingBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Inventory_Exit();
        }).AddTo(this);
        InvenNuniBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Inventory_Exit();
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isUpdate.Equals(true))
        {
            GameManager.isUpdate = false;
            Inventory_Building_Open();
        }
    }
    public void Inventory_Exit()
    {
        Transform[] Content_Child = Content.GetComponentsInChildren<Transform>();
        for (int i = 1; i < Content_Child.Length; i++)
        {
            Destroy(Content_Child[i].gameObject);
        }
    }
    public void Inventory_Building_Open()            //건물 인벤 버튼 눌렀을 때
    {
        Inventory_Exit();           //원래 있던 목록 다 지우기
        foreach (var item in LoadManager.Instance.MyBuildings)
        {

        
            bool isStr = false;
            for (int j = 0; j < GameManager.StrArray.Length; j++)
            {
                if (item.Value.Building_Image .Equals( GameManager.StrArray[j].Building_Image) )      //설치물인가
                {
                    isStr = true;
                }
            }
            if (item.Value.Id != "ii1y1"&&isStr.Equals(false) )         //분수가 아니고 설치물이 아니라면
            {
                
              
                GameObject inven = Instantiate(inventory_prefab, Content) as GameObject;         //인벤 버튼 프리팹 생성

                inven.gameObject.name = item.Value.Id;
                inven.gameObject.tag = "Inven_Building";            //인벤 버튼 태그 설정



                Image ButtonImage = inven.GetComponent<Image>();


                Image PrefabImage;// = GameManager.GetDogamChaImage(GameManager.BuildingList[i].Building_Image);
                ButtonImage.sprite = GameManager.GetDogamChaImage(item.Value.Building_Image);

                if (item.Value.isLock .Equals( "T"))
                {
                    Button Button = inven.GetComponent<Button>();
                    //Button.enabled= false;              //이미 설치되어 있으면 버튼 클릭 못하고 X 뜸

                }
            }
        }
    }
    public void Inventory_Structure_Open()            //설치물 인벤 버튼 눌렀을 때
    {
        Inventory_Exit();           //원래 있던 목록 다 지우기
        foreach (var item in LoadManager.Instance.MyBuildings)
        {

            bool isStr = false;
            for (int j = 0; j < GameManager.StrArray.Length; j++)
            {
                if (item.Value.Building_Image .Equals( GameManager.StrArray[j].Building_Image))       //설치물인가
                {
                    isStr = true;
                }
            }
            if (item.Value.Id != "ii1y1"&&isStr.Equals(true))          //분수가 아니고 설치물이라면
            {

                GameObject inven = Instantiate(inventory_prefab, Content) as GameObject;         //인벤 버튼 프리팹 생성
     
                inven.gameObject.name = item.Value.Id;
                inven.gameObject.tag = "Inven_Building";            //인벤 버튼 태그 설정



                Image ButtonImage = inven.GetComponent<Image>();


                ButtonImage.sprite = GameManager.GetDogamChaImage(item.Value.Building_Image);

                if (item.Value.isLock .Equals( "T"))
                {
                    Button Button = inven.GetComponent<Button>();
                    //Button.enabled= false;              //이미 설치되어 있으면 버튼 클릭 못하고 X 뜸

                }
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

            inven.name = GameManager.CharacterList[i].cardImage;
            inven.tag = "Inven_Nuni";            //인벤 버튼 태그 설정

            Image ButtonImage = inven.GetComponent<Image>();


          
            ButtonImage.sprite = GameManager.GetCharacterImage(GameManager.CharacterList[i].cardImage);

            inven.GetComponent<InventoryButton>().this_nuni = GameManager.CharacterList[i];
       


        }
    }
}
