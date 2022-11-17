using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkipButton : MonoBehaviour
{
    public GameObject TutorialManager;
    public static bool isTutoStop=false;

    public GameObject Bunsu;

    public static bool isGameTutoSkip = false;
    // Start is called before the first frame update
    void Start()
    {
        TutorialManager = GameObject.Find("TutorialsManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TutorialSkip()
    {
        isTutoStop = true; TutorialsManager.itemIndex = 14;
       
        
        Bunsu.SetActive(true);
        TutorialManager.SetActive(false);
    }
    public void GameTutoSkip()
    {
        Debug.Log("½Ã¹ß");
        isGameTutoSkip = true;
        TutorialManager.SetActive(false);
    }
}
