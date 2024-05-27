using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject enemyTarget;
    [SerializeField] int enemyHealth = 5;

    Rigidbody enemyRb;
    bool newPathNeeded;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyTarget = FindObjectOfType<GameObject>();
    }

    
    void Update()
    {
        FindPathToPlayer();  
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

    void FindPathToPlayer()
    {
        newPathNeeded = PathIsBlocked(gameObject);

        if (newPathNeeded)
        {
            //recalculate enemy path.
        }
        else
        {
            Vector3 lookDirection = (enemyTarget.transform.position - transform.position).normalized;

            enemyRb.AddForce(lookDirection * speed);
        }        
    }

    bool PathIsBlocked(GameObject target)
    {
        bool isBlocked;

        return isBlocked;
    }
}
