using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SESlider;

    public AudioSource BGM;
    public AudioSource SoundEffect;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundChange(string Soundname)
    {
        Debug.Log("change");
        if (Soundname=="BGM")
        {
            BGM.volume = BGMSlider.value;
        }
        else
        {
            SoundEffect.volume = SESlider.value;
        }
      
    }

    
}
