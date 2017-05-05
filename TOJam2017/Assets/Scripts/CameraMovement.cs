﻿using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //MemberVariables
    private bool isFollowing;
    float screenWidth, screenHeight;
    float moveSpeed = 5f;
    Vector3 position; // center of screen
                      // float FOV;


    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        position = transform.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFollowing)
        {
            HandleScrollMovement();
            transform.position = position;
        }
    }
    void HandleScrollMovement()
    {
        float mouseX = Input.mousePosition.x - (screenWidth / 2); //mouse position from the screenCenter as 0,0;
        float mouseY = Input.mousePosition.y - (screenHeight / 2); //..
        //print(mouseX); 
        //print(mouseY);
        if (mouseX > (0.95 * screenWidth) / 2)
        {
            print("MoveRight");
            position.x += 1.0f * moveSpeed * Time.deltaTime;
        }
        if (mouseX < -((0.95 * screenWidth) / 2))
        {
            print("MoveLeft");
            position.x -= 1.0f * moveSpeed * Time.deltaTime;
        }
        if (mouseY > (0.95 * screenHeight) / 2)
        {
            print("MoveUp");
            position.y += 1.0f * moveSpeed * Time.deltaTime;
        }
        if (mouseY < -((0.95 * screenHeight) / 2))
        {
            print("MoveDown");
            position.y -= 1.0f * moveSpeed * Time.deltaTime;
        }

    }

    public void FollowTarget(Transform transformToFollow)
    {
        isFollowing = true;
        transform.parent = transformToFollow;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10.0f);
    }
    public void StopFollowing()
    {
        transform.parent = null;
        isFollowing = false;
    }

    public void SnapToPosition(Vector3 targetPosition)
    {
        position.x = targetPosition.x;
        position.y = targetPosition.y;
        position.z = -10;
    }
}