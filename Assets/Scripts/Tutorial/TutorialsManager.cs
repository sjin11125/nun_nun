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
        // ��� �������� ��Ȱ��ȭ �ϰ�, ù��° �͸� Ȱ��ȭ �Ѵ�.
        if (items==null)
            return;

        if (items.Length .Equals( 0))
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

    // ���� �������� Ȱ��ȭ �Ѵ�.
    public void ActiveNextItem()
    {
        if (items.Length .Equals( itemIndex))
        {
            RandomSelect.isTuto = 1;
            PlayerPrefs.SetInt("TutorialsDone", itemIndex);
        }
        else
        {
            if (itemIndex - 1 > -1 && itemIndex - 1 < items.Length)
            {
                items[itemIndex - 1].gameObject.SetActive(false);// �� ������ ��Ȱ��ȭ
            }
            if (itemIndex > -1 && itemIndex < items.Length)
            {
                items[itemIndex].gameObject.SetActive(true);// ������ Ȱ��ȭ

                if (itemIndex .Equals( 1))
                {
                    bunsu = GameObject.FindWithTag("bunsu"); //ii1y1
                    if (bunsu != null)
                    {
                        bunsu.SetActive(false);
                    }
                }
                if (itemIndex .Equals( 10))

                {
                    GameManager.Money += 100;
                    GameManager.ShinMoney += 1;
                }
            }
            itemIndex++;
        }
    }
}
