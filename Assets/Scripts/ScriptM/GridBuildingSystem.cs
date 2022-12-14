using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UniRx;
[Serializable]
public class GridBuildingSystem : MonoBehaviour
{
    //1101 추가
    // public GameObject OkButton;

    private Button button;
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    #region 타일맵 Properties
    public Tilemap MainTilemap
    {
        get { return MainTilemaps; }
        set { MainTilemaps = value; }
    }
    public static Tilemap MainTilemaps;
    public Tilemap TempTilemap
    {
        get { return TempTilemaps; }
        set { TempTilemaps = value; }
    }
    public static Tilemap TempTilemaps;

    #endregion
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private Building temp; //building type으로 temp 생성
    private Vector3 prevPos;
    public BoundsInt prevArea;
    public BoundsInt prevArea2;
    public GameObject UpgradePannel;
    GameObject Grid;
    public Button StartButton;

 
    public GameObject buildings;
    GameObject Canvas;

    public GameObject Dialog;           //대화창
    //추가 1110
    public GameObject temp_gameObject;
    bool isEditing=false;
        //------------------------세이브 관련 변수들--------------------------------------
    public static bool isSave = false;          //건물 건설이나 삭제했을 때 건물들 저장하는 변수
    public BuildingSave BSave;

    float second = 0;
    bool isGrid = false;

    public GameObject VisitorBooksWindow;           //방명록창

    private GameObject settigPanel;
    Touch tempTouchs;
    #region unity Methods  
    public GameObject Effect;
    bool upgrade = false;

    public static Subject<Building> OnEditMode = new Subject<Building>();
    private void Awake()
    {
        if (current==null)
        {
            current = this;
            //isStart = true;
        }
        else if (current != this) // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        {
            Destroy(gameObject);
        }
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }
    private void Start()
    {
        
        string tilePath = @"Tiles\";

        if (GameManager.isStart .Equals( true)  )      //tileBases에 아무것도 없으면
        {
            tileBases.Add(TileType.Empty, null);
            tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "white"));
            tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "green"));
            tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "red"));
            GameManager.isStart = false;

        }

        Grid = GameObject.Find("back_down");
        Canvas= GameObject.Find("Canvas");
        // if (SceneManager.GetActiveScene().name.Equals("Main")
        // StartButton = GameObject.Find("Start").GetComponent<Button>();
        OnEditMode.Subscribe(temp=>
        {
            EditMode(temp);
        }).AddTo(this);

    }
    public void GridLayerSetting()
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -45;             //메인 타일 보이게
    }
    public void GridLayerNoSetting()
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -50;             //메인 타일 안보이게
    }
   public  void EditMode(Building tempBuilding)
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -45;             //메인 타일 보이게
        GameManager.CurrentBuilding_Script = temp;
        //UI_Manager.StartOpen();     //ui 중앙으로 이동
        tempBuilding.Type = BuildType.Move;
        tempBuilding.Placed = false;        //배치가 안 된 상태로 변환

        temp.area.position = gridLayout.WorldToCell(tempBuilding.gameObject.transform.position);
        BoundsInt buildingArea = tempBuilding.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);
        int size = baseArray.Length;
        for (int i = 0; i < size; i++)
        {
            baseArray[i] = tileBases[TileType.Empty];
        }
        TempTilemap.SetTilesBlock(buildingArea, baseArray);
        SetTilesBlock(buildingArea, TileType.White, MainTilemap);

    }
    private void Update()
    {
        if (ChaButtonScript.isEdit.Equals(true))
        {
            ChaButtonScript.isEdit = false;
            InitializeWithBuilding();
        }
        if (GameManager.isEdit.Equals(true))
        {
            GameManager.isEdit = false;
            isEditing = true;
            InitializeWithBuilding();
            temp.Type = BuildType.Move;
        }
        if (GameManager.isInvenEdit.Equals(true))
        {
            GameManager.isInvenEdit = false;
            InitializeWithBuilding_InvenButton();
            temp.Type = BuildType.Move;
        }
        if (isGrid .Equals( true))
        {
            second += Time.deltaTime;
        }
        else
        {
            second = 0;
        }
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return; //UI 터치가 감지됐을 경우 return
                        //여기서부터 화면 터치 코드
        }


        if (EventSystem.current.IsPointerOverGameObject(0))      //UI를 클릭했냐
        {
            return;
        }
        if (!CameraMovement.isTouch && Input.GetMouseButtonUp(0) && SceneManager.GetActiveScene().name .Equals( "Main"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.transform != null)          // 오브젝트를 클릭 했을 때
            {
                Transform Building = hit.transform.parent;
              
                if (temp != null)
                {
                    if (temp.Placed.Equals(false))             //건물이 배치가 안 된 상태인가?
                    {
                        Building hit_building = temp.GetComponent<Building>();
                        if (hit.transform.CompareTag("Button"))      //건물 배치 확인 버튼
                        {
                            if (temp.CanBePlaced())         //건물이 배치 될 수 있는가? 네
                            {
                                //temp.level += 1;        //레벨 +1
                                temp.Place(temp.Type);
                                if (GameManager.CurrentBuilding_Button != null)       //인벤이 눌렀나
                                {
                                    //temp.level += 1;        //레벨 +1
                                    temp.Place(temp.Type);


                                    Grid.GetComponent<SpriteRenderer>().sortingOrder = -48;             //메인 타일 안보이게
                                    StartButton.enabled = true;
                                    temp = null;
                                    isEditing = false;
                                    GameManager.CurrentBuilding_Script = null;
                                    if (GameManager.CurrentBuilding_Button != null)       //인벤이 눌렀나
                                    {
                                        GameManager.CurrentBuilding_Button.this_building.isLock = "T";
                                        GameManager.CurrentBuilding_Button = null;
                                    }

                                }
                                MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -50;       //메인 타일 안보이게
                                StartButton.enabled = true;
                                temp = null;
                                isEditing = false;
                                GameManager.CurrentBuilding_Script = null;

                                settigPanel.GetComponent<AudioController>().Sound[1].Play();
                            }
                            if (GameObject.FindWithTag("TutoBuy")!=null)
                            {
                                GameObject.FindWithTag("TutoBuy").GetComponent<TutorialsItemControl>().goNext = true;
                            }
                            // button.buttonok();
                        }
                        if (hit.transform.CompareTag("Rotation"))        //건물 회전 버튼
                        {

                            if (hit_building.isFliped .Equals("T"))
                                hit_building.isFliped = "F";
                            else
                                hit_building.isFliped = "T";
                            
                            hit_building.Rotation();

                            settigPanel.GetComponent<AudioController>().Sound[0].Play();
                        }
                        if (hit.transform.CompareTag("Upgrade"))         //업그레이드
                        {
                            GameManager.isMoveLock = true;
                            //hit_building.Type = BuildType.Upgrade;
                            upgrade= hit_building.Upgrade();
                            if (upgrade==false&& hit_building.Level<2)
                            {
                                Effect.SetActive(true);
                            }
                            settigPanel.GetComponent<AudioController>().Sound[1].Play();
                        }
                        if (hit.transform.CompareTag("Remove"))          //제거
                        {
                            hit_building.Sell_Pannel();
                            //temp.Remove(temp);
                            //UI_Manager.Start();
                            MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -50;         //메인 타일 안보이게

                            settigPanel.GetComponent<AudioController>().Sound[1].Play();
                        }
                    }

                }
               
                if (hit.transform.CompareTag("Nuni"))        //누니 클릭
                {

                    GameObject nuni = hit.transform.parent.gameObject;
                    Card nuni_card = nuni.GetComponent<Card>();
                    if (nuni_card.isDialog==false)                                               //누니 대사 안겹치게
                    {
                        nuni_card.isDialog=true; 

                           NuniDialog nuni_dialog = new NuniDialog();

                        for (int i = 0; i < GameManager.NuniDialog.Count; i++)
                        {
                            Debug.Log(GameManager.NuniDialog[i].Nuni);
                            if (nuni_card.cardName.Equals(GameManager.NuniDialog[i].Nuni))
                            {
                                Debug.Log(nuni_card.cardName);
                                nuni_dialog = GameManager.NuniDialog[i];
                            }
                        }

                        GameObject dialo_window = Instantiate(Dialog, Canvas.transform);
                        //child[2]
                        dialo_window.transform.SetAsFirstSibling();
                        dialo_window.GetComponent<NuniDialogParsing>().nuni = nuni_card;
                        dialo_window.GetComponentInChildren<Text>().text = nuni_dialog.Dialog[UnityEngine.Random.Range(0, nuni_dialog.Dialog.Length - 1)];

                        dialo_window.GetComponent<NuniDialogParsing>().nuniObject = hit.transform.parent.gameObject;
                        dialo_window.GetComponent<NuniDialogParsing>().isMove = true;
                        //dialo_windowi

                        settigPanel.GetComponent<AudioController>().Sound[0].Play();

                        StartCoroutine(isDialog_done(nuni_card, dialo_window));
                    }
                    
                }
                else if (hit.transform.CompareTag("bunsu"))              //생명의 분수 클릭
                {
                    settigPanel.GetComponent<AudioController>().Sound[1].Play();
                    SceneManager.LoadScene("Shop");
                }
            }
        }
        if (!CameraMovement.isTouch && Input.GetMouseButton(0) && SceneManager.GetActiveScene().name .Equals( "Main"))                    //그냥 클릭했을 때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.transform != null)          // 오브젝트를 클릭 했을 때
            {
                if (hit.transform.CompareTag("VisitorBook"))
                {
                    VisitorBooksWindow.SetActive(true);
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
        else if (!CameraMovement.isTouch && Input.GetMouseButton(0) && SceneManager.GetActiveScene().name .Equals( "FriendMain"))         //친구 씬에서 방명록킬때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);



            if (hit.transform != null)          // 오브젝트를 클릭 했을 때
            {
                if (hit.transform.CompareTag("VisitorBook"))
                {
                    VisitorBooksWindow.SetActive(true);
                }

            }
        }


            if (second >= 1.3f&& isEditing.Equals(false))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Building"))               //건물이라면
                {
                   

                }
                else if(hit.transform.CompareTag("Nuni"))           //누니라면
                {

                }
            }
            
            // BuildEdit(hit);
            isGrid = false;
        }
        if (Input.GetMouseButtonDown(0)&&SceneManager.GetActiveScene().name.Equals("Main")) //마우스를 누르고 있을 때             지금 내 닉넴과 마을
        {



            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Building") && !hit.collider.CompareTag("Nuni"))
                {

                    isGrid = true;

                    //StartCoroutine(WaitSecond(hit));
                }
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
        for (int i = 0; i < arr.Length; i++)
       {
           arr[i] = tileBases[type];
       }
   }


   private static TileBase[] GetTilesBlock (BoundsInt area, Tilemap tilemap)
   {
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
        int size = area.size.x * area.size.y * area.size.z;
       TileBase[] tileArray = new TileBase[size];
       FillTiles(tileArray, type);
       tilemap.SetTilesBlock(area, tileArray);

   }

    #endregion
  
    IEnumerator isDialog_done(Card nuni, GameObject dialog_Window)
    {
        yield return new WaitForSeconds(3f);
        nuni.isDialog = false;
        Destroy(dialog_Window);
    }
    #region Building Placement

    public void InitializeWithBuilding() //생성버튼 눌렀을 때 building 을 prefab으로 해서 생성
   {
        temp_gameObject = Instantiate(GameManager.CurrentBuilding, Vector3.zero, Quaternion.identity,buildings.transform) as GameObject;
        
          temp = temp_gameObject.GetComponent<Building>(); // 이때 building 프리펩의 속성 불러오기
        
        for (int i = 0; i < GameManager.BuildingArray.Length; i++)
        {
            if (GameManager.BuildingArray[i].Building_Image.Equals(temp.Building_Image))
            {
                GameManager.BuildingArray[i].Level = 1;
                temp.SetValue(GameManager.BuildingArray[i]);
                break;
            }
        }
        for (int i = 0; i < GameManager.StrArray.Length; i++)
        {
            if (GameManager.StrArray[i].Building_Image .Equals( temp.Building_Image))
            {
                GameManager.StrArray[i].Level = 1;
                temp.SetValue(GameManager.StrArray[i]);
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
    public void InitializeWithBuilding_InvenButton() //인벤버튼 눌렀을 때 building 을 prefab으로 해서 생성
    {
        temp_gameObject = Instantiate(GameManager.CurrentBuilding, Vector3.zero, Quaternion.identity, buildings.transform) as GameObject;

        temp = temp_gameObject.GetComponent<Building>(); // 이때 building 프리펩의 속성 불러오기
        temp.SetValue(GameManager.CurrentBuilding_Script);
        for (int i = 0; i < GameManager.BuildingArray.Length; i++)
        {
            if (GameManager.BuildingArray[i].Building_Image .Equals( temp.Building_Image))
            {
                temp.Cost = GameManager.BuildingArray[i].Cost;
                break;
            }
        }
        for (int i = 0; i < GameManager.StrArray.Length; i++)
        {
            if (GameManager.StrArray[i].Building_Image .Equals( temp.Building_Image))
            {
                GameManager.StrArray[i].Level = 1;
                temp.SetValue(GameManager.StrArray[i]);
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
    public void ClearArea()
   {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];//0
       FillTiles(toClear, TileType.Empty);
       TempTilemap.SetTilesBlock(prevArea, toClear);
   }
    public void ClearArea2()
    {
        TileBase[] toClear = new TileBase[prevArea2.size.x * prevArea2.size.y * prevArea2.size.z];//0
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea2, toClear);
        SetTilesBlock(prevArea2, TileType.White, MainTilemap);

    }
    

    private void FollowBuilding(bool isTransfer)                    //건물이 마우스 따라가게
   {
       ClearArea();


       temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
       BoundsInt buildingArea = temp.area;

       TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);
        int size = baseArray.Length;
 
       
       TileBase[] tileArray = new TileBase[size];

       for (int i = 0; i<baseArray.Length; i++)
       {

           if (baseArray[i] .Equals( tileBases[TileType.White]))
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
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        foreach (var b in baseArray)
       {
           if ( b != tileBases[TileType.White])
           {
               return false;
           }
       }
       return true;
   }
   // 빈공간에 둘 수없음 작동o

   public void TakeArea(BoundsInt area)
   {
       SetTilesBlock(area, TileType.Empty, TempTilemap);        //TmpTilemap 비우기
       SetTilesBlock(area, TileType.Red, MainTilemap);
   }
    public void RemoveArea(BoundsInt area)
    {
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
