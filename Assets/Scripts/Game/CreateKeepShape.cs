using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateKeepShape : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler //, IDragHandler
{
    private Canvas canvas;
    bool drop = false;
    private RectTransform rectTransform;
    //0.keep을 썻을때 뜨게
    //1.GridSquare에 있던 activeImage currentColor currentShape 정보를 저장, 적용 //잘못된 컬러 정보를 담고있는중임
    //2.sprite, color, shape 정보 전달

    public string keepColor;
    public string keepShape;
    public Image keepImg;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GridSquare")
        {
            if (collision.gameObject.GetComponent<GridSquare>().SquareOccupied == false)//비어있으면
            {
                //sprite, color, shape 정보 전달
                drop = true;
            }
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        drop = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (drop == true)//놓았으면
        {
            Destroy(this.gameObject);
        }
    }
}
