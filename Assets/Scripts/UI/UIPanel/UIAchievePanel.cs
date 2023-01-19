using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAchievePanel : UIBase
{
    // Start is called before the first frame update
    public UIAchievePanel(GameObject UIPrefab)
    {
        UIAchievePanel r = UIPrefab.GetComponent<UIAchievePanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;

        r.InstantiatePrefab();
    }
    override public void Start()
    {
        base.Start();


    }

}
