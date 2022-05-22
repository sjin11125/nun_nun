using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialsManager : MonoBehaviour
{
    [SerializeField] [Header("Tutorials items")] TutorialsItemControl[] items;
    public int itemIndex;
    public bool isItem;

    void Start()
    {
        if (!isItem)
        {
            if(RandomSelect.isTuto == 0)
            {
                ItemHooverOnClick();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ItemHooverOnClick()
    {
        if (items == null)
            return;

        if (items.Length == 0)
            return;

        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }
        ActiveNextItem();
    }

    public void ActiveNextItem()
    {
        if (items.Length == itemIndex)
        {
            this.gameObject.SetActive(false);
            if (isItem)
            {
                itemIndex = 0;
            }
        }
        else
        {
            if (itemIndex - 1 > -1 && itemIndex - 1 < items.Length)
            {
                items[itemIndex - 1].gameObject.SetActive(false);// 전 아이템 비활성화
            }

            if (itemIndex > -1 && itemIndex < items.Length)
            {
                items[itemIndex].gameObject.SetActive(true);// 아이템 활성화
            }
            itemIndex++;
        }
    }
}
