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
    public virtual void Start()
    {
        if (UINoBtn != null)
        {

            UINoBtn.onClick.AsObservable().Subscribe(_ =>
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
                Destroy(this.gameObject);

            }).AddTo(this);
        }
        if (UICloseBtn != null)
        {

            UICloseBtn.onClick.AsObservable().Subscribe(_ =>
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }).AddTo(this);
        }
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
