using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindDirection : MonoBehaviour {

    RectTransform rectTransform;
    //randomized angle of the new wind
    //*******************************NOTES******************************
    // 0 = east, 90 = north, 180 = west, 270 = south, 360 = east
    //******************************************************************
    public int angle;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

        //For Testing purposes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetWind();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            RandomWind();
        }
    }

    void RandomWind()
    {
        //randomizes the winds angle
        angle = Random.Range(0, 360);
        rectTransform.transform.eulerAngles = new Vector3(0, 0, angle);
    }


    //Resets the direction back to a facing east. 
    void ResetWind()
    {
        rectTransform.transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
