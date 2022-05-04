using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class OnPointerDownTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("누르고 있음");
        //StartCoroutine("PressCheck");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("뗌");
    }
}
