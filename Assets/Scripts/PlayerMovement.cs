using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    [SerializeField][Range(1, 5)] int playerSpeed = 1;
    void Start()
    {
        
    }

    
    void Update()
    {
        GetVector3Input();
    }

    void GetVector3Input()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical"); //initialize variables to corresponding buttons accessed from Input Manager.

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * playerSpeed);
    }
}
