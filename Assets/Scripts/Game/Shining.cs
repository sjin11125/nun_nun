using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shining : MonoBehaviour
{
    float time;
    bool down = true;

    public void ShinActive()//shapeStorage¿¡¼­ ÄÑ±â
    {
        int pers = Random.Range(0, 100);
        if (pers == 0 || pers == 1|| pers == 2)        //3ÆÛ È®·ü·Î ¶ß°Ô
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (down)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 1f - time / 1f);
                time += Time.deltaTime;
                if (time > 1.1f)
                {
                    down = false;
                }
            }
            else
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 1f - time / 1f);
                time -= Time.deltaTime;
                if (time < -0.1f)
                {
                    down = true;
                }
            }
        }       
    }
}
