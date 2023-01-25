using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour
{
    public Image hooverImage;
    public Image activeImage;
    public Image normalImage;
    public List<Sprite> normalImages;

    [HideInInspector]
    public Image spriteImage;

    public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied { get; set; }

    public string currentColor;
    public string currentShape;
    public string[] currentAchieveId;
    public bool UseKeepBool;

    private float clickTime;
    private float minClickTime = 0.8f;
    private bool isClick;
    GameObject rainbowObj;
    GameObject ChangeShapeObj;
    GameObject squareImage;

    public bool shinActive;

    public Sprite trashAndKeep;
    public bool IMtrash = false;

    public GameObject KeepShapeObj;
    public bool IMkeep = false;
    Sprite currentSprite;
    bool currentShin;

    private GameObject settigPanel;

    void Awake()
    {
        Selected = false;
        SquareOccupied = false;
        currentColor = null;
        currentShape = null;
        currentAchieveId = new string[2] { null,null};
        shinActive = false;
        UseKeepBool = false;

        GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
        if (contectShape != null)
        {
            squareImage = contectShape.transform.GetChild(0).gameObject;
            spriteImage = squareImage.GetComponent<Image>();
        }
        GameObject GetRainbow = GameObject.FindGameObjectWithTag("ItemController");//컨트롤러 다섯번째 자식인
        if(GetRainbow != null)
        {
            rainbowObj = GetRainbow.transform.GetChild(4).gameObject;//레인보우 아이템 오브젝트를 받아
            ChangeShapeObj = GetRainbow.transform.GetChild(5).gameObject;
        }
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }

    
    private void FixedUpdate()
    {
        if (UseKeepBool)
        {
            GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>().CheckIfKeepLineIsCompleted();
            UseKeepBool = false;
        }
    }
    
    private void Update()
    {
        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(wp, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            if (gameObject.transform.GetChild(2).gameObject.activeSelf)//켜진상태 이후에 롱클릭
            {
                if (clickTime >= minClickTime)//롱클릭이었으면
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            if(GridScript.RainbowItemTurn <= 0 && RainbowItem.rainbowActive)
                            {
                                rainbowObj.GetComponent<RainbowItem>().RainbowItemUse(currentShape);//레인보우 아이템 함수 호출
                                RainbowItem.squareColorObj = this.gameObject;
                            }
                            else if (GridScript.ChangeShapeItem <= 0 && ChangeShapeItem.changeActive)
                            {
                                ChangeShapeObj.GetComponent<ChangeShapeItem>().RainbowItemUse(currentColor);//컬러바꾸는 아이템 함수 호출
                                ChangeShapeItem.squareObj = this.gameObject;
                            }
                        }
                    }
                }
            }
            clickTime = 0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isClick = false;               
        }

        if (isClick)
        {
            clickTime += Time.deltaTime;
        }
    }

    public void PlaceShapeOnBoard()//그리스 스크립트 CheckIfShapeCanBePlaced에서 사용
    {
        ActivateSquare();
    }

    public void ActivateSquare()
    {
        settigPanel.GetComponent<AudioController>().Sound[2].Play();
        hooverImage.gameObject.SetActive(false);//선택되고있는중에뜨는 진한색끄고
        activeImage.gameObject.SetActive(true);//선택된 색 켜기

        if (squareImage.transform.GetChild(0).gameObject.activeSelf && !IMtrash && !IMkeep)//shin이 켜져있으면
        {
            activeImage.transform.GetChild(0).gameObject.SetActive(true);
            shinActive = true;
        }

        Selected = true; //선택됨
        SquareOccupied = true; //사용중
        
        if (activeImage.gameObject.activeSelf == true)
        {
            if (IMtrash)
            {
                activeImage.sprite = trashAndKeep;
                if (GridScript.TrashItemTurn < 1)
                {
                    GridScript.TrashItemTurn = 20;
                }
            }
            else if (IMkeep)
            {
                activeImage.sprite = trashAndKeep;
                if (GridScript.KeepItemTurn < 1)
                {
                    GameObject keepInstance = Instantiate(KeepShapeObj, this.transform.parent);
                    keepInstance.transform.localPosition = new Vector3(-377f, -660.5f, 0);
                    keepInstance.GetComponent<CreateKeepShape>().keepColor = currentColor;
                    keepInstance.GetComponent<CreateKeepShape>().keepShape = currentShape;
                    keepInstance.GetComponent<CreateKeepShape>().keepSprite = spriteImage.sprite;
                    keepInstance.GetComponent<Image>().sprite = spriteImage.sprite;
                    keepInstance.GetComponent<CreateKeepShape>().keepShin = currentShin;
                    NonKeep();
                }
            }
            else
            {
                if (UseKeepBool)
                {
                    activeImage.sprite = currentSprite;                   
                }
                else
                {
                    activeImage.sprite = spriteImage.sprite;//쉐이프 스프라이트 전달
                }
            }
        }
    }

    public void Deactivate()
    {
        activeImage.gameObject.SetActive(false);
        activeImage.sprite = null;//사라지고나면 색깔 안담기게해놓기 이거사실없어도될듯?
        currentColor = null;
        currentShape = null;
        currentAchieveId = new string[2] { null,null};

        activeImage.transform.GetChild(0).gameObject.SetActive(false);
        shinActive = false;
    }

    public void NonKeep()//keep 열에 나머지 애들에 사용
    {
        gameObject.SetActive(false);
    }

    public void ClearOccupied()
    {
        Selected = false;
        SquareOccupied = false;
    }

    public void SetImage(bool setFirstImage)
    {
        normalImage.sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)//충돌처음
    {
        if (SquareOccupied == false)//사용중이 아니면
        {
            Selected = true;//선택된걸로바꿔
            hooverImage.gameObject.SetActive(true);//진한색
            
            GameObject ShapeStorageObj = GameObject.FindGameObjectWithTag("ShapeStorage");
            if (ShapeStorageObj != null)//여기서 항상 들어갈때 shape의 정보를 받는다
            {                             
                currentColor = ShapeStorageObj.GetComponent<ShapeStorage>().shapeColor;
                currentShape = ShapeStorageObj.GetComponent<ShapeStorage>().shapeShape;
                currentShin = collision.transform.GetChild(0).gameObject.activeSelf;
                currentAchieveId = ShapeStorageObj.GetComponent<ShapeStorage>().shapeAchieveId;
            }
        }
        else if(collision.GetComponent<ShapeSquare>() != null)//쉐이프와 닿아있음
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();//쉐이프 레드라이트
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//충돌중
    {
        Selected = true;//선택된걸로바꿔

        if (SquareOccupied == false)//사용중이 아니면
        {            
            hooverImage.gameObject.SetActive(true);
        }
        else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)//충돌벗어남
    {
        if (SquareOccupied == false)//사용중이 아니면
        {
            Selected = false;//선택안된걸로해
            hooverImage.gameObject.SetActive(false);//진한색 꺼
            currentColor = null;
            currentShape = null;
        }
        else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().UnSetOccupied();//레드라이트꺼
        }
    }

    public void UseSquareKeep(string color, string shape, Sprite sprite)//킵 프리팹과 닿으면 켜지는 함수
    {
        UseKeepBool = true;
        currentColor = color;//킵 생성 후 커런트 컬러에 킵 컬러가 담김
        currentShape = shape;
        currentSprite = sprite;
        ActivateSquare();
    }
}
