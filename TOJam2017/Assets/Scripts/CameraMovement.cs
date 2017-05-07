using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //MemberVariables
    public float zoomFactor, zoomSpeed, minZoom, maxZoom;
    private bool isFollowing;
    public bool notSkitty;
    public bool skitty1;
    float screenWidth, screenHeight;
    float moveSpeed = 5f;
    Vector3 position; // center of screen
    Quaternion rotation;
    Transform followTransform;

    private void Awake()
    {
        minZoom = 2.5f;
        maxZoom = 6.5f;
        zoomFactor = 5;
        zoomSpeed = 5;
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
        HandleZoom();
        transform.position = position;
    }
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            print(scroll);
            zoomFactor -= scroll * zoomSpeed;
            zoomFactor = Mathf.Clamp(zoomFactor, minZoom, maxZoom);
        }

       Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoomFactor, zoomSpeed * Time.deltaTime);
    }
    void HandleScrollMovement()
    {
        float mouseX = Input.mousePosition.x - (screenWidth / 2); //mouse position from the screenCenter as 0,0;
        float mouseY = Input.mousePosition.y - (screenHeight / 2); //..

        if (mouseX > (0.95 * screenWidth) / 2)                  
        {
            position.x += 1.0f * moveSpeed * Time.deltaTime;
        }
        if (mouseX < -((0.95 * screenWidth) / 2))
        {
            position.x -= 1.0f * moveSpeed * Time.deltaTime;
        }
        if (mouseY > (0.95 * screenHeight) / 2)
        {
            position.y += 1.0f * moveSpeed * Time.deltaTime;
        }
        if (mouseY < -((0.95 * screenHeight) / 2))
        {
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
            
        }
    }
    public void StopFollowing()
    {
        isFollowing = false;            // start updating camera edge movement.
    }

    public void SnapToPosition(Vector3 targetPosition)
    {
       position.x = targetPosition.x;
       position.y = targetPosition.y;
       position.z = -10;
       transform.position = position;
    }  
}
