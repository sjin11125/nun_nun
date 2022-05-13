using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsItemControl : MonoBehaviour
{
    public enum ItemType
    {
        touch
    }

    [SerializeField] [Header("따라하기 아이템 종류")] ItemType itemType;
    [SerializeField] [Header("사용자 입력 대기까지 진행시간")] float timeToInput;
    [SerializeField] [Header("사용자 입력 대기시 표시할 게임오브젝트")] GameObject gameObjectToShow;

    bool isReadyToInput = false;


    private void Start()
    {

    }
    private void OnEnable()
    {

        Invoke("ShowGameObject", timeToInput);

    }
    // Update is called once per frame
    void Update()
    {
        // 입력대기 상태가 되면 터치를 입력 받는다.
        if (isReadyToInput)
        {
            if (itemType == ItemType.touch)
            {
                // 입력을 하면 계속 진행
                if (Input.GetMouseButtonDown(0))
                {
                   // RaycastHit2D hit;
                   // Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

                   // if (hit.collider != null)
                    //{
                     //   if(hit.collider.gameObject == gameObjectToShow)
                     //   {
                            Run();
                      //  }
                   // }                   
                }

            }

        }

    }

    virtual protected void Run()
    {
        if (gameObjectToShow == null)
            return;

        // 표시 item 비활성화 하고
        gameObjectToShow.SetActive(false);

        // 다음 아이템 활성화
        TutorialsManager parentTutorialsManager = parentTutorialsManager = transform.parent.GetComponent<TutorialsManager>();
        if (parentTutorialsManager != null)
        {
            parentTutorialsManager.ActiveNextItem();
        }

        Time.timeScale = 1.0f;
    }

    void ShowGameObject()
    {
        isReadyToInput = true;
        Time.timeScale = 0.0f;

        if (gameObjectToShow == null)
            return;

        gameObjectToShow.SetActive(true);
    }
}
