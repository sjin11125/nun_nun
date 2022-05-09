using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
[Serializable]
public class GridBuildingSystem : MonoBehaviour
{
    //1101 추가
    // public GameObject OkButton;

    private Button button;
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

 
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private Building temp; //building type으로 temp 생성
    private Vector3 prevPos;
    private BoundsInt prevArea;

    public GameObject UpgradePannel;
    GameObject Grid;
    public Button StartButton;

    public UIAniManager UI_Manager;
    public GameObject buildings;
    //추가 1110

        //------------------------세이브 관련 변수들--------------------------------------
    public static bool isSave = false;          //건물 건설이나 삭제했을 때 건물들 저장하는 변수
    public BuildingSave BSave;

    float second = 0;
    bool isGrid = false;

    public GameObject VisitorBooksWindow;           //방명록창


    #region unity Methods  
    private void Awake()
    {
        if (current == null)
        {
            current = this;
            //isStart = true;
        }
        else if (current != this) // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        string tilePath = @"Tiles\";

        if (GameManager.isStart == true)        //tileBases에 아무것도 없으면
        {
            tileBases.Add(TileType.Empty, null);
            tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "white"));
            tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "green"));
            tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "red"));
            GameManager.isStart = false;

        }

        Grid = GameObject.Find("back_down");
       // if (SceneManager.GetActiveScene().name=="Main")
       // StartButton = GameObject.Find("Start").GetComponent<Button>();

    }
   
    public void Inven_Move(Transform hit)
    {
        if (hit.transform != null)          // 오브젝트를 클릭 했을 때
        {

            Transform Building = hit.transform.parent;
            if (temp != null)
            {
                if (temp.isLock == "F")               //건물이 배치가 안 된 상태인가?
                {
                    Building hit_building = temp.GetComponent<Building>();
                    if (hit.transform.tag == "Button")      //건물 배치 확인 버튼
                    {
                        if (temp.CanBePlaced())         //건물이 배치 될 수 있는가? 네
                        {
                            //temp.level += 1;        //레벨 +1
                            temp.Place(temp.Type);
                            //UI_Manager.Start();

                            Grid.GetComponent<SpriteRenderer>().sortingOrder = -48;
                            //StartButton.enabled = true;
                            temp = null;
                        }
                        // button.buttonok();
                    }
                    if (hit.transform.tag == "Rotation")        //건물 회전 버튼
                    {

                        if (hit_building.isFliped == "T")
                        {
                            hit_building.isFliped = "F";
                        }
                        else
                        {
                            hit_building.isFliped = "T";
                        }
                        hit_building.Rotation();


                    }
                    if (hit.transform.tag == "Upgrade")         //업그레이드
                    {
                        GameManager.isMoveLock = true;
                        hit_building.Type = BuildType.Upgrade;
                        hit_building.Upgrade();
                    }
                    if (hit.transform.tag == "Remove")          //제거
                    {
                        temp.Remove(temp);
                        //UI_Manager.Start();
                        Grid.GetComponent<SpriteRenderer>().sortingOrder = -48;

                    }
                }

            }
            else             //temp가 없을 때               //건물이 배치 된 상태
            {

                Debug.Log("temp is null");
                temp = hit.transform.GetComponent<Building>();

                if (hit.transform.tag == "Building" && GameManager.isStore == false)           //빌딩을 눌렀을 때 업그레이드 할래 위치 바꿀래 회전할래
                {
                    //UI_Manager.StartOpen();     //ui 중앙으로 이동
                    temp.Type = BuildType.Move;
                    temp.Placed = false;        //배치가 안 된 상태로 변환

                    temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
                    BoundsInt buildingArea = temp.area;

                    TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);
                    int size = baseArray.Length;
                    for (int i = 0; i < size; i++)
                    {
                        baseArray[i] = tileBases[TileType.White];
                        //FillTiles(baseArray, TileType.White);
                        Debug.Log("tiles");
                    }
                    TempTilemap.SetTilesBlock(buildingArea, baseArray);
                    SetTilesBlock(buildingArea, TileType.White, MainTilemap);

                    //FollowBuilding(true);
                    Grid.GetComponent<SpriteRenderer>().sortingOrder = -50;
                    Debug.Log("Level: " + temp.Level);
                }
                if (hit.transform.tag == "Coin_Button")           //재화 버튼 누르면(되긴 하는데 수정해야함)
                {
                    //Transform BuildingCoin = hit.transform.parent;
                    Building.GetComponent<Building>().Coin_OK();

                    Debug.Log("huan");
                }
            }
        }
    }
    private void Update()
    {
        if (ChaButtonScript.isEdit==true)
        {
            ChaButtonScript.isEdit = false;
            InitializeWithBuilding();
        }
        if (GameManager.isEdit==true)
        {
            GameManager.isEdit = false;
            InitializeWithBuilding();
            temp.Type = BuildType.Move;
        }
        if (isGrid == true)
        {
            second += Time.deltaTime;
            Debug.Log("second: " + second);
        }
        else
        {
            second = 0;
        }
        if (Input.GetMouseButton(0) && SceneManager.GetActiveScene().name == "Main")                    //그냥 클릭했을 때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            Debug.Log("isMoveLock: " + GameManager.isMoveLock);



            if (hit.transform != null)          // 오브젝트를 클릭 했을 때
            {

                Transform Building = hit.transform.parent;
                if (temp != null)
                {
                    if (temp.Placed == false)               //건물이 배치가 안 된 상태인가?
                    {
                        Building hit_building = temp.GetComponent<Building>();
                        if (hit.transform.tag == "Button")      //건물 배치 확인 버튼
                        {
                            if (temp.CanBePlaced())         //건물이 배치 될 수 있는가? 네
                            {
                                //temp.level += 1;        //레벨 +1
                                temp.Place(temp.Type);
                                UI_Manager.Start();

                                Grid.GetComponent<SpriteRenderer>().sortingOrder = -48;
                                StartButton.enabled = true;
                                temp = null;
                            }
                            // button.buttonok();
                        }
                        if (hit.transform.tag == "Rotation")        //건물 회전 버튼
                        {

                            if (hit_building.isFliped == "T")
                            {
                                hit_building.isFliped = "F";
                            }
                            else
                            {
                                hit_building.isFliped = "T";
                            }
                            hit_building.Rotation();


                        }
                        if (hit.transform.tag == "Upgrade")         //업그레이드
                        {
                            GameManager.isMoveLock = true;
                            hit_building.Type = BuildType.Upgrade;
                            hit_building.Upgrade();
                        }
                        if (hit.transform.tag == "Remove")          //제거
                        {
                            temp.Remove(temp);
                            //UI_Manager.Start();
                            Grid.GetComponent<SpriteRenderer>().sortingOrder = -48;

                        }
                    }

                }
                else             //temp가 없을 때               //건물이 배치 된 상태
                {


                    if (hit.transform.tag == "Coin_Button")           //재화 버튼 누르면(되긴 하는데 수정해야함)
                    {
                        //Transform BuildingCoin = hit.transform.parent;
                        Building.GetComponent<Building>().Coin_OK();

                        Debug.Log("huan");
                    }

                    else if (hit.transform.tag == "VisitorBook")
                    {
                        VisitorBooksWindow.gameObject.SetActive(true);
                    }
                }

            }
            else   // 빈 공간을 클릭했을 때
            {
                if (temp != null)
                {
                    if (!temp.Placed)           //건물이 놓여지지 않았다.(마우스가 클릭하는 데로 건물 따라감)
                    {
                        if (temp.Type != BuildType.Upgrade)
                        {


                            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
                            Debug.Log("mouse");
                            if (prevPos != cellPos)
                            {
                                temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                                    + new Vector3(.5f, .5f, 0f)); //Vector3
                                prevPos = cellPos;
                                FollowBuilding(false); // 마우스가 위의 좌표 따라감. 
                            }
                        }

                    }
                }
            }
        }
        

        if (second >= 2.0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            Debug.Log("isMoveLock: " + GameManager.isMoveLock);

            Debug.Log("temp is null");
            temp = hit.transform.GetComponent<Building>();

            if (hit.transform.tag == "Building" && GameManager.isStore == false)           //빌딩을 눌렀을 때 업그레이드 할래 위치 바꿀래 회전할래
            {
                GameManager.CurrentBuilding_Script = temp;
                //UI_Manager.StartOpen();     //ui 중앙으로 이동
                temp.Type = BuildType.Move;
                temp.Placed = false;        //배치가 안 된 상태로 변환

                temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
                BoundsInt buildingArea = temp.area;

                TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);
                int size = baseArray.Length;
                for (int i = 0; i < size; i++)
                {
                    baseArray[i] = tileBases[TileType.White];
                    //FillTiles(baseArray, TileType.White);
                    Debug.Log("tiles");
                }
                TempTilemap.SetTilesBlock(buildingArea, baseArray);
                SetTilesBlock(buildingArea, TileType.White, MainTilemap);

                //FollowBuilding(true);
                Grid.GetComponent<SpriteRenderer>().sortingOrder = -50;
                Debug.Log("Level: " + temp.Level);
            }
            // BuildEdit(hit);

        }
        if (Input.GetMouseButtonDown(0)&&SceneManager.GetActiveScene().name=="Main") //마우스를 누르고 있을 때             지금 내 닉넴과 마을
        {



            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            Debug.Log("isMoveLock: " + GameManager.isMoveLock);

            if (hit.collider != null)
            {

                isGrid = true;
                Debug.Log("second: " + second);
                
                //StartCoroutine(WaitSecond(hit));
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            second = 0;
            isGrid = false;
        }
        //다 필요없고 ok버튼을 눌렀을 때

 

    }
    

    #endregion

    #region Tilemap Management
    static void MoveTIles()
    {

    }
   private static void FillTiles(TileBase[] arr, TileType type)
   {
        Debug.Log("FillTiles()");
        for (int i = 0; i < arr.Length; i++)
       {
           arr[i] = tileBases[type];
       }
   }


   private static TileBase[] GetTilesBlock (BoundsInt area, Tilemap tilemap)
   {
        Debug.Log("GetTilesBlock");
       TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
       int counter = 0;

       foreach (var v in area.allPositionsWithin)
       {
           Vector3Int pos = new Vector3Int(v.x, v.y, 0);
           array[counter] = tilemap.GetTile(pos);
           counter++;
       }
       return array;
   }

   private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
   {
        Debug.Log("SetTilesBlock(,,,)");
        int size = area.size.x * area.size.y * area.size.z;
       TileBase[] tileArray = new TileBase[size];
       FillTiles(tileArray, type);
       tilemap.SetTilesBlock(area, tileArray);

   }

   #endregion


   #region Building Placement

   public void InitializeWithBuilding() //생성버튼 눌렀을 때 building 을 prefab으로 해서 생성
   {
        GameObject temp_gameObject = Instantiate(GameManager.CurrentBuilding, Vector3.zero, Quaternion.identity,buildings.transform) as GameObject;
      
        temp = temp_gameObject.GetComponent<Building>(); // 이때 building 프리펩의 속성 불러오기
        Debug.Log("uuuuuuuuu"+ GameManager.BuildingArray.Length);
        for (int i = 0; i < GameManager.BuildingArray.Length; i++)
        {
            if (GameManager.BuildingArray[i].Building_Image==temp.Building_Image)
            {
                Debug.Log("Good");
                temp.SetValue(GameManager.BuildingArray[i]);
                break;
            }
        }


        temp.Type = BuildType.Make;

        temp.Rotation_Pannel.gameObject.SetActive(false);
        temp.UpgradePannel.SetActive(false);//업그레이드 패널 삭제
        temp.Placed = false;            //건물은 현재 배치가 안 된 상태
        //temp.Building_name = temp_gameObject.name;
        FollowBuilding(false);           //건물이 마우스 따라가게 하는 함수

   }
    public void ClickWithBuilding(Building click_building)
    {
        temp = click_building;
        temp.Placed = false;            //건물은 현재 배치가 안 된 상태
        ClearArea();
    }
    private void ClearArea()
   {
        Debug.Log("ClearArea()");
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];//0
        Debug.Log(prevArea.position);
       FillTiles(toClear, TileType.Empty);
       TempTilemap.SetTilesBlock(prevArea, toClear);
   }

   private void FollowBuilding(bool isTransfer)                    //건물이 마우스 따라가게
   {
        Debug.Log("Following");
       ClearArea();


       temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
       BoundsInt buildingArea = temp.area;

       TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);
        int size = baseArray.Length;
 
       
       TileBase[] tileArray = new TileBase[size];

       for (int i = 0; i<baseArray.Length; i++)
       {

           if (baseArray[i] == tileBases[TileType.White])
           {
               tileArray[i] = tileBases[TileType.Green];            //건물을 놓을 수 있다
            }
           else
           {
                FillTiles(baseArray, TileType.Red);
                FillTiles(tileArray, TileType.Red);                  //건물을 놓을 수 없다
                
                break;
           }
       }
       TempTilemap.SetTilesBlock(buildingArea, tileArray);
       prevArea = buildingArea;


   }

   public bool CanTakeArea(BoundsInt area)
   {
        Debug.Log("CanTakeArea()");
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        Debug.Log("CanTakeArea()         :"+ baseArray.Length);
        foreach (var b in baseArray)
       {
           if ( b != tileBases[TileType.White])
           {
               Debug.Log("둘 수 없습니다.");
               return false;
           }
       }
       return true;
   }
   // 빈공간에 둘 수없음 작동o

   public void TakeArea(BoundsInt area)
   {
        Debug.Log("TakeArea()");
       SetTilesBlock(area, TileType.Empty, TempTilemap);        //TmpTilemap 비우기
       SetTilesBlock(area, TileType.Red, MainTilemap);
   }
    public void RemoveArea(BoundsInt area)
    {
        Debug.Log("RemoveArea()");
        SetTilesBlock(area, TileType.Empty, TempTilemap);        //TmpTilemap 비우기
        SetTilesBlock(area, TileType.White, MainTilemap);
    }
    #endregion


    //추가
    private void FollowMouse()
   {
       transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       transform.position = new Vector3(transform.position.x, transform.position.y, 0);
   }

 

        //button management 공간 1101 추가


    public void buttoncancel()
    {

        // temp2.ClearArea();
        Destroy(temp.gameObject);

    }



}

public enum TileType
{
    Empty,
    White,
    Green,
    Red
}
