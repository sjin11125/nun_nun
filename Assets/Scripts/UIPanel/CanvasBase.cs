using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBase : MonoBehaviour
{
    public GameObject TopCanvas;
    public GameObject MiddleCanvas;
    public GameObject BottomCanvas;

    [SerializeField]
    public List<GameObject> UICanvas;
    public  GameObject CanvasCheck()
    {
        if (BottomCanvas == null)
        {
            BottomCanvas = Instantiate(UICanvas[0]) as GameObject;
            return BottomCanvas;
        }
        else 
        {
            if (BottomCanvas.activeSelf)           //Bottom Canvas가 활성화가 안되어있나
                return BottomCanvas;

            if (MiddleCanvas == null)
            {
                MiddleCanvas = Instantiate(UICanvas[1]) as GameObject;
                return MiddleCanvas;
            }
            else
            {
                if (MiddleCanvas.activeSelf)
                    return MiddleCanvas;
                

                if (TopCanvas = null)
                {
                    TopCanvas = Instantiate(UICanvas[2]) as GameObject;
                    return TopCanvas;
                }
                else
                   if (TopCanvas.activeSelf)
                    return TopCanvas;
            }
        }

        return null;
    }

}
