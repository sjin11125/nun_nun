using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveController : MonoBehaviour
{
    public GameObject[] content;

    void Start()
    {
    }

    public void GetPinkButton(int index)
    {
        content[index].GetComponent<AchieveContent>().getBtn.enabled = false;
        content[index].GetComponent<AchieveContent>().con_text.text = "ºÐÈ«»ö ¾óÀ½ 100°³ ±ú±â";
    }
}
