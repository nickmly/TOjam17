using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllMobiles;

public class AbilitiesUI : MonoBehaviour {

    //Player Component
    ControllerScript player;
    Mobiles mobile;

    public Image skill1, skill2, special;

    public bool skill1Active, skill2Active, specialActive;


    void Start()
    {
        mobile = GetComponent<Mobiles>();
    }

    void Update()
    {
        ActiveCheck();
    }


    void ActiveCheck()
    {
        //skill 1 highlight
        //sets the rest to false and resets their color
        if (mobile.attack == 0)
        {
            skill1Active = true;
            skill2Active = false;
            specialActive = false;
            ResetColors();
            skill1.GetComponent<Image>().color = Color.cyan;
        }

        //skill 2
        //sets the rest to false and resets their color
        if (mobile.attack == 1)
        {
            skill1Active = false;
            skill2Active = true;
            specialActive = false;
            ResetColors();
            skill2.GetComponent<Image>().color = Color.cyan;
        }

        //special 
        //sets the rest to false and resets their color
        if (mobile.attack == 2)
        {
            skill1Active = false;
            skill2Active = false;
            specialActive = true;
            ResetColors();
            special.GetComponent<Image>().color = Color.cyan;
        }
        
    }

    void ResetColors()
    {
        skill1.GetComponent<Image>().color = Color.white;
        skill2.GetComponent<Image>().color = Color.white;
        special.GetComponent<Image>().color = Color.white;
    }

}
