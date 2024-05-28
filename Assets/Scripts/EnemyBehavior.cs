using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isInteractingWithBarrier = false;
    public Transform enemyTarget;

    [SerializeField] int enemyHealth = 5;
    [SerializeField] float barrierInteractionLength = 4f;

    void Start()
    {        
        agent = GetComponent<NavMeshAgent>();
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
            Destroy(gameObject);
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
