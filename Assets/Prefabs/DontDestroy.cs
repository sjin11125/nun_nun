using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class DontDestroy : MonoBehaviour
{
    public static bool isStart = false;
    static DontDestroy _Instance;
    static Transform[] trans;

   public static TileBase[] Area;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            isStart = true;
        }
        else if (_Instance != this) // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.

    }
    // Update is called once per frame
    void Update()
    {
        trans = gameObject.GetComponentsInChildren<Transform>();

        if (trans!=null)
        {
            
        }
        
        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        
        else if (SceneManager.GetActiveScene().name == "Main")
        {

            if (transform.childCount != 0) 
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            
        }
    }
    
}
