using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManger : MonoBehaviour
{
    //canvas에 텍스트랑 재화 연결해라
    public Text Money;          //재화
    public Text Tree;          //재화
    public Text Snow;          //재화
    public Text Ice;          //재화
    public Text Grass;          //재화
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Money.text = "재화: "+GameManager.Money.ToString();
        Tree.text = GameManager.Tree.ToString();
        Snow.text = GameManager.Snow.ToString();
        Ice.text = GameManager.Ice.ToString();
        Grass.text = GameManager.Grass.ToString();
    }
}
