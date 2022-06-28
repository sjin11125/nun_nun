using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyScript : MonoBehaviour
{
    public Text CancelText;
    public GameObject Water;
    public static bool isfirst;

    public void NuniBuy()
    {
        if (GameManager.Money < 2000)
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
            gameObject.SetActive(false);
        }
    }
}
