using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingPanel;
    private Animation anim;
    bool animac;
    public GameObject ExitPanel;

    void Start()
    {
        anim = GetComponent<Animation>();
        //Time.timeScale = 1;
        animac = false;
    }

    public void SettingOnClick()
    {
        if (animac == false)//켜져있지않음
        {
            anim.Play("SettingBtn");
            settingPanel.GetComponent<Animator>().SetTrigger("PanelOn");
            animac = true;
        }
        else//켜져있음
        {
            anim.Play("SettingBtnClose");
            settingPanel.GetComponent<Animator>().SetTrigger("PanelOff");
            animac = false;
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {
                ExitPanel.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                ExitPanel.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.Menu))
            {
                ExitPanel.SetActive(true);
            }
        }
    }
}
