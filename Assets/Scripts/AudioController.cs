using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Music;
    public GameObject musicOn;
    public GameObject SoundOn;
    public AudioSource[] Sound;

    public void Music_on()
    {
        if (musicOn.activeSelf)
        {
            Music.Stop();
            musicOn.SetActive(false);
        }
        else
        {
            Music.Play();
            musicOn.SetActive(true);
        }
    }

    public void Sound_on()
    {
        if (SoundOn.activeSelf)
        {
            SoundOn.SetActive(false);
            for (int i = 0; i < Sound.Length; i++)
            {
                Sound[i].mute = true;
            }
        }
        else
        {
            SoundOn.SetActive(true);
            for (int i = 0; i < Sound.Length; i++)
            {
                Sound[i].mute = false;
            }
        }        
    }
}
