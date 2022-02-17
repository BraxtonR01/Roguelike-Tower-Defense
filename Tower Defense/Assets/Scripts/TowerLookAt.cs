using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLookAt : MonoBehaviour
{
    //Enum to determine what target the tower will be looking at
    private enum targetType { first, last, healthiest, weakest, closest};
    private targetType currentMode;

    private EnemySpawner es;

    //All enemies currently in the scence
    private List<GameObject> allEnemies;

    // Update is called once per frame
    void Start()
    {
        currentMode = targetType.first;
        allEnemies = new List<GameObject>();
        es = GameObject.Find("Spawn").GetComponent<EnemySpawner>();
    }
    void Update()
    {
        if (es.spawnFinished())
        {
            return;
        }

        allEnemies.Clear();
        allEnemies.AddRange(GameObject.FindGameObjectsWithTag("enemy"));

        switch (currentMode)
        {
            case targetType.first:
                gameObject.transform.LookAt(findFirstEnemy(), Vector3.forward);
                break;
            case targetType.last:
                gameObject.transform.LookAt(findLastEnemy(), Vector3.forward);
                break;
            case targetType.healthiest:
                gameObject.transform.LookAt(findHealthiestEnemy(), Vector3.forward);
                break;
            case targetType.weakest:
                gameObject.transform.LookAt(findWeakestEnemy(), Vector3.forward);
                break;
            case targetType.closest:
                gameObject.transform.LookAt(findClosestEnemy(), Vector3.forward);
                break;
        }
    }

    //Find enemy that is closest to the end of the track
    public Transform findFirstEnemy()
    {
        GameObject first = allEnemies[0];

        foreach(GameObject enemy in allEnemies)
        {
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
            if(enemy.GetComponent<Enemy>().health > healthiest.GetComponent<Enemy>().health)
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
            if(Vector3.Distance(gameObject.transform.position, enemy.transform.position) < Vector3.Distance(gameObject.transform.position, closest.transform.position))
            {
                closest = enemy;
            }
        }

        return gameObject.transform;
    }
}
