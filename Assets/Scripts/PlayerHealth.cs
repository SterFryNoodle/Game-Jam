using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] TMP_Text defeatText;
    private int currentHealth;
    private int dmgTaken = 10;
    private int delaySeconds = 4;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(dmgTaken);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Invoke("TakeBackToMenu", delaySeconds);
        }
    }

    void TakeBackToMenu()
    {
        defeatText.enabled = true;
        SceneManager.LoadScene("Main Menu");
    }
}
