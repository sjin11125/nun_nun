using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Build : MonoBehaviour
{
   
   
          void Awake()

    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.SetResolution(1080, 1920, false);

        //Screen.SetResolution(Screen.width, (Screen.width / 9) * 16, false);

    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
