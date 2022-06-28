using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyThis", 1f);
    }

    // Update is called once per frame
    void destroyThis()
    {
        Destroy(this.gameObject);
    }
}
