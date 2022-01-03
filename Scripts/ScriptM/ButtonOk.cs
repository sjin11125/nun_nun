using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOk : MonoBehaviour
{
    // 버튼 관리하는 곳 누르면 temp.Place() 되게 한다. 
    //public Button ok;
    public Building temp;
    // private GridBuildingSystem temp2;


    public Button button1;

    void start()
    {
        //button1.onClick.AddListener(TaskOnClick);
      

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (temp.CanBePlaced())
        {
            temp.Place();
        }
    }
    public void Buttonok()
    {
        Debug.Log("ButtonOK");
        if (temp.CanBePlaced())
        {
            temp.Place();
        }

    }

     public void buttoncancel()
     {

        // temp2.ClearArea();
         Destroy(temp.gameObject);

     }
    
    public void Coin_ButtonOK()
    {
        Debug.Log("cococoin");
        temp.Coin_OK();
    }

}
