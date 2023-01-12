using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UiLoadingPanel : UIBase
{
    // Start is called before the first frame update
    public UiLoadingPanel(GameObject UIPrefab)
    {
        UIYesNoPanel r = UIPrefab.GetComponent<UIYesNoPanel>();
        r.Start();
        r.UIPrefab = UIPrefab;

        r.InstantiatePrefab();
    }
   public override void Start()
    {
        base.Start();
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

 
}
