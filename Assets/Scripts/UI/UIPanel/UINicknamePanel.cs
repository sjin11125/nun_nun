using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class UINicknamePanel : UIBase
{
    // Start is called before the first frame update
    public Action Callback;
    public Button OkBtn;
    public UINicknamePanel(GameObject UIPrefab)
    {
        UINicknamePanel r = UIPrefab.GetComponent<UINicknamePanel>();
        r.Awake();
        r.UIPrefab = UIPrefab;

        this.UIPrefab = r.InstantiatePrefab() as GameObject;
    }
  public override  void Start()
    {
        base.Start();
        OkBtn.OnClickAsObservable().Subscribe(_=> {

            //닉넴 체크(몇글자 이하인지)

            Callback.Invoke();//콜백 실행
        });
    }
}
