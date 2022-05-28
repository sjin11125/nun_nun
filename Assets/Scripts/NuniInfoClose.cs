using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuniInfoClose : MonoBehaviour
{
    private GameObject settigPanel;
    public GameObject nuniInfo;

    private void Start()
    {
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }

    public void CloseOnClick()    //사운드 정보 받아서 켜져있으면 플레이 아니면 놉
    {
        if (GameManager.mainMusicOn)
        {
            settigPanel.GetComponent<AudioController>().Sound[0].Play();
            Destroy(nuniInfo);//누니인포패널 삭제
        }
    }
}
