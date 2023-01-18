using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UiLoadingPanel : UIBase
{
    // Start is called before the first frame update
    public UiLoadingPanel(GameObject UIPrefab)
    {
        UiLoadingPanel r = UIPrefab.GetComponent<UiLoadingPanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;

        this.UIPrefab = r.InstantiatePrefab() as GameObject;
    }
   public  void Start()
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

    public void DestroyGameObject()
    {
        UIPrefab.transform.parent.gameObject.SetActive(false);
        Destroy(UIPrefab);
    }
}
