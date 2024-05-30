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

    [SerializeField] int initialEnemyHealth = 5;
    [SerializeField] float barrierInteractionLength = 4f;
    [SerializeField] Transform enemyTarget;

    void OnEnable()
    {
        Debug.Log("OnEnable called!");

        enemyHealth = initialEnemyHealth;
        FindPlayerTag();
        EnemyManager.Instance.RegisterEnemy(enemyTarget);
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pool = FindObjectOfType<ObjectPool>();        
    }
    
    void Update()
    {
        if (!isInteractingWithBarrier && enemyTarget != null)
        {
            agent.destination = enemyTarget.position;
        }        
    }
    void FindPlayerTag()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            enemyTarget = playerObject.transform;

            Debug.Log("Player has found: " + enemyTarget);
        }
        else
        {
            Debug.Log("Error! Please make sure player has correct tag.");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        enemyHealth--;
        Debug.Log("Enemy has been hit!");

        if (enemyHealth == 0)
        {           
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
