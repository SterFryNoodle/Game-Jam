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

    public Transform enemyTarget;

    [SerializeField] int initialEnemyHealth = 5;
    [SerializeField] float barrierInteractionLength = 4f;

    void Start()
    {        
        agent = GetComponent<NavMeshAgent>();
        pool = FindObjectOfType<ObjectPool>();
        enemyHealth = initialEnemyHealth;
    }
    
    void Update()
    {
        if (!isInteractingWithBarrier && enemyTarget != null)
        {
            agent.destination = enemyTarget.position;
        }        
    }

    private void OnParticleCollision(GameObject other)
    {
        enemyHealth--;
        Debug.Log("Enemy has been hit!");

        if (enemyHealth == 0)
        {
            enemyHealth = initialEnemyHealth;
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
