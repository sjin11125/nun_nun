using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject eraserItem, nextSquareItem, nextResetItem;
    public int keepItemIndex, trashCanItemIndex;

    public bool[] mainItemBool = new bool[5];

    void Awake()
    {
       
        for (int i = 0; i < 5; i++)
        {
            mainItemBool[i] = GameManager.Items[i];
        }
        
        if (mainItemBool[0] == true)
        {
            eraserItem.SetActive(true);
        }
        else
        {
            eraserItem.SetActive(false);
        }

        if (mainItemBool[1] == true)
        {
            keepItemIndex = 1 + 25;
        }
        else
        {
            keepItemIndex = 5 + 25;
        }

        if (mainItemBool[2] == true)
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
            nextSquareItem.SetActive(true);
        }

        if (mainItemBool[4] == true)
        {
            nextResetItem.SetActive(true);
        }
        else
        {
            nextResetItem.SetActive(false);
        }
    }
}
