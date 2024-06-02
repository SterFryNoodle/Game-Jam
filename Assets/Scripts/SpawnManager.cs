using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] ObjectPool pool;
    [SerializeField] float spawnInterval = 5f;

    int spawnXMinRange = 10;
    int spawnZMinRange = 90;

    int spawnXMaxRange = 15;
    int spawnZMaxRange = 100;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }   

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            //spawns enemy prefabs after they have been set active.
            GameObject enemy = pool.GetEnemy();
            enemy.transform.position = GetSpawnPosition();
        }       
    }

    Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(spawnXMinRange, spawnXMaxRange), -2.4f , Random.Range(spawnZMinRange, spawnZMaxRange));
    }
}
