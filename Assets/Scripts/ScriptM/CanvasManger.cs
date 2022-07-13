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
    public static int[] achieveCount = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0,0,0,0,0};

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
        ZemMoney.text =  GameManager.Zem.ToString();
    }
}
