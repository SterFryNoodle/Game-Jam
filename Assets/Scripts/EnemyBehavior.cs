using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject enemyTarget;
    [SerializeField] int enemyHealth = 5;

    Rigidbody enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyTarget = FindObjectOfType<GameObject>();
    }

    
    void Update()
    {
        Vector3 lookDirection = (enemyTarget.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed);   
    }

    private void OnParticleCollision(GameObject other)
    {
        enemyHealth--;
        Debug.Log("Enemy has been hit!");

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
