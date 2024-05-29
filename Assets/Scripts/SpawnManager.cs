using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] ObjectPool pool;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float spawnXPos = 40f;
    [SerializeField] float spawnZpos = -10f;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        return new Vector3(spawnXPos, 1, spawnZpos);
    }
}
