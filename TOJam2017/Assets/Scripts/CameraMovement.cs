using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //MemberVariables
    public float zoomFactor, zoomSpeed, minZoom, maxZoom;
    public float MIN_X, MIN_Y, MAX_X, MAX_Y; //CONSTRAING VALUES
    private bool isFollowing;
    public bool notSkitty;
    public bool skitty1;
    float screenWidth, screenHeight;
    public float moveSpeed = 5f;
    Vector3 position; // center of screen
   
    Transform followTransform;
    bool firstzoomAdjust;
    private void Awake()
    {
        //MIN_X = -16f;
        //MAX_X = 16f;
        //MIN_Y = -10.0f;
        //MAX_Y = 17.0f;
        //MIN_Z = -1000; // not used yet so 1000 to be safe
        //MAX_Z = 1000;

        //minZoom = 2.5f;
        //maxZoom = 15.0f;
        //zoomFactor = 10;
        //zoomSpeed = 5;
        firstzoomAdjust = true;
        isFollowing = false;
        notSkitty = false;
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
        UpdateTransform();
    }

    void UpdateTransform()
    {


        transform.position = position;
        transform.position = new Vector3(
            Mathf.Clamp(position.x, MIN_X, MAX_X),
            Mathf.Clamp(position.y, MIN_Y, MAX_Y),
            Mathf.Clamp(position.z, -10, -10)
            );
    }
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newOrtho = Camera.main.orthographicSize;
        if (scroll != 0.0f)
        {
            zoomFactor -= scroll * zoomSpeed;
            zoomFactor = Mathf.Clamp(zoomFactor, minZoom, maxZoom -1);
        }

       Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoomFactor, zoomSpeed * Time.deltaTime);
       if(newOrtho != Camera.main.orthographicSize)
        {
            AdjustOrtho();
            newOrtho = Camera.main.orthographicSize;
        }
        if (firstzoomAdjust)
        {
            AdjustOrtho();
            newOrtho = Camera.main.orthographicSize;
            firstzoomAdjust = false;
        }
    }
    void AdjustOrtho()
    {
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        MIN_X = -50 + width/2;
        MAX_X = 50 - width/2;
        MIN_Y = -50 + height/2;
        MAX_Y = 50 - height/2;
        
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

        SnapToPosition(transformToFollow.position);
        followTransform = transformToFollow;

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
