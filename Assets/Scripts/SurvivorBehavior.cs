using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorBehavior : MonoBehaviour
{
    NavMeshAgent allyAgent;
    Transform allyTarget;
    
    [SerializeField] float backUpDistance = 2f;
    [SerializeField] float detectionRange = 15f;
    [SerializeField] float attackRange = 5f;
    private void Awake()
    {
        allyAgent = GetComponent<NavMeshAgent>();
    }
        
    void Update()
    {
        MoveFromEnemyInRange();
    }

    void MoveFromEnemyInRange()
    {
        if (allyTarget == null)
        {
            allyTarget = EnemyManager.Instance.GetClosestEnemy(transform);
        }
        else
        {
            float distanceToEnemy = Vector3.Distance(transform.position, allyTarget.position);

            if (distanceToEnemy < detectionRange)
            {
                Vector3 directionAwayFromEnemy = (transform.position - allyTarget.position).normalized;
                Vector3 newPosition = transform.position + directionAwayFromEnemy * backUpDistance; //takes into account both distance between ally and enemy and backupDistance.
                allyAgent.SetDestination(newPosition);

                if (distanceToEnemy < attackRange)
                {
                    AttackEnemy();
                }
            }

            else
            {
                TrackEnemy();
            }
        }
    }

    void AttackEnemy()
    {


    }

    void TrackEnemy()
    {
        transform.LookAt(allyTarget);
    }


}
