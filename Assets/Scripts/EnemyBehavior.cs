using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject playerObject;

    Rigidbody enemyRb;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerObject = GetComponent<GameObject>();
    }

    
    void Update()
    {
        Vector3 lookDirection = (playerObject.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed);   
    }
}
