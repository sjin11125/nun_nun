using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuItem : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector] public Image img;
    [HideInInspector] public Transform trans;



    void Awake()
    {
        img = GetComponent<Image>();
        trans = transform;



    }
}