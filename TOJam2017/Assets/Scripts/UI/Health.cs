using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    //Health Stuff 
    public Image healthBar;
    public float maxHp;
    public float currentHp;
    float hpBar;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        DeathCheck();
        HealthBar(hpBar);
    }

    void Initialize()
    {
        maxHp = this.GetComponent<AllMobiles.Mobiles>().Health;
        currentHp = maxHp;
        hpBar = currentHp / maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp < 0)
            currentHp = 0;

        hpBar = currentHp / maxHp;

        HealthBar(hpBar);
    }

    void HealthBar(float health)
    {
        healthBar.fillAmount = (Mathf.Lerp(healthBar.fillAmount, health, 0.2f));
    }

    void DeathCheck()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
