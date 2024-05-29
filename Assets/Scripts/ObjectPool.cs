using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int initialPoolSize = 5;
    [SerializeField] int maxPoolSize = 50;
    [SerializeField] int amtIncrease = 5;
    [SerializeField] float timeInterval = 20f;

    private List<GameObject> enemyPool;
    private int currentPoolSize;
    
    void Start()
    {
        enemyPool = new List<GameObject>(); //initialize list.
        currentPoolSize = initialPoolSize;
        InitializePool(currentPoolSize);
        StartCoroutine(IncreaseSizeOverTime());
    }

    void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        //instantiate new enemy prefab if there are none available
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(true);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    void IncreasePoolSize(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        if (currentPoolSize <= maxPoolSize)
        {
            currentPoolSize += amt;
        }
        else
        {
            return;
        }
    }

    IEnumerator IncreaseSizeOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            IncreasePoolSize(amtIncrease);
        }
    }
}
