using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_button : MonoBehaviour
{

    //int count = 0; 
    //public GameObject stopimage;
    public static bool IsPause;
    // Start is called before the first frame update

    void Start()
    {
        IsPause = false;
        //stopimage.SetActive == false;
    }

     //Update is called once per frame
    void Update()
    {
           // antiPutra();
 
    }

    public void Putra()
    {
        if (IsPause == false)
        {
            Time.timeScale = 0;
            IsPause = true;
            return;

           // count++;

            //stopimage.SetActive == true;
        }
    }

    /*public void antiPutra()
    {
        if (count == 1)
        {
            if (IsPause == true)
            {
                Time.timeScale = 1;
                IsPause = false;
                return;
            }
        }
    }
    */
}
