using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveContent : MonoBehaviour
{
    public Button getBtn;
    public Text int_text;
    public GameObject[] Nuni;
    private int myContIndex;
    private int nuniIndex;//CanvasManger와 연동
    private GameObject settigPanel;

    public void ContentStart(int saveMyIndex,int saveMynuniIndex)
    {
        myContIndex = saveMyIndex;
        nuniIndex = saveMynuniIndex;
        if (!CanvasManger.currentAchieveSuccess[myContIndex])
        {
            getBtn.enabled = false;
        }
        for (int i = 0; i < Nuni.Length; i++)
        {
            if (i < nuniIndex)
            {
                Nuni[i].transform.GetChild(1).gameObject.SetActive(true);//완료이미지
            }
        }
        int_text.text = Nuni[nuniIndex].GetComponent<ContentNuni>().int_text.ToString();
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }

    public void GetButton()
    {
        getBtn.enabled = false;
        Nuni[nuniIndex].transform.GetChild(1).gameObject.SetActive(true);//완료이미지
        GameManager.Money += Nuni[nuniIndex].GetComponent<ContentNuni>().get_money;
        GameManager.ShinMoney += Nuni[nuniIndex].GetComponent<ContentNuni>().get_shin;
        nuniIndex++;
        int_text.text = Nuni[nuniIndex].GetComponent<ContentNuni>().int_text.ToString();
        // GameManager.Zem += NunnComple[index].GetComponent<ContentNuni>().get_zem;
        //transform.parent.GetComponent<CanvasManger>().achieveContIndex[myContIndex] = nuniIndex;
        CanvasManger.achieveContNuniIndex[myContIndex] = nuniIndex;
        settigPanel.GetComponent<AudioController>().Sound[1].Play();
    }
}
