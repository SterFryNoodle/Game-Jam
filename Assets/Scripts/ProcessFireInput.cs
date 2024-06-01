using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessFireInput : MonoBehaviour
{
    [SerializeField] ParticleSystem bullet;

    Animator animator;

    void Start()
    {
        bullet = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
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
            animator.SetBool("isShooting", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            var getEmissionModule = bullet.emission;

            getEmissionModule.enabled = false;
            animator.SetBool("isShooting", false);
        }
    }    
}
