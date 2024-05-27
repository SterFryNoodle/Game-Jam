using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform enemyTarget;
    [SerializeField] int enemyHealth = 5;

    void Start()
    {        
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        agent.destination = enemyTarget.position;
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
