using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    Animator animator;
    private int currentHealth;
    private int dmgTaken = 10;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(dmgTaken);
            animator.SetBool("isHit", true);
        }
        else
        {
            animator.SetBool("isHit", false);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
