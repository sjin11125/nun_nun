using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShapeItem : MonoBehaviour
{
    public GameObject[] myChlid = new GameObject[2];
    GameObject shapestorageObj;
    public Sprite[] getShapes;// = new Sprite[6];
    public string[] getShpesName;
    public static GameObject squareObj;
    public int colorK = 0;
    public static bool changeActive;
    GameObject rainbowObj;
    public int ItemTurn;

    void Start()
    {
        for (int i = 0; i < myChlid.Length; i++)
        {
            myChlid[i] = gameObject.transform.GetChild(i).gameObject;
        }
        shapestorageObj = GameObject.FindGameObjectWithTag("ShapeStorage");

        myChlid[0].transform.GetChild(0).transform.gameObject.SetActive(true);
        myChlid[0].transform.GetChild(1).transform.gameObject.SetActive(false);
        myChlid[0].SetActive(false);

        GameObject GetRainbow = GameObject.FindGameObjectWithTag("ItemController");//컨트롤러 다섯번째 자식인
        if (GetRainbow != null)
        {
            rainbowObj = GetRainbow.transform.GetChild(5).gameObject;//레인보우 아이템 오브젝트를 받아
        }
    }

    public void RainbowItemUse(string color)
    {
        myChlid[0].transform.GetChild(0).transform.gameObject.SetActive(false);
        myChlid[0].transform.GetChild(1).transform.gameObject.SetActive(true);
        int j = 0;
        for (int i = 0; i < 36; i++)
        {
            if (shapestorageObj.GetComponent<ShapeStorage>().shapeData[i].color == color)
            {
                getShapes[j] = shapestorageObj.GetComponent<ShapeStorage>().shapeData[i].sprite;
                getShpesName[j] = shapestorageObj.GetComponent<ShapeStorage>().shapeData[i].shape;
                j++;
            }
        }
    }

    public Sprite RainbowItemChange(int k)
    {
        if (k > 5)
        {
            k = 0;
            colorK = k;
        }
        return getShapes[k];
    }

    void Update()
    {
        myChlid[2].transform.GetChild(0).gameObject.GetComponent<Text>().text = GridScript.ChangeShapeItem.ToString();//항상 숫자를 받는데

        if (GridScript.ChangeShapeItem <= 0)
        {
            myChlid[2].SetActive(false);//0이하면 사용가능해지게
        }

        if (Input.GetMouseButtonDown(0))//다음 모양으로
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("GridSquare") && changeActive)//스퀘어가 선택됐긴한데
                {
                    if (hit.collider.gameObject == squareObj)
                    {
                        squareObj.transform.GetChild(2).GetComponent<Image>().sprite = RainbowItemChange(colorK);
                        squareObj.GetComponent<GridSquare>().currentShape = getShpesName[colorK];//색깔전달
                        colorK++;
                    }
                    else//다른 스퀘어면
                    {
                        if (squareObj != null)//그전에 롱클릭없었을시
                        {
                            changeActive = false;
                        }
                    }
                }
                else if (hit.collider.gameObject == myChlid[0])//사용완료
                {
                    GridScript.ChangeShapeItem = ItemTurn;
                    myChlid[2].SetActive(true);
                    myChlid[1].SetActive(true);
                    myChlid[0].transform.GetChild(0).transform.gameObject.SetActive(true);
                    myChlid[0].transform.GetChild(1).transform.gameObject.SetActive(false);
                    myChlid[0].SetActive(false);
                    GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>().CheckIfKeepLineIsCompleted();
                    squareObj = null;
                    changeActive = false;
                    if (rainbowObj != null)
                    {
                        rainbowObj.SetActive(true);
                    }
                }
                else if (hit.collider.gameObject == myChlid[1])//사용
                {
                    myChlid[0].SetActive(true);
                    myChlid[1].SetActive(false);
                    changeActive = true;
                    //얘를 사용누르면 컬러아이템은 꺼져야됨
                    if (rainbowObj != null)
                    {
                        rainbowObj.SetActive(false);
                    }
                }
            }
        }
    }
}
