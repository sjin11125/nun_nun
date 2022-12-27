using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory_prefab;     //인벤토리 활성화된 버튼 프리팹
    public GameObject inventory_nuni_prefab;     //인벤토리 버튼 프리팹
    public Transform Content;

    public Button InvenBuildingBtn;
    public Button InvenNuniBtn;

    public GridBuildingSystem gridBuildingSystem;
    GameObject ActiveBuildingPrefab;
    InventoryButton ActiveButton;
    void Start()
    {
        if (LoadManager.Instance == null)
            return;

     
            InvenBuildingBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Inventory_Building_Open();
            }).AddTo(this);
        
       
            InvenNuniBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Inventory_Nuni_Open();
            }).AddTo(this);
        
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

                Debug.Log(item.Value.Id);
                GameObject inven = Instantiate(inventory_prefab, Content) as GameObject;         //인벤 버튼 프리팹 생성


                InventoryButton inventoryBtn = inven.GetComponent<InventoryButton>();

                inventoryBtn.SetButtonImage(GameManager.GetDogamChaImage(item.Value.Building_Image));   //버튼 이미지 설정

                Building building = item.Value;
                inventoryBtn.SetBuildingInfo(building);                           //해당 건물 정보 등록
                inventoryBtn.temp_building=item.Value;

                Button Button = inven.GetComponent<Button>();
                


                Button.OnClickAsObservable().Subscribe(_ =>                     //인벤토리 건물 클릭 구독
                {
                    inventoryBtn.SetBuildingInfo(LoadManager.Instance.MyBuildings[inventoryBtn.temp_building.Id]);
                    if (inventoryBtn.temp_building.isLock=="T")         //해당 건물이 설치되었으면
                    {
                        
                        inventoryBtn.temp_building.Type = BuildType.Load;
                        GridBuildingSystem.OnEditMode.OnNext(inventoryBtn.temp_building);  //건설모드 ON (타일 초기화)
                        LoadManager.Instance.RemoveBuilding(inventoryBtn.temp_building.Id); //해당 프리팹 삭제

                        inventoryBtn.temp_building.isLock = "F";                            //배치안된 상태로 바꾸기

                        inventoryBtn.SetNoImage(false);

                        inventoryBtn.temp_building.BuildingPosition.x = 0;                            //위치 초기화
                        inventoryBtn.temp_building.BuildingPosition.y= 0;
                        inventoryBtn.temp_building.Placed = false;
                        LoadManager.Instance.buildingsave.BuildingReq(BuildingDef.updateValue, inventoryBtn.temp_building);     //서버로 전송
                    }
                    else                               //해당 건물이 설치안되어있으면
                    {
                        Building ActiveBuilding=new Building();

                        if (ActiveButton!=null)            //이전에 배치안한 건물이 있었다면
                        {
                            ActiveButton.SetBuildingInfo(LoadManager.Instance.MyBuildings[ActiveButton.temp_building.Id]);
                            ActiveButton.temp_building.area = LoadManager.Instance.MyBuildings[ActiveButton.temp_building.Id].area;
                            //  Debug.LogError(ActiveButton.temp_building);
                            if (ActiveButton.temp_building.isLock == "F")
                            {


                                Destroy(ActiveBuildingPrefab);
                                ActiveButton.temp_building.Type = BuildType.Load;
                                ActiveButton.SetNoImage(false);                  //X표시 생기게
                                GridBuildingSystem.OnEditMode.OnNext(ActiveButton.temp_building);  //건설모드 ON (타일 초기화)
                                if (LoadManager.Instance.MyBuildingsPrefab.ContainsKey(ActiveButton.temp_building.Id))
                                LoadManager.Instance.RemoveBuilding(ActiveButton.temp_building.Id); //해당 프리팹 삭제
                            }
                        }
                        try
                        {
                            ActiveBuilding = LoadManager.Instance.InstatiateBuilding(inventoryBtn.temp_building);
                            
                            ActiveBuildingPrefab = ActiveBuilding.gameObject;

                            ActiveButton = inventoryBtn;
                            ActiveButton.temp_building.area = LoadManager.Instance.MyBuildings[ActiveButton.temp_building.Id].area;

                            Debug.Log(ActiveBuildingPrefab);
                            ActiveBuilding.Type = BuildType.Move;

                            GridBuildingSystem.OnEditMode.OnNext(ActiveBuilding);  //건설모드 ON
                            inventoryBtn.SetNoImage(true);

                           /* ActiveButton.temp_building.BuildEditBtn[1].btn.OnClickAsObservable().Subscribe(_=>
                            {
                                ActiveButton.temp_building.isLock = "T";
                            }).AddTo(this);*/
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError(e.Message);
                            throw;
                        }
                       
                    }



                }).AddTo(this);             

                
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
        Inventory_Exit();

        for (int i = 0; i < GameManager.CharacterList.Count; i++)
        {

            GameObject inven = Instantiate(inventory_nuni_prefab, Content) as GameObject;         //인벤 버튼 프리팹 생성

            //inven.name = GameManager.CharacterList[i].cardImage;
            inven.tag = "Inven_Nuni";            //인벤 버튼 태그 설정

            Image ButtonImage = inven.GetComponent<Image>();


          
            ButtonImage.sprite = GameManager.GetCharacterImage(GameManager.CharacterList[i].cardImage);

            inven.GetComponent<InventoryButton>().this_nuni = GameManager.CharacterList[i];
       


        }
    }
}
