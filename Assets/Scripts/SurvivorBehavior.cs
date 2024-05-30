using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorBehavior : MonoBehaviour
{
    private NavMeshAgent allyAgent;

    [SerializeField] Transform allyTarget;

    [SerializeField] float safeDistance = 4f;
    [SerializeField] float backUpDistance = 2f;
    private void Awake()
    {
        allyAgent = GetComponent<NavMeshAgent>();
    }
        
    void Update()
    {
        if(allyTarget == null)
        {
            allyTarget = EnemyManager.Instance.GetClosestEnemy(allyTarget.transform);
        }
        else
        {
            float distanceToEnemy = Vector3.Distance(transform.position, allyTarget.position);

            if(distanceToEnemy < safeDistance)
            {
                Vector3 directionAwayFromEnemy = (transform.position - allyTarget.position).normalized;
                Vector3 newPosition = transform.position + directionAwayFromEnemy * backUpDistance; //takes into account both distance between ally and enemy and backupDistance.

                allyAgent.SetDestination(newPosition);
            }

            else
            {
                allyAgent.SetDestination(allyTarget.position);
            }
        }
    }


}
