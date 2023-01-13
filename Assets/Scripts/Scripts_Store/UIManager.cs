using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text MoneyText;          //재화 UI 

    public static int isSetMoney = 0;       //결제되냐 (1:결제함 , 0:아무것도 아님 -1:거절함)

    public GameObject RefusePanel;
    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text = GameManager.Instance.PlayerUserInfo.Money;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetMoney == 1)            //결제
        {
            MoneyText.text = GameManager.Instance.PlayerUserInfo.Money;
            isSetMoney = 0;


        }
        else if(isSetMoney==-1)         //결제 거절
        {
            GameObject DogamCha = Instantiate(RefusePanel);
            DogamCha.transform.SetParent(GameObject.Find("Canvas").transform);
            DogamCha.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            isSetMoney = 0;
        }
    }
}
