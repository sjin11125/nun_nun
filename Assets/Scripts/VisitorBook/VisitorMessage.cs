using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitorMessage : MonoBehaviour
{
    public Text Name,Message,Time;

    public Image Image;
 
    public void SetMessage(string name, string message,string time,string image)
    {
        Name.text = name;
        Message.text = message;
        Time.text = time;
        //이미지 설정
    }

}
