using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoard : MonoBehaviour
{
    TouchScreenKeyboard id;
    string saveID;//여기에 저장


    public void OpenIDKeyBoard()
    {
       id= TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
    }


    public void Update()
    {
        if (TouchScreenKeyboard.visible == false && id != null)
        {
            if (id.done)//엔터 눌렀어
            {
                saveID = id.text;
                id = null;
            }
        }
    }
}
