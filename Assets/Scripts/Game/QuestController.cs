using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public bool[] QuestActive = new bool[12];//이건 나중에 메인에서 켜기
    GameObject gridObj;
    int Count;
    int questIndex;
    Image timerBar;
    public Text number;

    void Start()
    {
        gridObj = GameObject.FindGameObjectWithTag("Grid");
        Count = 0;
        timerBar = gameObject.transform.GetChild(3).gameObject.GetComponent<Image>();
        questIndex = -1;
        for (int i = 0; i < QuestActive.Length; i++)
        {
            if (QuestActive[i] == true)//현재 트루인 퀘스트의 인덱스 저장
            {
                questIndex = i;
            }
        }
        if(questIndex == -1)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        timerBar.fillAmount = Mathf.Lerp(timerBar.fillAmount, (float)Count * 0.1f, Time.deltaTime);
    }

    public void QuestIndex()
    {
        switch (questIndex)
        {
            case 0: GetColors("Pink", 0);
                break;
            case 1: GetColors("Sky", 1);
                break;
            case 2: GetColors("Orange", 2);
                break;
            case 3: GetColors("Mint", 3);
                break;
            case 4: GetColors("Yellow", 4);
                break;
            case 5: GetColors("Purple", 5);
                break;
            case 6: GetShapes("Heart", 6);
                break;
            case 7: GetShapes("Tri", 7);
                break;
            case 8: GetShapes("Star", 8);
                break;
            case 9: GetShapes("Square", 9);
                break;
            case 10:GetShapes("Six", 10);
                break;
            case 11: GetShapes("Dia", 11);
                break;
        }
    }

    public void GetColors(string color, int questIndex)
    {
        for (int i = 0; i < 5; i++)
        {
            if (gridObj.GetComponent<GridScript>().colors[gridObj.GetComponent<GridScript>().completeIndexArray[i]] == color)
            {
                Count++;
                if (Count > 10)//10개 모으면 클리어
                {
                    Count = 10;
                    QuestActive[questIndex] = false;
                }
                number.text = Count.ToString();
            }
        }
    }
    public void GetShapes(string shape, int questIndex)
    {
        for (int i = 0; i < 5; i++)
        {
            if (gridObj.GetComponent<GridScript>().shapes[gridObj.GetComponent<GridScript>().completeIndexArray[i]] == shape)
            {
                Count++;
                if (Count > 10)//10개 모으면 클리어
                {
                    Count = 10;
                    QuestActive[questIndex] = false;
                }
                number.text = Count.ToString();
            }
        }
    }
}
