using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //MemberVariables
    private bool isFollowing;
    public bool notSkitty;
    public bool skitty1;
    float screenWidth, screenHeight;
    float moveSpeed = 5f;
    Vector3 position; // center of screen
    Quaternion rotation;
    
                      // float FOV;
    Transform followTransform;

    private void Awake()
    {
        isFollowing = false;
        notSkitty = false;
    }
    // Use this for initialization
    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
       
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!isFollowing)
        {
            HandleScrollMovement();
        }
        else
        {
            if (followTransform != null)
            {
                position = followTransform.position;
                position.z = -10;
            }
            else
            {
                isFollowing = false;
            }
        }
        transform.position = position;
        transform.rotation = rotation;
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

        if (transformToFollow != null)
        {
       
            SnapToPosition(transformToFollow.position);
            rotation = transformToFollow.rotation;
            followTransform = transformToFollow;
        }
        else
        {
            StopFollowing();
            followTransform = null;
        }
    }
    public void StopFollowing()
    {
        isFollowing = false;            // start updating camera edge movement.
       // followTransform = null;
    }

    public void SnapToPosition(Vector3 targetPosition)
    {
       position.x = targetPosition.x;
       position.y = targetPosition.y;
       position.z = -10;
       transform.position = position;
    }  
}
