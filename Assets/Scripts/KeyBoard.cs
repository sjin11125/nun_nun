using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoard : MonoBehaviour
{
    TouchScreenKeyboard keyboard;

    public void OpenKeyBoard()
    {
        keyboard = TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
    }

    public void Update()
    {
        if (TouchScreenKeyboard.visible)
        {
            if (keyboard.done)//¿£ÅÍ ´­·¶¾î
            {
                keyboard = null;//²ô±â
            }
        }
    }
}
