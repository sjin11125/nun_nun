using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Music;
    public GameObject musicOn;
    public GameObject SoundOn;
    public Sounds[] Sound;

    [System.Serializable]
    public struct Sounds
    {
        public string name;
        public AudioSource audio;
    }

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
        if (Sound.Length > 0)
        {
            for (int i = 0; i < Sound.Length; i++)
            {
                if (SoundOn.activeSelf)
                {
                    Sound[i].audio.mute = true;
                    SoundOn.SetActive(false);
                }
                else
                {
                    Sound[i].audio.mute = false;
                    SoundOn.SetActive(true);
                }
            }
        }         
    }
}
