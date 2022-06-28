using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject eraserItem, nextSquareItem, nextResetItem, NextExchangeItem, RainbowItem, ChangeShapeItem, ThreeVerticalItem, ThreeHorizontalTtem;
    public int keepItemIndex, trashCanItemIndex;

    public bool[] mainItemBool = new bool[10];
    public GameObject keepshdow;
    public GameObject trashshdow;

    public GameObject startManagerObj;
    public GameObject GameoverObj;

    public static bool reStart;

    private void Awake()
    {
        /*
        for (int i = 0; i < GameManager.Items.Length; i++)
        {
            GameManager.Items[i] = false;
        }*/
        for (int i = 0; i < mainItemBool.Length; i++)
        {
            mainItemBool[i] = false;
        }
        SetItem();
        reStart = false;
    }
    public void ItemActive()//게임시작에서 넣기
    {      
        for (int i = 0; i < mainItemBool.Length; i++)
        {
            mainItemBool[i] = GameManager.Items[i];
        }
        SetItem();
        GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>().SettingKeep();
    }

    void SetItem()
    {
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
            keepItemIndex = 25;
            keepshdow.SetActive(true);
        }
        else
        {
            keepItemIndex = 30;
        }

        if (mainItemBool[2] == true)
        {
            trashCanItemIndex = 29;
            trashshdow.SetActive(true);
        }
        else
        {
            trashCanItemIndex = 30;
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
        if (mainItemBool[5] == true)
        {
            NextExchangeItem.SetActive(true);
        }
        else
        {
            NextExchangeItem.SetActive(false);
        }
        if (mainItemBool[6] == true)
        {
            RainbowItem.SetActive(true);
        }
        else
        {
            RainbowItem.SetActive(false);
        }
        if (mainItemBool[7] == true)
        {
            ChangeShapeItem.SetActive(true);
        }
        else
        {
            ChangeShapeItem.SetActive(false);
        }
        if (mainItemBool[8] == true)
        {
            ThreeVerticalItem.SetActive(true);
        }
        else
        {
            ThreeVerticalItem.SetActive(false);
        }
        if (mainItemBool[9] == true)
        {
            ThreeHorizontalTtem.SetActive(true);
        }
        else
        {
            ThreeHorizontalTtem.SetActive(false);
        }
    }

    public void GameoverReStart()
    {
        startManagerObj.transform.parent.gameObject.SetActive(true);
        GameoverObj.gameObject.SetActive(false);
        startManagerObj.GetComponent<StartManager>().CharacterOpen();
        for (int i = 0; i < mainItemBool.Length; i++)
        {
            mainItemBool[i] = false;
        }
        GridScript.EraserItemTurn = 10;
        GridScript.ReloadItemTurn = 15;
        GridScript.NextExchangeItemTurn = 15;
        GridScript.KeepItemTurn = 30;
        GridScript.TrashItemTurn = 20;
        GridScript.RainbowItemTurn = 40;
        GridScript.ChangeShapeItem = 40;
        GridScript.ThreeVerticalItem = 30;
        GridScript.ThreeHorizontalItem = 30;
        SetItem();
        reStart = true;
    }
}
