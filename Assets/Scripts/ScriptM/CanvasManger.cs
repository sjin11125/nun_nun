using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManger : MonoBehaviour
{
    //canvas에 텍스트랑 재화 연결해라
    public Text Money;          //재화
    public Text ShinMoney;
    public Text ZemMoney;
    public GameObject[] Achieves;

    public static int[] achieveContNuniIndex = new int[17] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public static bool[] currentAchieveSuccess = new bool[17] { false, false , false , false , false , false , false , false , false , false , false , false, false, false, false, false, false };
    public static int[] achieveCount = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0 };
    public static int AchieveMoney = 0;          
    public static int AchieveShinMoney = 0;
    public static int AchieveFriendCount = 0;

    private void Start()
    {
        for (int i = 0; i < Achieves.Length; i++)
        {
            Achieves[i].GetComponent<AchieveContent>().ContentStart(i, achieveContNuniIndex[i], achieveCount[i]);
        }
    }

    void Update()
    {
        Money.text = GameManager.Money.ToString();
        ShinMoney.text = GameManager.ShinMoney.ToString();
        ZemMoney.text = "보유 잼 : " + GameManager.Zem;
    }

    public void AchieveSetActive()
    {
        switch (achieveContNuniIndex[13])
        {
            case 0:
                if (AchieveMoney >= 1000)
                {
                    currentAchieveSuccess[13] = true;
                }
                break;
            case 1:
                if (AchieveMoney >= 5000)
                {
                    currentAchieveSuccess[13] = true;
                }
                break;
            case 2:
                if (AchieveMoney >= 20000)
                {
                    currentAchieveSuccess[13] = true;
                }
                break;
            case 3:
                if (AchieveMoney >= 100000)
                {
                    currentAchieveSuccess[13] = true;
                }
                break;
            case 4:
                if (AchieveMoney >= 500000)
                {
                    currentAchieveSuccess[13] = true;
                }
                break;
            default:
                currentAchieveSuccess[13] = false;
                break;
        }

        switch (achieveContNuniIndex[14])
        {
            case 0:
                if (AchieveShinMoney >= 5)
                {
                    currentAchieveSuccess[14] = true;
                }
                break;
            case 1:
                if (AchieveShinMoney >= 20)
                {
                    currentAchieveSuccess[14] = true;
                }
                break;
            case 2:
                if (AchieveShinMoney >= 50)
                {
                    currentAchieveSuccess[14] = true;
                }
                break;
            case 3:
                if (AchieveShinMoney >= 120)
                {
                    currentAchieveSuccess[14] = true;
                }
                break;
            case 4:
                if (AchieveShinMoney >= 250)
                {
                    currentAchieveSuccess[14] = true;
                }
                break;
            default:
                currentAchieveSuccess[14] = false;
                break;
        }
    }
}
