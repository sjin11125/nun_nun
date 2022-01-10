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

    public GameObject activeObj;

    public string currentColor;
    public string currentShape;
    public Sprite keepImage;
    public static bool UseKeepBool = false;

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
        if (SquareOccupied == false)//사용중이 아니면
        {
            Selected = true;//선택된걸로바꿔
            hooverImage.gameObject.SetActive(true);//진한색
            
            GameObject ShapeStorageObj = GameObject.FindGameObjectWithTag("ShapeStorage");
            if (ShapeStorageObj != null)//여기서 항상 들어갈때 shape의 정보를 받는다
            {                             
                currentColor = ShapeStorageObj.GetComponent<ShapeStorage>().shapeColor;
                currentShape = ShapeStorageObj.GetComponent<ShapeStorage>().shapeShape;               
            }
        }
        else if(collision.GetComponent<ShapeSquare>() != null)//쉐이프와 닿아있음
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();//쉐이프 레드라이트
        }
        UseKeepBool = false;
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

        GameObject GridObj = GameObject.FindGameObjectWithTag("Grid");
        if (GridObj != null && UseKeepBool == true)
        {
            currentColor = GridObj.GetComponent<GridScript>().KeepColor;//나갈때확인해서 keep을 사용했던거라면 
            currentShape = GridObj.GetComponent<GridScript>().KeepShape;
        }
    }

    public void TrashCan()
    {
        activeImage.sprite = normalImage.sprite;
    }
    public void UseSquareKeep()//킵 프리팹과 닿으면 켜지는 함수
    {
        UseKeepBool = true;
        hooverImage.gameObject.SetActive(false);//선택되고있는중에뜨는 진한색끄고
        activeImage.gameObject.SetActive(true);//선택된 색 켜기

        Selected = true; //선택됨
        SquareOccupied = true; //사용중
        gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = keepImage;
    }
}
