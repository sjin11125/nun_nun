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

    bool isAndroid;
    bool isIos;

    void start()
    {
        //button1.onClick.AddListener(TaskOnClick);
      

    }
    void Awake()
    {
    }
    public void UpdateUrl()             //마켓으로 가는 버튼
    {
#if UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SoMa.NuNNuN");
#endif
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.identifier);
#endif
#if UNITY_IOS
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1629395506");
#endif

    }
    public void UpdateNo()
    {
        Application.Quit();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (temp.CanBePlaced())
        {
           // temp.Place(false);
        }
    }
    public void Buttonok()
    {
        Debug.Log("ButtonOK");
        if (temp.CanBePlaced())
        {
            //temp.Place(false);
        }

    }

     public void buttoncancel()
     {

        // temp2.ClearArea();
         Destroy(temp.gameObject);

     }


}
