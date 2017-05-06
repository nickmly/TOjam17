using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text counterText;

    private float seconds = 25f;

    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

    void Update()
    {
        //seconds -= (int)Time.deltaTime;
        seconds -= (Time.deltaTime);
        counterText.text = seconds.ToString("00");
        ColourMeRad();
    }

    void ResetTime()
    {
        seconds = 25f;
    }

    void RanOutOfTime()
    {
        if (seconds <= 0)
        {
            GameMaster.gameMode.AdvanceTurn();
        }
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
