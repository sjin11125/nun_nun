using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEnable : MonoBehaviour
{
    public GameObject[] btnObj;

    public void ButtonEnableFalse()
    {
        for (int i = 0; i < btnObj.Length; i++)
        {
            btnObj[i].GetComponent<Button>().enabled = false;
        }
    }

    public void ButtonEnableTrue()//È°¼ºÈ­
    {
        for (int i = 0; i < btnObj.Length; i++)
        {
            btnObj[i].GetComponent<Button>().enabled = true;
        }
    }
}
