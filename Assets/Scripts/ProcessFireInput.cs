using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessFireInput : MonoBehaviour
{
    GameObject bullet;
    void Start()
    {
        Transform particleSystemTransform = transform.Find("Gun/Bullets");

        if (particleSystemTransform != null )
        {            
            var emissionModule = bullet.GetComponent<ParticleSystem>().emission;            
        }
        else
        {
            Debug.Log("Child object not found!");
        }
    }

    void Update()
    {
        //FireInput();
    }

    /*void FireInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            emissionModule.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            emissionModule.enabled = false;
        }
    }*/
}
