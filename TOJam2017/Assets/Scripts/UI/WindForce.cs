using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindForce : MonoBehaviour {

    public Text windForce;

    public float force = 0f;

	void Start ()
    {
		windForce = GetComponent<Text>() as Text;
    }
	


	void Update () {
        //Spits out the force in the proper format
        windForce.text = force.ToString("00") + " " + "m/s";

        //Testing purposes
        if (Input.GetKeyDown(KeyCode.N))
        {
            RandomForce();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ResetForce();
        }

    }

    //randomizes the force between 0-14
    void RandomForce()
    {
        force = Random.Range(0, 14);
    }

    //Resets the force to 0
    void ResetForce()
    {
        force = 0f;
    }
}
