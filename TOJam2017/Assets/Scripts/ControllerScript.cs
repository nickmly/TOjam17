﻿using UnityEngine;
using System.Collections;
using AllMobiles;

public class ControllerScript : MonoBehaviour
{

    // POLYGON COLLIDERS FOR EACH CHARACTER
    [SerializeField]
    PolygonCollider2D skittyCollider, goatyCollider, spuppyCollider;
    //

    /// <summary>
    /// Transform for the gun.
    /// </summary>
    [SerializeField]
    Transform gunTransform;

    /// <summary>
    /// Rigidbody2D component attached to character
    /// </summary>
    Rigidbody2D rb;

    /// <summary>
    /// The sprite for this player
    /// </summary>
    SpriteRenderer sprite;

    /// <summary>
    /// Animator for this player
    /// </summary>
    Animator animator;

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

    private void OnDestroy()
    {
        GameMaster.gameMode.RemovePlayer(this);
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
                sprite.sprite = Resources.Load<Sprite>("Textures/Skitty/Model/alien_normal");
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("MobileModels/Skitty/Model/Skitty");
                skittyCollider.enabled = true;
                break;
            case GameMaster.MobileType.Goaty:
                Goaty goaty = gameObject.AddComponent<Goaty>();
                goaty.gunTransform = gunTransform;
                sprite.sprite = Resources.Load<Sprite>("Textures/Goaty/Model/char_goat_hop-1");
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("MobileModels/Goaty/Model/Goaty");
                goatyCollider.enabled = true;
                break;
            case GameMaster.MobileType.Spuppy:
                Spuppy spuppy = gameObject.AddComponent<Spuppy>();
                spuppy.gunTransform = gunTransform;
                sprite.sprite = Resources.Load<Sprite>("Textures/Spuppy/Model/char_solider_1");
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("MobileModels/Spuppy/Model/Spuppy");
                spuppyCollider.enabled = true;
                break;
        }
        
    }

    public void SetCollidersToTriggers(bool _toggle)
    {
        skittyCollider.isTrigger = _toggle;
        goatyCollider.isTrigger = _toggle;
        spuppyCollider.isTrigger = _toggle;
    }

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        AssignMobileType();
        rb = GetComponent<Rigidbody2D>();
   
        
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
            HandleAnimation();
        }
    }

    /// <summary>
    /// It is now this player's turn again
    /// </summary>
    public void Activate()
    {
        hasShot = false;
        stamina.ResetStamina(this);
        GameMaster.gameMode.abilitiesUI.mobile = mobile;
    }

    /// <summary>
    /// Process input for character
    /// </summary>
    void HandleInput()
    {
        xAxisValue = Input.GetAxis("Horizontal") * Time.deltaTime;

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

        ////TEST SWITCHING TURNS
        //GameMaster.gameMode.AdvanceTurn();
        ////

        Debug.Log("Shooting");
    }

    /// <summary>
    /// Convert inputs to actual movement
    /// </summary>
    void HandleMovement()
    {
        if (stamina.CanMove())
        {
            transform.Translate(transform.right * xAxisValue * (moveSpeed * speedImpairment));
        }
    }

    void HandleAnimation()
    {
        if (IsMoving())
        {
            sprite.flipX = xAxisValue > 0;
        }
        animator.SetFloat("xAxisValue", Mathf.Abs(xAxisValue));
        animator.SetBool("charging", powerBar.IsCharging());
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
