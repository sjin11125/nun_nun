using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Music;
    public GameObject musicOn;
    public GameObject SoundOn;
    public AudioSource[] Sound;

    public bool gameScene;

    private void Awake()
    {
        if (gameScene)
        {
            if (!GameManager.gameMusicOn)
            {
                musicOn.SetActive(false);
                Music.Stop();
            }
            if (!GameManager.gameSoundOn)
            {
                SoundOn.SetActive(false);
                for (int i = 0; i < Sound.Length; i++)
                {
                    Sound[i].mute = true;
                }
            }
        }
        else
        {
            if (!GameManager.mainMusicOn)
            {
                musicOn.SetActive(false);
                Music.Stop();
            }
            if (!GameManager.mainSoundOn)
            {
                SoundOn.SetActive(false);
                for (int i = 0; i < Sound.Length; i++)
                {
                    Sound[i].mute = true;
                }
            }
        }
    }

    public void Music_on()
    {
        if (musicOn.activeSelf)
        {
            Music.Stop();
            musicOn.SetActive(false);
            if (gameScene)
            {
                GameManager.gameMusicOn = false;
            }
            else
            {
                GameManager.mainMusicOn = false;
            }
        }
        else
        {
            Music.Play();
            musicOn.SetActive(true);
            if (gameScene)
            {
                GameManager.gameMusicOn = true;
            }
            else
            {
                GameManager.mainMusicOn = true;
            }
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

            if (gameScene)
            {
                GameManager.gameSoundOn = false;
            }
            else
            {
                GameManager.mainSoundOn = false;
            }
        }
        else
        {
            SoundOn.SetActive(true);
            for (int i = 0; i < Sound.Length; i++)
            {
                Sound[i].mute = false;
            }

            if (gameScene)
            {
                GameManager.gameSoundOn = true;
            }
            else
            {
                GameManager.mainSoundOn = true;
            }
        }        
    }
}
