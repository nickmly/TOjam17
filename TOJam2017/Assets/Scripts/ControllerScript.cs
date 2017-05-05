using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {

    /// <summary>
    /// Rigidbody component attached to character
    /// </summary>
    Rigidbody rb;

    public Camera mainCam;

    /// <summary>
    /// Horizontal axis value from controller/keyboard
    /// </summary>
    float xAxisValue = 0;

    /// <summary>
    /// Speed for horizontal movement
    /// </summary>
    [SerializeField]
    float moveSpeed = 15.0f;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        
	}
	
	
	void Update ()
    {
        HandleInput();
        HandleMovement();
	}
    
    /// <summary>
    /// Process input for character
    /// </summary>
    void HandleInput()
    {
        xAxisValue = Input.GetAxis("Horizontal") * Time.deltaTime;

        // ------ Testing ------

       
        ////This demonstrates the snap to any object function
        //if (Input.GetButton("Fire1")) //left crtl
        //{
        //    mainCam.GetComponent<CameraMovement>().SnapToPosition(transform.position);
        //}


        // Tabbing through the 3 different attacks
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetComponent<AllMobiles.Mobiles>().attack++;

            if (GetComponent<AllMobiles.Mobiles>().attack > 2)
            {
                GetComponent<AllMobiles.Mobiles>().attack = 0;
            }
        }
        // Checking to see if it will spawn different ammo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AllMobiles.Mobiles>().AttackShot();
            Debug.Log("Shooting");
        }
        // ------Testing------

    }

    /// <summary>
    /// Convert inputs to actual movement
    /// </summary>
    void HandleMovement()
    {
        transform.Translate(transform.right * xAxisValue * moveSpeed);
    }
}
