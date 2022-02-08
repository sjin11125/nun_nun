using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public bool[] QuestActive = new bool[12];//�̰� ���߿� ���ο��� �ѱ�
    GameObject gridObj;
    int Count;
    int questIndex;

    void Start()
    {
        gridObj = GameObject.FindGameObjectWithTag("Grid");
        Count = 0;
        questIndex = -1;
        for (int i = 0; i < QuestActive.Length; i++)
        {
            if (QuestActive[i] == true)//���� Ʈ���� ����Ʈ�� �ε��� ����
            {
                questIndex = i;
            }
        }
        if(questIndex == -1)
        {
            gameObject.SetActive(false);
        }
    }

    public void QuestIndex()
    {
        switch (questIndex)
        {
            case 0: GetColors("Pink", 0);
                break;
            case 1: GetColors("Blue", 1);
                break;
            case 2: GetColors("LightBlue", 2);
                break;
            case 3: GetColors("Green", 3);
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
            case 10:GetShapes("8", 10);
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
                if (Count > 10)//10�� ������ Ŭ����
                {
                    Count = 10;
                    QuestActive[questIndex] = false;
                }
                gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = Count.ToString();                   
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
                if (Count > 10)//10�� ������ Ŭ����
                {
                    Count = 10;
                    QuestActive[questIndex] = false;
                }
                gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = Count.ToString();
            }
        }
    }
}