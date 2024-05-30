using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorBehavior : MonoBehaviour
{    
    Transform allyTarget;
    NavMeshAgent allyAgent;
    bool isAttacking;

    [SerializeField] float detectionRange = 20f;    
    [SerializeField] float safeDistance = 10f;
    [SerializeField] float attackRange = 8f;
    [SerializeField] float backUpDistance = 2f;
    [SerializeField] ParticleSystem allyBullet;
    void Start()
    {
        allyBullet = GetComponentInChildren<ParticleSystem>();
        allyAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveFromEnemyInRange();
    }

    void MoveFromEnemyInRange()
    {        
        allyTarget = EnemyManager.Instance.GetClosestEnemy(transform);
                
        if (allyTarget != null )
        {
            float distanceToEnemy = Vector3.Distance(transform.position, allyTarget.position); //Continuously find closest enemy.

            // Enemy is within attack range.
            if (distanceToEnemy <= attackRange)
            {
                isAttacking = true;
                AttackEnemy(isAttacking);
            }
            else
            {
                isAttacking = false;
                AttackEnemy(isAttacking);
            }

            // Enemy is reaching into run boundaries.
            if (distanceToEnemy < safeDistance)
            {
                Vector3 directionAwayFromEnemy = (transform.position - allyTarget.position).normalized;
                Vector3 newPosition = transform.position + directionAwayFromEnemy * backUpDistance;
                allyAgent.SetDestination(newPosition);
            }

            // Distance tracking enemies.
            if ((distanceToEnemy <= detectionRange && distanceToEnemy > attackRange) || distanceToEnemy < safeDistance)
            {
                TrackEnemy();
            }
        }
    }

    void AttackEnemy(bool attack)
    {
        var getEmissionModule = allyBullet.emission;
        getEmissionModule.enabled = attack;
    }

    void TrackEnemy()
    {
        transform.LookAt(allyTarget);
    }
}
