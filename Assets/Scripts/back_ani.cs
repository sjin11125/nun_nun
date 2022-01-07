using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class back_ani : MonoBehaviour
{

    public Animator back;
    // Start is called before the first frame update
    void Start()
    {
        //back = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void backPlay()
    {
        back.Play("game_back");
    }

}
