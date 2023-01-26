using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProfilePanel : UIBase
{
    public UIProfilePanel(GameObject UIPrefab)
    {
        UIProfilePanel r = UIPrefab.GetComponent<UIProfilePanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;

        this.UIPrefab = r.InstantiatePrefab() as GameObject;
    }
    // Start is called before the first frame update
   override public void Start()
    {
        base.Start();


    }

}
