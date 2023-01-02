using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBase : MonoBehaviour
{
    public GameObject TopCanvas;
    public GameObject MiddleCanvas;
    public GameObject BottomCanvas;


    public  GameObject CanvasCheck()
    {
        GameObject ReturnCanvas=new GameObject();
        if (BottomCanvas == null)
        {
            ReturnCanvas= Instantiate(BottomCanvas)as GameObject;
            return ReturnCanvas;
        }
        else 
        {
            if (!BottomCanvas.activeSelf)           //Bottom Canvas가 활성화가 안되어있나
                return BottomCanvas;

             if(MiddleCanvas==null)
                ReturnCanvas = Instantiate(MiddleCanvas) as GameObject;
            else
            {
                if (!MiddleCanvas.activeSelf)
                {
                    ReturnCanvas = MiddleCanvas;
                    return ReturnCanvas; 
                }

                if (TopCanvas=null)
                {
                    ReturnCanvas=Instantiate(TopCanvas) as GameObject;
                    return ReturnCanvas;
                }
                else
                   if (!TopCanvas.activeSelf)
                    return TopCanvas;
            }
        }
        return ReturnCanvas;
    }

}
