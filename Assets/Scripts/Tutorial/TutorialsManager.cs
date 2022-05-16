using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialsManager : MonoBehaviour
{
    [SerializeField] [Header("Tutorials items")] TutorialsItemControl[] items;
    int itemIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // 모든 아이템을 비활성화 하고, 첫번째 것만 활성화 한다.
        if (items == null)
            return;

        if (items.Length == 0)
            return;

        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }

        itemIndex = -1;
        ActiveNextItem();
    }

    // 다음 아이템을 활성화 한다.
    public void ActiveNextItem()
    {
        // 현재 아이템 비활성화
        if (itemIndex > -1 && itemIndex < items.Length)
        {
            items[itemIndex].gameObject.SetActive(false);
        }

        // 인덱스 변경
        itemIndex++;

        if (itemIndex > -1 && itemIndex < items.Length)
        {
            items[itemIndex].gameObject.SetActive(true);
        }

        if (items.Length == itemIndex)
        {
            PlayerPrefs.SetInt("TutorialDone", 1);
            SceneManager.LoadScene("Main");
        }
    }
}
