using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInfo : MonoBehaviour
{
    public static string Id;            //플레이어 아이디
    public static string NickName;      //플레이어 닉네임
    public static string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public static string Info;          //상태메세지

    string[] Friends;       //친구 목록(닉네임)
    // Start is called before the first frame update
    void Start()
    {
        Friends = new string[1];
        Friends[1] = "Vicky";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
