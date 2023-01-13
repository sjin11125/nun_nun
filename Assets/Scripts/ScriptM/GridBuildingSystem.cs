using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
[Serializable]
public class GridBuildingSystem : MonoBehaviour
{
    //1101 추가
    // public GameObject OkButton;

    private Button button;
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    #region 타일맵 Properties
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    #endregion
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public Building temp; //building type으로 temp 생성
    private Vector3 prevPos;
    public BoundsInt prevArea;
    public BoundsInt prevArea2;
    GameObject Grid;
    public Button StartButton;



    
 
   

    public GameObject Dialog;           //대화창
    //추가 1110
    public GameObject temp_gameObject;
    public static ReactiveProperty<bool> isEditing=new ReactiveProperty<bool>();             //건설모드
        //------------------------세이브 관련 변수들--------------------------------------
    public static bool isSave = false;          //건물 건설이나 삭제했을 때 건물들 저장하는 변수
    public BuildingSave BSave;

    float second = 0;
    bool isGrid = false;

    public GameObject VisitorBooksWindow;           //방명록창

    private GameObject settigPanel;
    #region unity Methods  
    public GameObject Effect;
    GameObject CurrentBuilding;

    public static GameObject Canvas;
    public static Subject<Building> OnEditMode = new Subject<Building>();
    public static Subject<Building> OnEditModeOff = new Subject<Building>();
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

        if (GameManager.isStart.Equals(true))      //tileBases에 아무것도 없으면
        {
            tileBases.Add(TileType.Empty, null);
            tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "white"));
            tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "green"));
            tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "red"));
            GameManager.isStart = false;

        }

        Grid = GameObject.Find("back_down");
        Canvas= GameObject.Find("Canvas");
       

        OnEditMode.Subscribe(temp =>                     //건설모드 구독
        {
            
            this.temp = temp;

            EditMode(temp);

            isEditing.Value = true;
        }).AddTo(this);

        OnEditModeOff.Subscribe(temp =>                        //건설모드끄기 구독                  
        {
            isEditing.Value = false;
            this.temp = temp;
            EditModeOff(temp);
        }).AddTo(this);

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ =>
               {
                   try
                   {

                   if (isEditing.Value)
                   {

                       if (!IsPointerOverGameObject())         //UI 위에 있는지 체크
                        {
                               if (temp==null)
                               {
                                   return;
                               }
                           Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //마우스 월드 좌표 받아옴
                            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
                           if (prevPos != cellPos)
                           {
                               try
                               {

                                       Vector3 pos = new Vector3(gridLayout.CellToLocalInterpolated(cellPos
                               + new Vector3(.5f, .5f, 0f)).x, gridLayout.CellToLocalInterpolated(cellPos
                               + new Vector3(.5f, .5f, 0f)).y, gridLayout.CellToLocalInterpolated(cellPos
                               + new Vector3(.5f, .5f, 0f)).z
                               );
                                      
                                     
                                  // temp.BuildingPosition = new Vector2(temp.transform.localPosition.x, temp.transform.localPosition.y);
                                //temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                              //  + new Vector3(.5f, .5f, 0f)); //Vector3
                                prevPos = cellPos;

                                      // temp.pos = temp.transform.localPosition;
                               FollowBuilding(pos); // 마우스가 위의 좌표 따라감. 
                                       
                               }
                               catch (Exception e)
                               {
                                   Debug.LogError(e.Message);
                                   throw;
                               }
                           }
                           //Debug.Log(temp.gameObject.transform.position);
                       }
                   }

                   }
                   catch (Exception e )
                   {
                       Debug.LogError(e.Message);
                       throw;
                   }
               }).AddTo(this);
    
    }
    public static bool IsPointerOverGameObject()           //UI가 터치되었는지
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    public void GridLayerSetting()
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -45;             //메인 타일 보이게
    }
    public void GridLayerNoSetting()
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -50;             //메인 타일 안보이게
    }
   public  void EditMode(Building tempBuilding)                         //건축 모드 
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -45;             //메인 타일 보이게
       // GameManager.CurrentBuilding_Script = temp;


       // tempBuilding.area.position = gridLayout.WorldToCell((Vector3)tempBuilding.BuildingPosition);
        BoundsInt buildingArea = tempBuilding.area;

        switch (tempBuilding.Type)
        {
            case BuildType.Empty:
                break;
            case BuildType.Load:
               
                    
                    RemoveArea(buildingArea);      //타일 초기화
                
                break;
            
            case BuildType.Move:
                TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);                //해당 건물이 있던 타일 불러오기
                int size = baseArray.Length;
                for (int i = 0; i < size; i++)
                {
                    baseArray[i] = tileBases[TileType.Empty];
                }
                TempTilemap.SetTilesBlock(buildingArea, baseArray);
                SetTilesBlock(buildingArea, TileType.White, MainTilemap);

                break;
            case BuildType.Rotation:
                break;
            
            case BuildType.Upgrade:
                break;
            case BuildType.Remove:
                break;
            default:
                break;
        }

     
    }
   public  void EditModeOff(Building tempBuilding)                         //건축 모드 Off
    {
        MainTilemap.GetComponent<TilemapRenderer>().sortingOrder = -50;             //메인 타일 보이게
        //GameManager.CurrentBuilding_Script = null;


        //tempBuilding.area.position = gridLayout.WorldToCell(tempBuilding.gameObject.transform.position);
        BoundsInt buildingArea = tempBuilding.area;

         if (tempBuilding.Type == BuildType.Load)
            RemoveArea(buildingArea) ;

    }
    private void Update()
    {
        
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return; //UI 터치가 감지됐을 경우 return
                        //여기서부터 화면 터치 코드
        }


        /*if (EventSystem.current.IsPointerOverGameObject(0))      //UI를 클릭했냐
        {
            return;
        }*/
        if (!CameraMovement.isTouch && Input.GetMouseButtonUp(0) && SceneManager.GetActiveScene().name .Equals( "Main"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.transform != null)          // 오브젝트를 클릭 했을 때
            {
                Transform Building = hit.transform.parent;
              
              
                
               
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

                        GameObject dialo_window = Instantiate(Dialog);
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
            }
        }

          

 

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
    

    private void FollowBuilding(Vector3 pos)                    //건물이 마우스 따라가게
    {
        try
        {
            temp.area.position = GridBuildingSystem.current.gridLayout.WorldToCell(pos);


            //temp.SetPosition(pos);
            ClearArea();

            
                temp.gameObject.transform.position = pos;
            if(temp.Type==BuildType.Make)
            {
              LoadManager.Currnetbuildings.transform.position = temp.gameObject.transform.position;
            }
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
            if (temp.Type != BuildType.Make)
        {
            LoadManager.Instance.MyBuildings[temp.Id].area = temp.area;
            LoadManager.Instance.MyBuildings[temp.Id].BuildingPosition = temp.BuildingPosition;
        }
        }
        catch (Exception ed)
        {
            Debug.LogError(ed.Message);
            throw;
        }
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
        try
        {

       SetTilesBlock(area, TileType.Empty, TempTilemap);        //TmpTilemap 비우기
       SetTilesBlock(area, TileType.Red, MainTilemap);

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
    }
    public void RemoveArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTilemap);        //TmpTilemap 비우기
        SetTilesBlock(area, TileType.White, MainTilemap);
    }
    #endregion




}


