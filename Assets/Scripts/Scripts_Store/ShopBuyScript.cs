using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyScript : MonoBehaviour
{
    public Text CancelText;
    public GameObject Water;
    public static bool isfirst;
    public static int Achieve12;

    public void NuniBuy()
    {
        if (int.Parse(GameManager.Instance.PlayerUserInfo.Money) < 2000)
        {
            CancelText.gameObject.SetActive(true);
            return;
        }
        else
        {
            if (!isfirst)
            {
                Destroy(GameObject.FindGameObjectWithTag("Card").gameObject);
            }
            Water.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Achieve12++;
            int goalCount = CanvasManger.achieveContNuniIndex[12];
            switch (goalCount)
            {
                case 0:
                    if (Achieve12 >= 5)
                    {
                        CanvasManger.currentAchieveSuccess[12] = true;
                    }
                    break;
                case 1:
                    if (Achieve12 >= 20)
                    {
                        CanvasManger.currentAchieveSuccess[12] = true;
                    }
                    break;
                case 2:
                    if (Achieve12 >= 50)
                    {
                        CanvasManger.currentAchieveSuccess[12] = true;
                    }
                    break;
                case 3:
                    if (Achieve12 >= 120)
                    {
                        CanvasManger.currentAchieveSuccess[12] = true;
                    }
                    break;
                case 4:
                    if (Achieve12 >= 250)
                    {
                        CanvasManger.currentAchieveSuccess[12] = true;
                    }
                    break;
                default:
                    CanvasManger.currentAchieveSuccess[12] = false;
                    break;
            }
        }
    }
}
