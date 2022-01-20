using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextExchangeItem : MonoBehaviour
{
    public GameObject[] myChlid = new GameObject[3];
    GameObject shapestorageObj;
    public int ItemTurn = 15;
    public Sprite getBlue;
    string shapeStorageName;
    void Start()
    {
        for (int i = 0; i < myChlid.Length; i++)
        {
            myChlid[i] = gameObject.transform.GetChild(i).gameObject;
        }
        shapestorageObj = GameObject.FindGameObjectWithTag("ShapeStorage");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if(hit.collider != null)
            {
                if (hit.collider.gameObject == myChlid[1].gameObject)
                {
                    if (GridScript.NextExchangeItemTurn <= 0)
                    {
                        shapestorageObj.GetComponent<ShapeStorage>().nextExchangeItem();
                        GridScript.NextExchangeItemTurn = ItemTurn;
                        myChlid[1].gameObject.GetComponent<Image>().sprite = getBlue;
                        myChlid[0].SetActive(true);//��ƼŬ �ѱ�
                        myChlid[2].SetActive(true);//���� �̹��� �ѱ�
                    }
                }
            }             
        }
        myChlid[2].transform.GetChild(0).gameObject.GetComponent<Text>().text = GridScript.NextExchangeItemTurn.ToString();//�׻� ���ڸ� �޴µ�
        if (GridScript.NextExchangeItemTurn <= 0)
        {
            myChlid[2].SetActive(false);//0���ϸ� ��밡��������
        }
    }
}