using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TutoButtonClick()
    {
        GameObject tutobutton = new GameObject();
        tutobutton.AddComponent<TutorialButton>();

    }
}
