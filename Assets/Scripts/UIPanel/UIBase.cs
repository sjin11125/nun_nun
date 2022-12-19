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

    public GameObject UIPrefab;

    public Canvas canvas;

    public BuildUIType buildUItype;

    public virtual void  Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        transform.SetParent(canvas.transform);
    }

}
