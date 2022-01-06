using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject keepTimer;
    public GameObject activeObj;

    [HideInInspector]
    public int keepCount, trashCount;

    public string currentColor;
    public string currentShape;
    public GameObject KeepShape;

    void Start()
    {
        Selected = false;
        SquareOccupied = false;
        currentColor = null;
        currentShape = null;

        GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
        if (contectShape != null)
        {
            GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
            spriteImage = squareImage.GetComponent<Image>();
        }
    }

    void Update()
    {
        if (keepTimer.activeSelf ==false)
        {
            keepCount = 0;
        }
        if (keepTimer.activeSelf == false)
        {
            trashCount = 0;
        }
    }

    //temp function remove it
    public bool CanWeUseThisSquare()
    {
        return hooverImage.gameObject.activeSelf;
    }

    public void PlaceShapeOnBoard()//그리스 스크립트 CheckIfShapeCanBePlaced에서 사용
    {
        ActivateSquare();
    }

    public void ActivateSquare()
    {
        hooverImage.gameObject.SetActive(false);//선택되고있는중에뜨는 진한색끄고
        activeImage.gameObject.SetActive(true);//선택된 색 켜기

        Selected = true; //선택됨
        SquareOccupied = true; //사용중

        if (activeImage.gameObject.activeSelf == true)
        {
            activeImage.GetComponent<Image>().sprite = spriteImage.sprite;//쉐이프 스프라이트 전달
        }
    }

    public void Deactivate()
    {
        activeImage.gameObject.SetActive(false);
        activeImage.GetComponent<Image>().sprite = null;//사라지고나면 색깔 안담기게해놓기 이거사실없어도될듯?
        currentColor = null;
        currentShape = null;
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
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)//충돌처음
    {
        if(SquareOccupied == false)//사용중이 아니면
        {
            Selected = true;//선택된걸로바꿔
            hooverImage.gameObject.SetActive(true);//진한색
            GameObject ShapeStorageObj = GameObject.FindGameObjectWithTag("ShapeStorage");
            if (ShapeStorageObj != null)
            {
                currentColor = ShapeStorageObj.GetComponent<ShapeStorage>().shapeColor;
                currentShape = ShapeStorageObj.GetComponent<ShapeStorage>().shapeShape;
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

    GameObject twentyNine;
    int keepSquareIndex;
    //public GameObject prefab;
    public void ColorTransfer() //그리드스크립트 UseKeep과 연결
    {
        GameObject ItemControllerObj = GameObject.FindGameObjectWithTag("ItemController");//컨트롤러에서 선택한 인덱스에 따라 위치 결정
        if (ItemControllerObj != null)
        {
            keepSquareIndex = ItemControllerObj.GetComponent<ItemController>().keepItemIndex;
        }

        GameObject contectGrid = GameObject.FindGameObjectWithTag("Grid");
        if (contectGrid != null)
        {
            twentyNine = contectGrid.transform.GetChild(keepSquareIndex).gameObject; //29번오브젝트저장
        }

        if (Input.GetMouseButtonDown(0)) //좌클할때
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);//광선을 쏴
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == twentyNine && keepCount == 0)// && activeObj.activeSelf==true)//내가 누른게 29번오브젝트가 맞고 한번 눌렀고 액티브오브젝트가 켜져야
                {
                    keepOnclick();
                    /*
                    spriteImage.sprite = activeImage.GetComponent<Image>().sprite;//저장해둔 색깔을 shape에 줘
                    GameObject useKeepShape = GameObject.FindGameObjectWithTag("ShapeStorage");//keep에있는 색깔과모양정보를 현재 shape에 준다
                    if (useKeepShape != null)
                    {
                        useKeepShape.GetComponent<ShapeStorage>().shapeColor = currentColor;
                        useKeepShape.GetComponent<ShapeStorage>().shapeShape = currentShape;
                    }*/
                    currentColor = null;//주고나서 비운다
                    currentShape = null;
                    keepTimer.SetActive(true);                  
                    activeObj.SetActive(false);
                    keepCount++;
                }
            }
        }
    }

    public void TrashCan()
    {
        activeImage.sprite = normalImage.sprite;

        if (trashCount == 0)
        {  
             keepTimer.SetActive(true);
             activeObj.SetActive(false);
             trashCount++;
        }
    }
    
    public void keepOnclick()
    {
        GameObject keepInstance = Instantiate(KeepShape) as GameObject;
        keepInstance.transform.SetParent(this.transform, false);
        Vector3 pos = new Vector3(0, 0, 0);
        keepInstance.transform.localPosition = pos;

        GameObject useKeepShape = GameObject.FindGameObjectWithTag("KeepShape");//keep에있는 색깔과모양정보를 현재 shape에 준다
        if (useKeepShape != null)
        {
            useKeepShape.GetComponent<CreateKeepShape>().keepColor = currentColor;
            useKeepShape.GetComponent<CreateKeepShape>().keepShape = currentShape;
            useKeepShape.GetComponent<Image>().sprite = activeImage.sprite;
        }
        
    }
}
