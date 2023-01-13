using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsManager : MonoBehaviour
{
    [SerializeField] [Header("Tutorials items")] TutorialsItemControl[] items;
    public static int itemIndex;
    public GameObject bunsu;
    
    void Start()
    {
        if (items==null)
            return;

        if (items.Length.Equals( 0))
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
            if (bunsu != null)
            {
                bunsu.gameObject.SetActive(false);
            }
        }
        else
        {
            if (bunsu != null)
            {
                bunsu.gameObject.SetActive(true);
            }
        }
    }

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

                if (itemIndex.Equals(1))
                {
                    bunsu = GameObject.FindWithTag("bunsu"); //ii1y1
                    if (bunsu != null)
                    {
                        bunsu.SetActive(false);
                    }
                }
                if (itemIndex.Equals(10))
                {
                
                    if (int.Parse(GameManager.Instance.PlayerUserInfo.Money) < 100 
                        || int.Parse(GameManager.Instance.PlayerUserInfo.ShinMoney) < 1)
                    {
                        int Money = int.Parse(GameManager.Instance.PlayerUserInfo.Money);
                        Money+= 100;
                        GameManager.Instance.PlayerUserInfo.Money = Money.ToString();

                        int ShinMoney = int.Parse(GameManager.Instance.PlayerUserInfo.ShinMoney);
                        ShinMoney +=1;
                        GameManager.Instance.PlayerUserInfo.Money = ShinMoney.ToString();

                    }
                }
            }
            PlayerPrefs.SetInt("TutorialsDone", itemIndex);
            itemIndex++;
        }
    }
}
