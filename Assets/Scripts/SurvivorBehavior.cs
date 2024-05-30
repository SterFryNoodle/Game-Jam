using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorBehavior : MonoBehaviour
{    
    Transform allyTarget;
    bool isAttacking;
    
    [SerializeField] float backUpDistance = 2f;
    [SerializeField] float safeDistance = 10f;
    [SerializeField] float detectionRange = 20f;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem allyBullet;
    void Start()
    {
        allyBullet = GetComponentInChildren<ParticleSystem>();
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

            if (distanceToEnemy <= detectionRange)
            {                
                // Enemy is within attack range.
                if (distanceToEnemy <= attackRange)
                {
                    isAttacking = true;
                    AttackEnemy(isAttacking);
                }
                else if (distanceToEnemy < safeDistance)
                {
                    Vector3 directionAwayFromEnemy = (transform.position - allyTarget.position).normalized;
                    Vector3 newPosition = transform.position + directionAwayFromEnemy * backUpDistance; //Takes into account both distance between ally and enemy and backupDistance.
                    transform.position = newPosition;
                }
                else
                {
                    TrackEnemy();
                }
            }            
        }
    }

    void AttackEnemy(bool attack)
    {
        if (attack)
        {
            var getEmissionModule = allyBullet.emission;
            getEmissionModule.enabled = attack;
        }
        else if(!attack)
        {
            var getEmissionModule = allyBullet.emission;
            getEmissionModule.enabled = !attack;
        }        
    }

    void TrackEnemy()
    {
        transform.LookAt(allyTarget);
    }
}
