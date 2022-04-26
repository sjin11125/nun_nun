using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoard : MonoBehaviour
{
    TouchScreenKeyboard id;
    string saveID;//여기에 저장

    TouchScreenKeyboard password;
    string savePassword;//여기에 저장

    public void OpenIDKeyBoard()
    {
       id= TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
    }
    public void OpenPasswordKeyBoard()
    {
        password = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
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

        if (TouchScreenKeyboard.visible == false && password != null)
        {
            if (password.done)//엔터 눌렀어
            {
                savePassword = password.text;
                password = null;
            }
        }
    }
}
