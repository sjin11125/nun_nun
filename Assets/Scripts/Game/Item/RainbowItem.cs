using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowItem : MonoBehaviour
{
    public GameObject[] myChlid = new GameObject[2];
    GameObject shapestorageObj;
    public Sprite[] getShpes;// = new Sprite[6];

    void Start()
    {
        for (int i = 0; i < myChlid.Length; i++)
        {
            myChlid[i] = gameObject.transform.GetChild(i).gameObject;
        }
        shapestorageObj = GameObject.FindGameObjectWithTag("ShapeStorage");
    }

    public void RainbowItemUse(string color)
    {
        int j = 0;
        for (int i = 0; i < 36; i++)
        {
            if (shapestorageObj.GetComponent<ShapeStorage>().shapeData[i].color == color)
            {
                getShpes[j] = shapestorageObj.GetComponent<ShapeStorage>().shapeData[i].sprite;
                j++;
            }
        }
    }

    public Sprite RainbowItemChange(int k)
    {
        if(k > 5)
        {
            k = 0;
        }
        return getShpes[k];
    }
    public void OnClick()//아이템 쓰고 난 뒤에 누르는 버튼
    {
        myChlid[1].SetActive(true);//블락 켜기
        GridScript.RainbowItemTurn = 4;
        for (int i = 0; i < getShpes.Length; i++)
        {
            getShpes[i] = null;
        }
    }

    void Update()
    {
        myChlid[1].transform.GetChild(0).gameObject.GetComponent<Text>().text = GridScript.RainbowItemTurn.ToString();//항상 숫자를 받는데
        
        if (GridScript.RainbowItemTurn <= 0)
        {
           myChlid[1].SetActive(false);//0이하면 사용가능해지게
        }
    }

    public void UseThisItem(GameObject useObj)
    {
        GameObject[] squares = GameObject.FindGameObjectsWithTag("GridSquare");
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i] != useObj)
            {
                squares[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    /*
    public void UseThisItemEx()//다시 켜는 함수
    {
        GameObject[] squares = GameObject.FindGameObjectsWithTag("GridSquare");
        for (int i = 0; i < squares.Length; i++)
        {
            squares[i].transform.GetChild(3).gameObject.SetActive(false);
        }
    }*/
}
