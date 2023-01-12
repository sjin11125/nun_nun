using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class UIBase :MonoBehaviour
{
    //public Text UIPanelName;
    //public Text UIPanelText;
    public Button UICloseBtn;
    public Button UIYesBtn;
    public Button UINoBtn;

    public Button UIBackCloseBtn;           //배경 누르면 닫기

    public GameObject UIPrefab;

    public CanvasBase canvas;


   protected GameObject parent;

    public virtual void  Awake()
    {
        canvas = CanvasBase._Instance;

    }

    public GameObject InstantiatePrefab()
    {
        if (UIPrefab != null)
        {
             parent = canvas.CanvasCheck();

            return Instantiate(UIPrefab, parent.transform) ;
        }
        else
        {
            return null;
        }
           
    }

}
