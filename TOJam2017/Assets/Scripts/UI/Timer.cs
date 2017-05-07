using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text counterText;
    public float setShotTime = 25.0f;
    private float seconds = 25f;
    private bool start = false;
    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

    void Update()
    {
        //seconds -= (int)Time.deltaTime;
        if (start) {
            seconds -= (Time.deltaTime);
            counterText.text = seconds.ToString("00");
            ColourMeRad();
            if(seconds <= 0)
            {
                GameMaster.gameMode.AdvanceTurn();
                start = false;
            }
        }
    }

    void ResetTime()
    {
        seconds = setShotTime;
    }
    public void StartTimer()
    {
        ResetTime();
        start = true;
    }
    public void PauseTimer()
    {
        start = false;
    }
    void ColourMeRad()
    {
        if(seconds > 19f)
        {
            counterText.color = Color.green;
        }
         else if(seconds > 9f) { 
       
            counterText.color = Color.yellow;
        }
        else
        {
            counterText.color = Color.red;
        }

    }

}
