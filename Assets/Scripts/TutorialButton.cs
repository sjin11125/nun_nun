using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    public static bool isTutoButton;
    static TutorialButton _Instance;
    bool isEnd = false;
    bool isIndex = false;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else if (_Instance != this) // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.


    }
    // Start is called before the first frame update
    void Start()
    {
        isTutoButton = true;


        GameManager.Money = 2000;
        GameManager.ProfileImage = GameManager.AllNuniArray[0].Image;
        SceneManager.LoadScene("Main");
        
        
    }
        // Update is called once per frame
        void Update()
    {
        if (TutorialsManager.itemIndex==14)
        {
            isEnd = true;
        }
        if (isEnd)
        {
            isEnd = false;
            StartCoroutine(WaitTutoEnd());
        }
    }
    IEnumerator WaitTutoEnd()
    { 
     yield return new WaitForSeconds(1f);
        if (Input.GetMouseButtonUp(0))
        {
            isTutoButton = false;
            GameManager.CharacterList.Clear();
            SceneManager.LoadScene("Login");
            Destroy(gameObject);
        }
 
    }
  
}
