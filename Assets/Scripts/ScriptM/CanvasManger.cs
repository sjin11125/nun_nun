using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManger : MonoBehaviour
{
    //canvas에 텍스트랑 재화 연결해라
    public Text Money;          //재화
    public Text ShinMoney;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       /// Money.text = GameManager.Money.ToString();
        //ShinMoney.text = GameManager.ShinMoney.ToString();
    }
    
}
