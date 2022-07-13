using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    GameObject gridObj;
    int[] count = new int[12];

    public void AchieveStart()
    {
        gridObj = GameObject.FindGameObjectWithTag("Grid");
        for (int i = 0; i < count.Length; i++)
        {
            count[i] = CanvasManger.achieveCount[i];
            transform.GetChild(i).GetChild(1).GetComponent<Text>().text= count[i].ToString();
        }
    }

    public void QuestIndex()
    {
        GetColors("Pink", 0);
        GetColors("Sky", 1);
        GetColors("Orange", 2);
        GetColors("Mint", 3);
        GetColors("Yellow", 4);
        GetColors("Purple", 5);
        GetShapes("Heart", 6);
        GetShapes("Tri", 7);
        GetShapes("Star", 8);
        GetShapes("Square", 9);
        GetShapes("Six", 10);
        GetShapes("Dia", 11);
    }

    public void GetColors(string color, int questIndex)
    {
        for (int i = 0; i < 5; i++)
        {
            if (gridObj.GetComponent<GridScript>().colors[gridObj.GetComponent<GridScript>().completeIndexArray[i]] == color)
            {
                count[questIndex]++;           
            }
        }
        transform.GetChild(questIndex).GetChild(1).GetComponent<Text>().text = count[questIndex].ToString();
        int goalCount = CanvasManger.achieveContNuniIndex[questIndex];
        switch (goalCount)
        {
            case 0:
                if (count[questIndex] >= 20)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 1:
                if (count[questIndex] >= 100)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 2:
                if (count[questIndex] >= 200)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 3:
                if (count[questIndex] >= 500)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 4:
                if (count[questIndex] >= 2000)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            default:
                CanvasManger.currentAchieveSuccess[questIndex] = false;
                break;
        }
        CanvasManger.achieveCount[questIndex] = count[questIndex];
    }
    public void GetShapes(string shape, int questIndex)
    {
        for (int i = 0; i < 5; i++)
        {
            if (gridObj.GetComponent<GridScript>().shapes[gridObj.GetComponent<GridScript>().completeIndexArray[i]] == shape)
            {
                count[questIndex]++;             
            }
        }
        transform.GetChild(questIndex).GetChild(1).GetComponent<Text>().text = count[questIndex].ToString();
        int goalCount = CanvasManger.achieveContNuniIndex[questIndex];
        switch (goalCount)
        {
            case 0:
                if (count[questIndex] >= 20)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 1:
                if (count[questIndex] >= 100)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 2:
                if (count[questIndex] >= 200)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 3:
                if (count[questIndex] >= 500)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            case 4:
                if (count[questIndex] >= 2000)
                {
                    CanvasManger.currentAchieveSuccess[questIndex] = true;
                }
                break;
            default:
                CanvasManger.currentAchieveSuccess[questIndex] = false;
                break;
        }
        CanvasManger.achieveCount[questIndex] = count[questIndex];
    }
}
