using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

    //Player Component
    ControllerScript player;

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
        MoveCheck();

        if (canMove && player.IsMoving())
        {
            DrainStamina();
        }
    }

    void Initialize()
    {
        player = GetComponent<ControllerScript>();
        //sets default to full
        staminaBarValue = 10f;

        //same as above for stamina 
        staminaBar.minValue = 0f;
        staminaBar.maxValue = 10f;
        staminaBar.value = staminaBarValue;
    }

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

    public bool CanMove()
    {
        return canMove;
    }

}
