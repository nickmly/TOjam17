using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    //Player Component
    ControllerScript player;

    //Health Stuff 
    public Slider healthBar;
    //not used private float healthBarThreshhold = 10f;
    private float healthBarValue = 0f;
    
    void Awake()
    {
        Initialize();
    }

    void Update()
    {
       
        DeathCheck();
    }

    void Initialize()
    {
        player = GetComponent<ControllerScript>();

        //Sets both the defaults of the stats to full
        healthBarValue = 10f;
        //Sets the minimum, maximum value clamps of the health & stamina bar and sets the current value to our variable
        healthBar.minValue = 0f;
        healthBar.maxValue = 10f;
        healthBar.value = healthBarValue;
    }

    public void TakeDamage(float damage)
    {
        healthBarValue -= damage;
        healthBarValue = Mathf.Clamp(healthBarValue, healthBar.minValue, healthBar.maxValue);

        healthBar.value = healthBarValue;
    }

    void DeathCheck()
    {
        //kills the unit
        if (healthBar.value < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
