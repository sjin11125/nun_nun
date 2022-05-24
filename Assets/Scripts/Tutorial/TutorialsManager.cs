using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsManager : MonoBehaviour
{
    [SerializeField] [Header("Tutorials items")] TutorialsItemControl[] items;
    public static int itemIndex;
    GameObject bunsu;

    void Start()
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

    private void Update()
    {
        if (itemIndex < 4)
        {
            bunsu = GameObject.FindWithTag("bunsu"); //ii1y1
            if (bunsu != null)
            {
                bunsu.gameObject.SetActive(false);
            }
        }
        else
        {
            bunsu = GameObject.FindWithTag("bunsu"); //ii1y1
            if (bunsu != null)
            {
                bunsu.gameObject.SetActive(true);
            }
        }
    }

    // 다음 아이템을 활성화 한다.
    public void ActiveNextItem()
    {
        if (items.Length == itemIndex)
        {
            RandomSelect.isTuto = 1;
            PlayerPrefs.SetInt("TutorialsDone", itemIndex);
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

                if (itemIndex == 10)
                {
                    GameManager.Money += 100;
                    GameManager.ShinMoney += 1;
                }
            }
            itemIndex++;
        }
    }
}
