using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLookAt : MonoBehaviour
{
    //Enum to determine what target the tower will be looking at
    private enum targetType { first, last, healthiest, weakest, closest};
    private targetType currentMode;

    //All enemies currently in the scence
    private List<GameObject> allEnemies;

    private float towerRange;

    private Transform target;

    // Update is called once per frame
    void Start()
    {
        currentMode = targetType.first;
        allEnemies = new List<GameObject>();
        Tower tow = gameObject.GetComponentInParent<Tower>();
        towerRange = tow.range;
        InvokeRepeating("UpdateEnemiesList", 0f, .1f);
    }

    //Draw Tower Range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }

    //Updates the current enemies list in range of the tower
    public void UpdateEnemiesList()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        allEnemies.Clear();
        foreach(GameObject enemy in enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) < towerRange)
            {
                allEnemies.Add(enemy);
            }
        }
    }

    public Transform GetTarget()
    {
        return target;
    }

    public bool EnemiesInRange()
    {
        return (allEnemies.Count>0);
    }

    void Update()
    {
        if(allEnemies.Count == 0) { return; }

        switch (currentMode)
        {
            case targetType.first:
                target = findFirstEnemy();
                break;
            case targetType.last:
                target = findLastEnemy();
                break;
            case targetType.healthiest:
                target = findHealthiestEnemy();
                break;
            case targetType.weakest:
                target = findWeakestEnemy();
                break;
            case targetType.closest:
                target = findClosestEnemy();
                break;
        }
        if(target != null)
        {
            gameObject.transform.LookAt(target, Vector3.forward);
        }
    }

    //Find enemy that is closest to the end of the track
    public Transform findFirstEnemy()
    {
        GameObject first = allEnemies[0];

        foreach(GameObject enemy in allEnemies)
        {
            if(first == null || enemy == null)
            {
                return transform;
            }
            if (enemy.GetComponent<EnemyMove>().getPosition() > first.GetComponent<EnemyMove>().getPosition() || (enemy.GetComponent<EnemyMove>().getPosition() == first.GetComponent<EnemyMove>().getPosition() && first.GetComponent<EnemyMove>().distanceToWaypoint() > enemy.GetComponent<EnemyMove>().distanceToWaypoint()))
            {
                first = enemy;
            }
        }

        return first.transform;
    }

    //Find enemy that is closest to the start of the track
    public Transform findLastEnemy()
    {
        GameObject last = allEnemies[0];

        foreach (GameObject enemy in allEnemies)
        {
            if (last == null)
            {
                return transform;
            }
            if (enemy.GetComponent<EnemyMove>().getPosition() < last.GetComponent<EnemyMove>().getPosition() || (enemy.GetComponent<EnemyMove>().getPosition() == last.GetComponent<EnemyMove>().getPosition() && last.GetComponent<EnemyMove>().distanceToWaypoint() < enemy.GetComponent<EnemyMove>().distanceToWaypoint()))
            {
                last = enemy;
            }
        }
        return last.transform;
    }

    //Find highest health enemy
    public Transform findHealthiestEnemy()
    {
        GameObject healthiest = allEnemies[0];

        foreach(GameObject enemy in allEnemies)
        {
            if (healthiest == null)
            {
                return transform;
            }
            if (enemy.GetComponent<Enemy>().health > healthiest.GetComponent<Enemy>().health)
            {
                healthiest = enemy;
            }
        }
        return healthiest.transform;
    }

    //Find lowest health enemy
    public Transform findWeakestEnemy()
    {
        GameObject weakest = allEnemies[0];

        foreach (GameObject enemy in allEnemies)
        {
            if (weakest == null)
            {
                return transform;
            }
            if (enemy.GetComponent<Enemy>().health < weakest.GetComponent<Enemy>().health)
            {
                weakest = enemy;
            }
        }
        return weakest.transform;
    }

    //Find enemy that is closest to the tower object
    public Transform findClosestEnemy()
    {
        GameObject closest = allEnemies[0];

        foreach(GameObject enemy in allEnemies)
        {
            if (closest == null)
            {
                return transform;
            }
            if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < Vector3.Distance(gameObject.transform.position, closest.transform.position))
            {
                closest = enemy;
            }
        }

        return gameObject.transform;
    }
}
