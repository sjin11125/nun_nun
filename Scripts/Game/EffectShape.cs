using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShape : MonoBehaviour
{
    AudioSource audioSource;
    bool OndeAct = false;
    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    public void ActivateEffect() //animation add event에 사용
    {
        if(OndeAct == true)
        {
            this.audioSource.Stop();
        }
        else
        {
            this.audioSource.Play();
        }
    }
    public void DeactivateEffect() //animation add event에 사용
    {        
        OndeAct = true;
        Destroy(gameObject);
    }
}
