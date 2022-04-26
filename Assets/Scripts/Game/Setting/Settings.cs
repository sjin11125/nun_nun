using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingPanel;
    private Animation anim;

    void Start()
    {
        settingPanel.SetActive(false);
        anim = GetComponent<Animation>();
    }

    public void SettingOnClick()
    {   
        if (settingPanel.activeSelf == false)//켜져있지않음
        {
            settingPanel.SetActive(true);
            anim.Play("SettingBtn");
            settingPanel.GetComponent<Animation>().Play("SettingPanel");
        }
        else//켜져있음
        {
            anim.Play("SettingBtnClose");
            settingPanel.GetComponent<Animation>().Play("SettingPanelClose");
        }
    }
}
