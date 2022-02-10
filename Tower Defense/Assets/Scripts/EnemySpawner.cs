using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //List of enemy waves depending on round and stage
    public List<Wave> spawnList;
    public List<GameObject> allEnemies;

    //Potential waves for given round and stage
    public List<Wave> potentialWaves = new List<Wave>();

    // Start is called before the first frame update
    void Start()
    {
        spawnList = new Wave(allEnemies).SetUpEnemyList();
        findWave(1, 1);
    }

    public void findWave(int level, int stage)
    {
        //Loop through spawnList
        foreach (Wave w in spawnList)
        {
            if(w.level == level && w.stage == stage)
            {
                potentialWaves.Add(w);
            }
        }
        //RNG to find a wave to spawn
        int rand = Random.Range(0, potentialWaves.Count);

        //Looping through chosen wave
        StartCoroutine(SpawnWave(potentialWaves[rand]));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        float wait = wave.waitTime;
        foreach(GameObject e in wave.waveEnemies)
        {
            Instantiate(e, transform.position, new Quaternion(0, 0, 0, 0));
            yield return new WaitForSeconds(wait);
        }
    }
}
