using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessFireInput : MonoBehaviour
{
    [SerializeField] ParticleSystem bullet;

    void Start()
    {
        bullet = GetComponentInChildren<ParticleSystem>();        
    }

    void Update()
    {
        FireInput();
    }

    void FireInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var getEmissionModule = bullet.emission;

            getEmissionModule.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            var getEmissionModule = bullet.emission;

            getEmissionModule.enabled = false;
        }
    }    
}
