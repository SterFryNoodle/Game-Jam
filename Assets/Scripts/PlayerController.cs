using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;    
    
    [SerializeField] float speed; //SerializeField means you can access & change the variable value in Unity.
    [SerializeField][Range(1, 20)] float sprintSpeed = 3f;
    [SerializeField][Range(1, 15)] float baseSpeed = 1f;    
        
    void Start()
    {        
        speed = baseSpeed; // initializes player speed at base once game starts.        
    }

    void Update()
    {
        GetVector3Input();        
    }

    void GetVector3Input()
    {
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed; //initialize variables to corresponding buttons accessed from Input Manager.

        transform.Translate(horizontalInput, 0 , verticalInput);
                
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

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            // Prevent player from moving through the barrier
            Vector3 direction = transform.position - other.transform.position;
            direction.y = 0; // Only consider horizontal direction
            transform.position += direction.normalized * speed * Time.deltaTime;
            
        }
    }
}

    
