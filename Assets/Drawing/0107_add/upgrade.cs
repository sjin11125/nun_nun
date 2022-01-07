using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    //public Text healthText;
    public Image healthBar;
    public Image[] healthPoints; // 게이지바들 리스트 생성

    float health = 100; 
    float maxHealth = 100;
    float lerpSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        health = 0;
    } 

    private void Update() 
    {
        //healthText.text = "Health: " + health + "%";
        if (health > maxHealth) health = maxHealth;
        {
            lerpSpeed = 3f * Time.deltaTime;

            HealthBarFiller();
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
        for(int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(health, i);
        }
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }
    // Update is called once per frame

    public void Heal (float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }

}
