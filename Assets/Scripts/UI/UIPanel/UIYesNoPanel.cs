using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIYesNoPanel : UIBase
{
    // Start is called before the first frame update
    
    public UIYesNoPanel(GameObject UIPrefab)
    {
        UIYesNoPanel r = UIPrefab.GetComponent<UIYesNoPanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;

        this.UIPrefab= r.InstantiatePrefab() as GameObject;
    }
    void Start()
    {
        
        if (UINoBtn != null)
        {

            UINoBtn.onClick.AsObservable().Subscribe(_ =>
            {
                DestroyGameObject(); 

            }).AddTo(this);
        }
        if (UICloseBtn != null)
        {

            UICloseBtn.onClick.AsObservable().Subscribe(_ =>
            {
                DestroyGameObject();
            }).AddTo(this);
        }
    }
    public void DestroyGameObject()
    {
        UIPrefab.transform.parent.gameObject.SetActive(false);
        Destroy(UIPrefab);
    }
}
