using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public ParticleSystem particleObj;

    AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void particlePlay() //animation add event에 사용
    {
        particleObj.Play();
        this.audioSource.Play();
    }

    public void audioPlay() //animation add event에 사용
    {
        audioSource2.Play();
    }

    public void audioPlay2() //animation add event에 사용
    {
        audioSource3.Play();
    }
}
