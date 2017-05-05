using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    /// <summary>
    /// The speed at which you can rotate
    /// </summary>
    [SerializeField]
    float rotateSpeed = 50.0f;

    /// <summary>
    /// Vertical axis value from controller/keyboard
    /// </summary>
    float yAxisValue = 0.0f;

    void Start()
    {

    }


    void Update()
    {
        HandleInput();
        Rotate();
    }

    void HandleInput()
    {
        yAxisValue = Input.GetAxis("Vertical");
    }

    void Rotate()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, yAxisValue * rotateSpeed * Time.deltaTime);
    }
}
