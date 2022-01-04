using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSetting : MonoBehaviour
{
    //private ParticleSystem ps;
    private ParticleSystem ps;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void OnclickPlay()
    {
        ps.Play();
    }

}
