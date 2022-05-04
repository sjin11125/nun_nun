using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSquare : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(255 / 255, 255 / 255, 255 / 255);
    }

    public void SetOccupied()
    {
        gameObject.GetComponent<Image>().color = new Color(255 / 255f, 146 / 255f, 146 / 255f);
    }

    public void UnSetOccupied()
    {
        gameObject.GetComponent<Image>().color = new Color(255 / 255, 255 / 255, 255 / 255);
    }
}
