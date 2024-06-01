using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] ObjectPool pool;
    [SerializeField] float spawnInterval = 5f;
    
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
        return new Vector3(Random.Range(10,15), -2.4f , Random.Range(90,100));
    }
}
