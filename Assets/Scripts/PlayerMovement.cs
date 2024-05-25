using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    
    [SerializeField] float speed;
    [SerializeField][Range(1, 5)] float sprintSpeed = 3f;
    [SerializeField][Range(1, 4)] float baseSpeed = 1f;
    [SerializeField] float rotationSpeed = 10f; 
    void Start()
    {
        
    }
    
    void Update()
    {
        GetVector3Input();
        RotationAndMovement();
    }

    void GetVector3Input()
    {
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed; //initialize variables to corresponding buttons accessed from Input Manager.

        transform.Translate(horizontalInput, 0, verticalInput);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift); //Set bool to determine what mode player speed should be set to based on input.

        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = baseSpeed;
        }        
    }

    void RotationAndMovement()
    {
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized; //create a vector that represents direction of movement, normalized to
                                                                                       //have consistent speed.

        if (direction.magnitude >= 0.1f)
        {
            // Calculate target angle and smooth the rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

            // Apply rotation
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Move the player
            Vector3 move = direction * speed * Time.deltaTime;
            transform.Translate(move , Space.World); //ensures movement direction is independent from current player rotation.
        }
    }
}
