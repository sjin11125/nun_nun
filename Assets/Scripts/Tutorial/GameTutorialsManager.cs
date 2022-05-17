using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialsManager : MonoBehaviour
{
    [SerializeField] [Header("Tutorials items")] TutorialsItemControl[] items;
    public int itemIndex;
    void Start()
    {
        if (RandomSelect.isTuto == 0)
        {
            // 모든 아이템을 비활성화 하고, 첫번째 것만 활성화 한다.
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
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ActiveNextItem()
    {
        if (items.Length == itemIndex)
        {
            this.gameObject.SetActive(false);
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
