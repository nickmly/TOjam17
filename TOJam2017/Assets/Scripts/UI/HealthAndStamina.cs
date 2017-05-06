using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndStamina : MonoBehaviour {

    //Player Component
    ControllerScript player;

    //Health Stuff 
    public Slider healthBar;
    //not used private float healthBarThreshhold = 10f;
    private float healthBarValue = 0f;

    //Stamina Stuff
    public Slider staminaBar;
    //change the threshhold to increase stamina drain rate
    private float staminaBarThreshhold = 5f;
    private float staminaBarValue = 0f;

    private bool isMoving = false;
    private bool canMove = true;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        //stamina stuff--------------------------------------------
        MoveCheck();

        if (canMove && player.IsMoving())
        {
            DrainStamina();
        }
        //----------------------------stam stuff end---------------
        DeathCheck();
    }
	
    void Initialize()
    {
        player = GetComponent<ControllerScript>();

        //Sets both the defaults of the stats to full
        healthBarValue = 10f;
        staminaBarValue = 10f;
        //Sets the minimum, maximum value clamps of the health & stamina bar and sets the current value to our variable
        healthBar.minValue = 0f;
        healthBar.maxValue = 10f;
        healthBar.value = healthBarValue;
        //same as above for stamina 
        staminaBar.minValue = 0f;
        staminaBar.maxValue = 10f;
        staminaBar.value = staminaBarValue;
    }

    //=================================STAMINA TINGS=============================================
    void DrainStamina()
    {
        //reduces stamina when called
        staminaBarValue -= staminaBarThreshhold * Time.deltaTime;
        staminaBarValue = Mathf.Clamp(staminaBarValue, staminaBar.minValue, staminaBar.maxValue);

        staminaBar.value = staminaBarValue;
    }

    //only able to move if the unit has stamina
    void MoveCheck()
    {
        if (staminaBarValue > 0f)
            canMove = true;
        else
            canMove = false;
    }

    public void ResetStamina()
    {
        staminaBarValue = 10f;
        staminaBar.value = staminaBarValue;
    } 
    
    //=================================STAMINA TINGS END =============================================

    //=================================HEALTH TINGS=============================================
    //**TO DO** link the health stuff up to the mobiles script values 

    void TakeDamage(float damage)
    {
        healthBarValue -= damage;
        healthBarValue = Mathf.Clamp(healthBarValue, healthBar.minValue, healthBar.maxValue);

        healthBar.value = healthBarValue;
    }

    void DeathCheck()
    {
        //kills the unit
        //kills all units unfortunately.
        if (healthBar.value < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    public bool CanMove()
    {
        return canMove;
    }
}
