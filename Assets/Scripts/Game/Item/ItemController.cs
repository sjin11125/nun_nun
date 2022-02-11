using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject eraserItem, nextSquareItem, nextResetItem, NextExchangeItem, RainbowItem, ChangeShapeItem, ThreeVerticalItem, ThreeHorizontalTtem;
    public int keepItemIndex, trashCanItemIndex;

    public bool[] mainItemBool = new bool[10];

    void Awake()
    {      
        for (int i = 0; i < mainItemBool.Length; i++)
        {
            mainItemBool[i] = GameManager.Items[i];
        }
        
        if (mainItemBool[0] == true)
        {
            eraserItem.SetActive(true);
        }
        else
        {
            eraserItem.SetActive(true);//false
        }

        if (mainItemBool[1] == false)//true
        {
            keepItemIndex = 1 + 25;
        }
        else
        {
            keepItemIndex = 5 + 25;
        }

        if (mainItemBool[2] == false)//true
        {
            trashCanItemIndex = 2 + 25;
        }
        else
        {
            trashCanItemIndex = 5 + 25;
        }

        if (mainItemBool[3] == true)
        {
            nextSquareItem.SetActive(false);
        }
        else
        {
            nextSquareItem.SetActive(false);//true
        }

        if (mainItemBool[4] == true)
        {
            nextResetItem.SetActive(true);
        }
        else
        {
            nextResetItem.SetActive(true);//false
        }
        if (mainItemBool[5] == true)
        {
            NextExchangeItem.SetActive(true);
        }
        else
        {
            NextExchangeItem.SetActive(true);//false
        }
        if (mainItemBool[6] == true)
        {
            RainbowItem.SetActive(true);
        }
        else
        {
            RainbowItem.SetActive(true);//false
        }
        if (mainItemBool[7] == true)
        {
            ChangeShapeItem.SetActive(true);
        }
        else
        {
            ChangeShapeItem.SetActive(true);//false
        }
        if (mainItemBool[8] == true)
        {
            ChangeShapeItem.SetActive(true);
        }
        else
        {
            ChangeShapeItem.SetActive(true);//false
        }
        if (mainItemBool[9] == true)
        {
            ChangeShapeItem.SetActive(true);
        }
        else
        {
            ChangeShapeItem.SetActive(true);//false
        }
    }
}
