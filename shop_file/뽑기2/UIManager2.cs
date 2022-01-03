using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager2 : MonoBehaviour
{

    public GameObject GetButton;
    public GameObject backButton;

    public AudioSource water;
    public AudioSource backsound;


    public void Click()
    {
        water = GetComponent<AudioSource>();
        water.Play();
        Destroy(GetButton);
        //Button.SetBool("Get", false);
    }

    public void backclick()
    {
        backsound.Play();
    }
    // Start is called before the first frame update
   
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
