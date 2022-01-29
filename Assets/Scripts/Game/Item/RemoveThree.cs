using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveThree : MonoBehaviour
{//세로
    GameObject[] myChlid = new GameObject[3];
    private Image squareImage;
    public Image normalImage;
    public int ItemTurn = 3;
    bool useRemove;
    bool centerhave;
    void Start()
    {
        for (int i = 0; i < myChlid.Length; i++)
        {
            myChlid[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);//광선을 쏴
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("GridSquare") && useRemove == true)//채워져있는애가 맞으면
                {
                    if(hit.collider.gameObject.transform.GetChild(2).gameObject.activeSelf == true)//선택한애가 무조건 켜져있어야 사용할거임
                    {
                        FindUpDown(hit.collider.gameObject);
                    }
                    if(centerhave == true)
                    {
                        GridScript.ThreeVerticalItem = ItemTurn;
                        myChlid[0].SetActive(false);
                        myChlid[1].SetActive(true);
                        myChlid[2].SetActive(true);
                        useRemove = false;
                    }
                }
                else if (hit.collider.gameObject == myChlid[1])
                {
                    myChlid[0].SetActive(true);
                    myChlid[1].SetActive(false);
                    useRemove = true;
                    centerhave = false;
                }
            }
        }

        myChlid[2].transform.GetChild(0).gameObject.GetComponent<Text>().text = GridScript.ThreeVerticalItem.ToString();//항상 숫자를 받는데
        if (GridScript.ThreeVerticalItem <= 0)
        {
            myChlid[2].SetActive(false);//0이하면 사용가능해지게
        }
    }
    
    void FindUpDown(GameObject center)
    {
        GameObject[] tempObj = new GameObject[25];
        for (int i = 0; i < 25; i++)
        {
           // tempObj[i] = center.GetComponentInParent<GridScript>().transform.GetChild(i).gameObject;//그리드 스크립트 자식들 모두 저장
            tempObj[i] = GameObject.FindGameObjectWithTag("Grid").transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < tempObj.Length; i++)
        {
            if (tempObj[i] == center)//걔가 지금 선택된 애랑 같은놈
            {
                clearSquare(tempObj[i]);
                if(i - 5 > 0 && i + 5 < 25)
                {
                    clearSquare(tempObj[i - 5]);
                    clearSquare(tempObj[i + 5]);
                }
                else
                {
                    if (i - 5 > 0)
                    {
                        clearSquare(tempObj[i - 5]);
                    }
                    else if (i + 5 < 25)
                    {
                        clearSquare(tempObj[i + 5]);
                    }
                }
                centerhave = true;
            }
        }
      
    }

    void clearSquare(GameObject square)
    {
        if(square.transform.GetChild(2).gameObject.activeSelf == true)
        {
            square.GetComponent<GridSquare>().ClearOccupied();
            square.GetComponent<GridSquare>().Deactivate();
            squareImage = square.transform.GetChild(2).gameObject.GetComponent<Image>();
            squareImage.sprite = normalImage.sprite;
        }
    }
}
