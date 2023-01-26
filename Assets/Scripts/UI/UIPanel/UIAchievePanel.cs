using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft;
using UniRx;

public class UIAchievePanel : UIBase
{
    // Start is called before the first frame update
    [SerializeField]
    public List<AchieveMenu> AchieveMenus;

    public GameObject Content;
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
        foreach (var item in AchieveMenus)
        {
            item.Btn.OnClickAsObservable().Subscribe(_=> {
                Exit();
                switch (item.Type)
                {
                    case AchieveMenuType.Color:
                        foreach (var info in GameManager.Instance.AchieveInfos)
                        {
                            if (info.Value.Id[0] != 'C')
                                continue;

                            GameObject AchieveInfoObj = Instantiate(item.Prefab, Content.transform) as GameObject;
                            AchieveScroll AchieveInfo = AchieveInfoObj.GetComponent<AchieveScroll>();



                            AchieveInfo.SetData(info.Value);
                        }
                      
                        break;

                    case AchieveMenuType.Ect:
                        foreach (var info in GameManager.Instance.AchieveInfos)
                        {
                            if (info.Value.Id[0] != 'E')
                                continue;

                            GameObject AchieveInfoObj = Instantiate(item.Prefab, Content.transform) as GameObject;
                            AchieveScroll AchieveInfo = AchieveInfoObj.GetComponent<AchieveScroll>();

                            AchieveInfo.SetData(info.Value);
                        }
                        break;

                    case AchieveMenuType.Shape:
                        foreach (var info in GameManager.Instance.AchieveInfos)
                        {
                            if (info.Value.Id[0] != 'S')
                                continue;

                            GameObject AchieveInfoObj = Instantiate(item.Prefab, Content.transform) as GameObject;
                            AchieveScroll AchieveInfo = AchieveInfoObj.GetComponent<AchieveScroll>();

                            AchieveInfo.SetData(info.Value);
                        }
                        break;

                    default:
                        break;
                }

            }).AddTo(this);
        }
       //Newtonsoft.Json.dese
    }
    public void Exit()
    {
        Transform[] Content_Child = Content.GetComponentsInChildren<Transform>();
        for (int i = 1; i < Content_Child.Length; i++)
        {
            Destroy(Content_Child[i].gameObject);
        }
    }
    //Json.Linq.JObject

}
