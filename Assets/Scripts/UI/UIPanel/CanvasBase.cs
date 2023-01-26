using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBase : MonoBehaviour
{
    public static CanvasBase _Instance;

    public GameObject TopCanvas;
    public GameObject MiddleCanvas;
    public GameObject BottomCanvas;


    [SerializeField]
    public List<GameObject> UICanvas;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (_Instance == null)
        {
            _Instance = this;
        }
        else if (_Instance != this) // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);  // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.

    }
    public  GameObject CanvasCheck()
    {
        if (BottomCanvas == null)
        {
            BottomCanvas = Instantiate(UICanvas[0]) as GameObject;
            return BottomCanvas;
        }
        else
        {
            if (!BottomCanvas.activeSelf)           //Bottom Canvas가 활성화가 안되어있나
            {
                BottomCanvas.SetActive(true);
                return BottomCanvas;
            }


            if (MiddleCanvas == null)
            {
                MiddleCanvas = Instantiate(UICanvas[1]) as GameObject;
                return MiddleCanvas;
            }
            else
            {
                if (!MiddleCanvas.activeSelf)
                {
                    MiddleCanvas.SetActive(true);
                    return MiddleCanvas;

                }
                if (TopCanvas = null)
                {
                    TopCanvas = Instantiate(UICanvas[2]) as GameObject;
                    return TopCanvas;
                }
                else
                   if (!TopCanvas.activeSelf)
                {
                    TopCanvas.SetActive(true);
                    return TopCanvas;
                }

            }
        }

        return null;
    }

}
