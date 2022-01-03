using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{

    private GameObject TimeText; // 게임오브젝트 생성
    public Text text;

    public GameObject Coin;
    //private PlayerStats PlayerStats;

    //private Text UIText;

    float currentTime = 0f;
    float startingTime = 100f; //초기시간

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        //TimeText = GameObject.Find("Canvas/TimeText"); //게임오브젝트 = 캔버스에 있는 TimeText로 설정

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        //text.text = currentTime.ToString("0.0");
        //TimeText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.30f, 1.4f, 0)); //Timer위치

        //이제 추가해야할 것은 건물을 눌렀을때 시간이 뜨도록 하기 (이거는 나중에)
        //건물이 생성되면 시간도 생성되어야 함
        // 시간이 흐르는 것이 계속 저장되도록 하기
        // current Time이 일정시간 밑으로 떨어졌을 때 수확 아이콘 생성
        if ( currentTime <= 95)
        {
            Coin.SetActive(true);
        }
        // 아이콘을 누르면 재화 + 
        
        // 재화를 누르면 current Time 초기화 or 0이 되면 current Time 초기화
    }
}
