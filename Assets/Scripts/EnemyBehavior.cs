using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isInteractingWithBarrier = false;
    private ObjectPool pool;
    private int enemyHealth;
    Animator animator;

    [SerializeField] int initialEnemyHealth = 5;
    [SerializeField] float barrierInteractionLength = 4f;
    [SerializeField] Transform enemyTarget;
    [SerializeField] float detectionRange = 20f;

    void OnEnable()
    {        
        EnemyManager.Instance.RegisterEnemy(transform);
        enemyHealth = initialEnemyHealth;
        FindPlayerTag();        
    }

    void OnDisable()
    {
        EnemyManager.Instance.UnRegisterEnemy(transform);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pool = FindObjectOfType<ObjectPool>();   
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        UpdateTarget();
        
        if (!isInteractingWithBarrier && enemyTarget != null)
        {
            agent.destination = enemyTarget.position;
        }        
    }
    
    void UpdateTarget()
    {
        Transform closestTarget = EnemyManager.Instance.GetClosestAlly(transform);

        if (closestTarget != enemyTarget)
        {
            enemyTarget = closestTarget;
        }

        if (enemyTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, enemyTarget.position);

            if (distanceToTarget <= detectionRange)
            {
                agent.SetDestination(enemyTarget.position);
            }
        }
    }
    
    void FindPlayerTag()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

        if (playerObject != null && distanceToPlayer > detectionRange)
        {
            enemyTarget = playerObject.transform;            
        }
        else if (playerObject == null) 
        {
            Debug.Log("Error! Please make sure player has correct tag.");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        enemyHealth--;
        
        if (enemyHealth == 0)
        {
            animator.SetTrigger("isDead");
            pool.ReturnEnemy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            StartCoroutine(InteractWithBarrier(other.gameObject));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Survivor")
        {
            agent.isStopped = true;
            animator.SetTrigger("isBiting");
            agent.isStopped = false;
        }
    }

    IEnumerator InteractWithBarrier(GameObject barrier)
    {
        isInteractingWithBarrier = true;

        agent.isStopped = true;

        yield return new WaitForSeconds(barrierInteractionLength); // enemy stops at barrier for amount of time it take
                                                                                                                                            
        barrier.SetActive(false); // barrier is destroyed.

        agent.isStopped = false;
        isInteractingWithBarrier = false;

        agent.destination = enemyTarget.position;
    }
}
