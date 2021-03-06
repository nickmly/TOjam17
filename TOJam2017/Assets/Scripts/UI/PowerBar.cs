﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider powerBar;
    private float powerBarThreshhold = 10f;
    private float powerBarValue = 0f;

    private bool didShoot = false;
    private bool charging = false;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetPower();
            charging = true;
        }

        if (charging && Input.GetKeyUp(KeyCode.Space))
        {
            Shot();
            charging = false;
        }
    }

    void Initialize()
    {
        //Finds our slider in the scene UI canvas 
        powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();

        powerBarValue = 0f;
        //Sets the minimum, maximum value clamps of the power bar and sets the current value to our variable
        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }

    void SetPower()
    {      
        //Racks up the power for the charge meter  
        powerBarValue += powerBarThreshhold * Time.deltaTime;
        powerBarValue = Mathf.Clamp(powerBarValue, powerBar.minValue, powerBar.maxValue);
      
        powerBar.value = powerBarValue;
        didShoot = false;
    }

    void Shot()
    {
        //resets the charge meter
        didShoot = true;
        powerBarValue = 0f;
        powerBar.value = powerBarValue;
    }

    public bool DidShoot()
    {
        return didShoot;
    }

    public bool IsCharging()
    {
        return charging;
    }

    public float GetValue()
    {
        return powerBarValue;
    }
}
