using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    //Health Stuff 
    public Slider healthBar;
    public float maxHp;
    public float currentHp;
    float hpBar;

    void Start()
    {

    }

    void Update()
    {
        DeathCheck();
        HealthBar(hpBar);
    }

    public void Initialize()
    {
        maxHp = this.GetComponent<AllMobiles.Mobiles>().Health;
        currentHp = maxHp;
        hpBar = currentHp / maxHp;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken: " + damage);
        currentHp -= damage;

        if (currentHp < 0)
            currentHp = 0;

        hpBar = currentHp / maxHp;

        HealthBar(hpBar);
    }

    void HealthBar(float health)
    {
        healthBar.value = (Mathf.Lerp(healthBar.value, health, 0.2f));
    }

    void DeathCheck()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
