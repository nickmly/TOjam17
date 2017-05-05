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

        //ONLY UNCOMMENT ONE SECTION AT A TIME OR CHANGE THE INPUT BUTTONS
        ///////////////////CHENZ/////////////////////////////// 
        //Snap To position only --  no parent attached.
        //This demonstrates the snap to any object function
        //if (Input.GetButton("Fire1")) //left crtl
        //{
        //    mainCam.GetComponent<CameraMovement>().SnapToPosition(transform.position); /// This will snap to the posiiton when the Fire 1 input is pressed
        //}
        ////////////////////CHENZ/////////////////////////////////////
        //Follow Target Functionality-- parent attached.
        //if (Input.GetButton("Fire1")) //left ctrl
        //{
        //    mainCam.GetComponent<CameraMovement>().FollowTarget(transform);           /// This will parent the transfrom to the target on Fire 1
        //}
        //if (Input.GetButton("Fire2")) //Left alt
        //{
        //    mainCam.GetComponent<CameraMovement>().StopFollowing();
        //}
        /////////////////////////////////////////////////////////////////

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
