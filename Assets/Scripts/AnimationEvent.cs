using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject rand;
    private GameObject settigPanel;

    private void Awake()
    {
        settigPanel = GameObject.FindGameObjectWithTag("SettingPanel");
    }

    public void NuniActive()
    {
        //카드 덱 엑티브
        rand.GetComponent<RandomSelect>().ResultSelect();
        GameManager.Money -= 2000;       //500원 빼기
    }

    public void NuniAnimationEnd()
    {
        //누니 애니메이션 종료
        settigPanel.GetComponent<AudioController>().Sound[2].Play();
    }

    public void EffectEnd29()
    {
        Destroy(this.gameObject);
    }
}
