using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool goNext;
    public bool isGame;

    private void OnEnable()
    {
        Invoke("ShowGameObject", timeToInput);
    }

    void Update()
    {
        // 입력대기 상태가 되면 터치를 입력 받는다.
        if (isReadyToInput)
        {
            if (itemType == ItemType.touch)
            {
                // 입력을 하면 계속 진행
                if (Input.GetMouseButtonDown(0) && goNext)
                {
                    Run();                  
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
        if (isGame)
        {
            GameTutorialsManager parentTutorialsManager = transform.parent.GetComponent<GameTutorialsManager>();
            if (parentTutorialsManager != null)
            {
              //  if (!parentTutorialsManager.isItem)
               // {
                    parentTutorialsManager.ActiveNextItem();
               // }
            }
        }
        else
        {
            TutorialsManager parentTutorialsManager = transform.parent.GetComponent<TutorialsManager>();
            if (parentTutorialsManager != null)
            {
                parentTutorialsManager.ActiveNextItem();
            }
        }
    }

    void ShowGameObject()
    {
        isReadyToInput = true;

        if (gameObjectToShow == null)
            return;

        gameObjectToShow.SetActive(true);
    }

    public void HiRandomOnclick()
    {
        LoadingSceneController.Instance.LoadScene(SceneName.Shop);
        goNext = true;
    }

    public void StoreCloseOnclick()
    {
        goNext = true;
    }

    public void GameItem()
    {
        goNext = true;
    }
}
