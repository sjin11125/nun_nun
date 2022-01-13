using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraserItem : MonoBehaviour
{
    private Image squareImage;
    public Image normalImage;
    public GameObject normalObj;
    public GameObject hooverObj;
    bool buttonDown;
    public int ItemTurn= 20;
    void Start()
    {
        GridScript.EraserItemTurn = ItemTurn;
        hooverObj.SetActive(true);
        hooverObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = GridScript.EraserItemTurn.ToString();
    }
    void Update()
    {
        hooverObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = GridScript.EraserItemTurn.ToString();
        
        if (Input.GetMouseButtonDown(0) && buttonDown == true)// && hooverObj.activeSelf == false) //좌클할때&&버튼 눌려있을때
        {
            GridScript.EraserItemTurn = ItemTurn;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);//광선을 쏴
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("GridSquare"))//스퀘어가 선택됐냐
                {
                    GameObject contectChild = hit.collider.gameObject.transform.GetChild(2).gameObject; //세번째 자식에 activeImage있음
                    squareImage = contectChild.GetComponent<Image>();
                    squareImage.sprite = normalImage.sprite;//흰색으로 바꿔

                    GameObject contectSquare = hit.collider.gameObject.transform.gameObject; //부모도 받어
                    contectSquare.GetComponent<GridSquare>().ClearOccupied(); //스크립트에 선택안된옵션으로 바꿔
                    contectSquare.GetComponent<GridSquare>().Deactivate();
                    buttonDown = false;
                }
            }
        }

        if(buttonDown == true)
        {
            hooverObj.SetActive(true);
            hooverObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = " ";
        }
        else
        {
            if (GridScript.EraserItemTurn == ItemTurn)
            {
                hooverObj.SetActive(true);
            }
            else if(GridScript.EraserItemTurn == 0)
            {
                hooverObj.SetActive(false);
            }
        }
    }

    public void NormalBtnOnClick()
    {
        buttonDown = true;      
    }
}
