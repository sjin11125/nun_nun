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
    int shapeCount;

    public void ContentStart(int saveMyIndex,int saveMynuniIndex, int saveCount)
    {
        myContIndex = saveMyIndex;
        nuniIndex = saveMynuniIndex;
        shapeCount = saveCount;
        for (int i = 0; i < Nuni.Length; i++)
        {
            if (i < nuniIndex)
            {
                Nuni[i].transform.GetChild(1).gameObject.SetActive(true);//완료이미지
            }
        }
        int_text.text = Nuni[nuniIndex].GetComponent<ContentNuni>().int_text.ToString();

        if (!CanvasManger.currentAchieveSuccess[myContIndex])
        {
            getBtn.enabled = false;
        }
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel"); 
    }

    public void GetButton()
    {
        Nuni[nuniIndex].transform.GetChild(1).gameObject.SetActive(true);//완료이미지
        GameManager.Money += Nuni[nuniIndex].GetComponent<ContentNuni>().get_money;
        CanvasManger.AchieveMoney += Nuni[nuniIndex].GetComponent<ContentNuni>().get_money;
        GameManager.ShinMoney += Nuni[nuniIndex].GetComponent<ContentNuni>().get_shin;
        CanvasManger.AchieveShinMoney += Nuni[nuniIndex].GetComponent<ContentNuni>().get_shin;
        GameManager.Zem += Nuni[nuniIndex].GetComponent<ContentNuni>().get_zem;
        nuniIndex++;
        if (nuniIndex < Nuni.Length)
        {
            int_text.text = Nuni[nuniIndex].GetComponent<ContentNuni>().int_text.ToString();
            if (shapeCount < Nuni[nuniIndex].GetComponent<ContentNuni>().int_text)
            {
                getBtn.enabled = false;
                CanvasManger.currentAchieveSuccess[myContIndex] = false;
                settigPanel.GetComponent<AudioController>().Sound[1].Play();
            }
            CanvasManger.achieveContNuniIndex[myContIndex] = nuniIndex;
        }
        else
        {
            getBtn.enabled = false;
            CanvasManger.currentAchieveSuccess[myContIndex] = false;
        }
        for (int i = 0; i < CanvasManger.currentAchieveSuccess.Length; i++)
        {
            Debug.Log("CanvasManger.currentAchieveSuccess[" + i + "] : " + CanvasManger.currentAchieveSuccess[i]);
        }
        GameManager.isBScore = true;
    }
}
