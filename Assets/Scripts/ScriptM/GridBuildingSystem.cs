using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.EventSystems;

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
    Button StartButton;
    //추가 1110



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
        StartButton = GameObject.Find("Start").GetComponent<Button>();

    }
    private void Update()
    {
        if (ChaButtonScript.isEdit==true)
        {
            ChaButtonScript.isEdit = false;
            InitializeWithBuilding();
        }
        /* if (!temp) //temp 를 변수로 사용
         {
             Debug.Log("temp is null");
             return;
         }*/

        if (Input.GetMouseButtonDown(0)) //마우스를 눌렀을때 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
           
            if (temp!=null)
            {
                Debug.Log("temp is not null");
            }
            if (EventSystem.current.IsPointerOverGameObject(0)) //현재포인트return
            {
                return;
            }
            if (hit.transform != null)          // 오브젝트를 클릭 했을 때
            {
                Transform Building = hit.transform.parent;
                if (temp != null)
                {
                    if (temp.Placed == false)               //건물이 배치가 안 된 상태인가?
                    {

                        if (hit.transform.tag == "Button")      //건물 배치 확인 버튼
                        {
                            if (temp.CanBePlaced())         //건물이 배치 될 수 있는가? 네
                            {
                                temp.level += 1;        //레벨 +1
                                temp.Place(false);
                                

                                Grid.GetComponent<SpriteRenderer>().sortingOrder = -48;
                                StartButton.enabled = true;
                                temp = null;
                            }
                            // button.buttonok();
                        }
                        if (hit.transform.tag == "Rotation")        //건물 회전 버튼
                        {
                            Building.GetComponent<Building>().Rotation();

                        }
                    }

                }
                else             //temp가 없을 때               //건물이 배치 된 상태
                {
                    Debug.Log("temp is null");
                    temp = hit.transform.GetComponent<Building>();
                   
                    if (hit.transform.tag == "Building" && GameManager.isStore == false)           //빌딩을 눌렀을 때 업그레이드 할래 위치 바꿀래 회전할래
                    {
                        Building b = hit.transform.GetComponent<Building>();
                        b.Upgrade();


                        Debug.Log("Level: " + b.level);
                    }
                    temp = null;
                }
                if (hit.transform.tag == "Coin_Button")           //재화 버튼 누르면(되긴 하는데 수정해야함)
                {
                    //Transform BuildingCoin = hit.transform.parent;
                    Building.GetComponent<Building>().Coin_OK();

                    Debug.Log("huan");
                }
            }
            else   // 빈 공간을 클릭했을 때
            {
                if (temp != null)
                {
                    if (!temp.Placed)           //건물이 놓여지지 않았다.(마우스가 클릭하는 데로 건물 따라감)
                    {
                        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
                        Debug.Log("mouse");
                        if (prevPos != cellPos)
                        {
                            temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                                + new Vector3(.5f, .5f, 0f)); //Vector3
                            prevPos = cellPos;
                            FollowBuilding(); // 마우스가 위의 좌표 따라감. 

                        }
                        //1108 추가
                        // 건물을 누르면 위의 기능이 가능하도록 수정해야함.

                        /* else // 그냥 버튼을 눌렀을 때 다음 코드가 실행
                         {
                             button.button1();
                         }
                        */

                    }
                }
            }

        
        
        }
        //다 필요없고 ok버튼을 눌렀을 때

 

    }


   #endregion

   #region Tilemap Management

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
        GameObject temp_gameObject = Instantiate(GameManager.CurrentBuilding, Vector3.zero, Quaternion.identity) as GameObject;
       temp = temp_gameObject.GetComponent<Building>(); // 이때 building 프리펩의 속성 불러오기
        temp.Placed = false;            //건물은 현재 배치가 안 된 상태
        //temp.Building_name = temp_gameObject.name;
        FollowBuilding();           //건물이 마우스 따라가게 하는 함수

   }

   private void ClearArea()
   {
        Debug.Log("ClearArea()");
        Debug.Log(prevArea.size.x+" " + prevArea.size.y+" "+ prevArea.size.z);
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];//0
       FillTiles(toClear, TileType.Empty);
       TempTilemap.SetTilesBlock(prevArea, toClear);
   }

   private void FollowBuilding()                    //건물이 마우스 따라가게
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
           if(baseArray[i] == tileBases[TileType.White])
           {
               tileArray[i] = tileBases[TileType.Green];
           }
           else
           {
               FillTiles(tileArray, TileType.Red);
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
       SetTilesBlock(area, TileType.Empty, TempTilemap);
       SetTilesBlock(area, TileType.Green, MainTilemap);
   }

   #endregion


   //추가
   private void FollowMouse()
   {
       transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       transform.position = new Vector3(transform.position.x, transform.position.y, 0);
   }

   //예시
   /*public void InitializeWithBuilding(GameObject building) //생성버튼 눌렀을 때 building 을 prefab으로 해서 생성
   {

       temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>(); // 이때 building 프리펩의 속성 불러오기
       FollowBuilding();

   }
   */


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
