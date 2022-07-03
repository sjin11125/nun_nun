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
    public Text number;

    private GameObject settigPanel;

    public void StartAndReStart()
    {
        normalObj.SetActive(true);
        hooverObj.SetActive(true);
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }

    void Update()
    {
        number.text = GridScript.EraserItemTurn.ToString();
        
        if (Input.GetMouseButtonDown(0) && buttonDown == true)// && hooverObj.activeSelf == false) //좌클할때&&버튼 눌려있을때
        {
            GridScript.EraserItemTurn = 10;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);//광선을 쏴
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("GridSquare"))//스퀘어가 선택됐냐
                {
                    GameObject contectSquare = hit.collider.gameObject.transform.gameObject; //부모 받어
                    contectSquare.GetComponent<GridSquare>().ClearOccupied(); //스크립트에 선택안된옵션으로 바꿔
                    contectSquare.GetComponent<GridSquare>().Deactivate();
                    squareImage = contectSquare.transform.GetChild(2).gameObject.GetComponent<Image>();
                    squareImage.sprite = normalImage.sprite;//흰색으로 바꿔
                    buttonDown = false;
                    settigPanel.GetComponent<AudioController>().Sound[0].Play();
                    GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>().CheckIfKeepLineIsCompleted();
                }
            }
        }

        if(buttonDown == true)
        {
            hooverObj.SetActive(true);
            number.text = " ";
        }
        else
        {
            if (GridScript.EraserItemTurn == 10)
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
