using UnityEngine;
using System.Collections;
using AllMobiles;

public class ControllerScript : MonoBehaviour
{

    /// <summary>
    /// Transform for the gun.
    /// </summary>
    [SerializeField]
    Transform gunTransform;

    /// <summary>
    /// Rigidbody component attached to character
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// Mobile component attached to character
    /// </summary>
    Mobiles mobile;

    /// <summary>
    /// The power bar in the HUD
    /// </summary>
    public PowerBar powerBar;

    /// <summary>
    /// Health and stamina in HUD
    /// </summary>
    Stamina stamina;
    Health health;

    CameraMovement mainCam;

    /// <summary>
    /// Horizontal axis value from controller/keyboard
    /// </summary>
    float xAxisValue = 0;

    /// <summary>
    /// Speed for horizontal movement
    /// </summary>
    [SerializeField]
    float moveSpeed = 15.0f;

    /// <summary>
    /// Value to impair the speed of mobile
    /// </summary>
    public float speedImpairment = 1.0f;

    /// <summary>
    /// Identifier for player to distinguish between different players
    /// </summary>
    public int playerID = 0;


    /// <summary>
    /// Has this player shot yet?
    /// </summary>
    bool hasShot = false;

    void Awake()
    {
        stamina = FindObjectOfType<Stamina>();
        health = GetComponent<Health>();
    }

    /// <summary>
    /// Change this controller to have the appropriate mobile type
    /// </summary>
    void AssignMobileType()
    {
        switch (GameMaster.mobileTypes[playerID])
        {            
            case GameMaster.MobileType.Skitty:
                Skitty skitty = gameObject.AddComponent<Skitty>();
                skitty.gunTransform = gunTransform;
                break;
            case GameMaster.MobileType.Goaty:
                Goaty goaty = gameObject.AddComponent<Goaty>();
                goaty.gunTransform = gunTransform;
                break;
        }
    }

    void Start()
    {
        AssignMobileType();
        rb = GetComponent<Rigidbody>();
        mobile = GetComponent<Mobiles>();
        mainCam = Camera.main.GetComponent<CameraMovement>();
        powerBar = FindObjectOfType<PowerBar>();
       

    }

    void Update()
    {
        if (IsMyTurn())
        {
            HandleInput();
            HandleMovement();
        }
    }

    /// <summary>
    /// It is now this player's turn again
    /// </summary>
    public void Activate()
    {
        hasShot = false;
        stamina.ResetStamina(this);
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
        //    mainCam.SnapToPosition(transform.position); /// This will snap to the posiiton when the Fire 1 input is pressed
        //}
        ////////////////////CHENZ/////////////////////////////////////
        //Follow Target Functionality-- parent attached.
        //if (Input.GetButton("Fire1")) //left ctrl
        //{
        //    mainCam.FollowTarget(transform);           /// This will parent the transfrom to the target on Fire 1
        //}
        //if (Input.GetButton("Fire2")) //Left alt
        //{
        //    mainCam.StopFollowing();
        //}
        /////////////////////////////////////////////////////////////////

        // Tabbing through the 3 different attacks
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mobile.attack++;

            if (mobile.attack > 2)
            {
                mobile.attack = 0;
            }
        }
        if (!hasShot && powerBar.IsCharging())
        {
            // Checking to see if it will spawn different ammo
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Shoot();
            }
        }

    }

    void Shoot()
    {
        mobile.AttackShot(powerBar.GetValue());
        hasShot = true;

        //TEST SWITCHING TURNS
        GameMaster.gameMode.AdvanceTurn();
        //
        
        Debug.Log("Shooting");
    }

    /// <summary>
    /// Convert inputs to actual movement
    /// </summary>
    void HandleMovement()
    {
        if(stamina.CanMove())
            transform.Translate(transform.right * xAxisValue * (moveSpeed * speedImpairment));
    }

    public void SetID(int _id)
    {
        playerID = _id;
    }

    /// <summary>
    /// Check if this player is able to take input
    /// </summary>
    public bool IsMyTurn()
    {
        return GameMaster.gameMode.currentPlayerTurn == playerID;
    }

    public bool IsMoving()
    {
        return Mathf.Abs(xAxisValue) > 0;
    }

}
