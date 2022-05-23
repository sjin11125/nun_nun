using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyScript : MonoBehaviour
{
    public Text CancelText;
    public GameObject Water;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NuniBuy()
    {
        if (GameManager.Money < 2000)
        {
            CancelText.gameObject.SetActive(true);
            return;
        }
        else
        {
            Water.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
}
