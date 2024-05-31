using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private List<Transform> enemies = new List<Transform>();
    private List<Transform> allies = new List<Transform>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy(Transform enemy)
    {
        if(!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }        
    }    

    public void UnRegisterEnemy(Transform enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }        
    }
    
    public Transform GetClosestEnemy(Transform currentTransform)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach(Transform enemy in enemies)
        {
            float distance = Vector3.Distance(currentTransform.position, enemy.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public Transform GetClosestAlly(Transform currentTransform)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform ally in allies)
        {
            float distance = Vector3.Distance(currentTransform.position, ally.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = ally;
            }
        }

        return closestEnemy;
    }

    public void RegisterAlly(Transform ally)
    {
        if ((ally.CompareTag("Player") || ally.CompareTag("Survivor")) && !allies.Contains(ally))
        {
            allies.Add(ally);
        }
    }

    public void UnRegisterAlly(Transform ally)
    {
        if ((ally.CompareTag("Player") || ally.CompareTag("Survivor")) && allies.Contains(ally))
        {
            allies.Add(ally);
        }
    }
}
