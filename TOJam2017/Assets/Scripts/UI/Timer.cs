using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text counterText;

    public float seconds = 45f;

    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

    void Update()
    {
        //seconds -= (int)Time.deltaTime;
        seconds -= (Time.deltaTime);
        counterText.text = seconds.ToString("00");
    }

    void ResetTime()
    {
        seconds = 45f;
    }

    void RanOutOfTime()
    {
        if (seconds == 0)
        {
            GameMaster.gameMode.AdvanceTurn();
        }
    }

}
