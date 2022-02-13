using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //List of enemy waves depending on round and stage
    public List<Wave> spawnList;
    public List<GameObject> allEnemies;

    //Potential waves for given round and stage
    public List<Wave> potentialWaves = new List<Wave>();

    //15 stages in 1 level
    public int level;
    public int stage;

    // Start is called before the first frame update
    void Start()
    {
        spawnList = new Wave(allEnemies).SetUpEnemyList();
        level = 1;
        stage = 1;

        GameObject.Find("Next Wave").GetComponent<NextWaveButton>().es = this;
    }

    public bool spawnFinished()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length != 0)
        {
            return false;
        }
        else { return true; }
    }

    public void findWave(int lev, int sta)
    {
        //Loop through spawnList
        foreach (Wave w in spawnList)
        {
            if(w.level == lev && w.stage == sta)
            {
                potentialWaves.Add(w);
            }
        }
        //RNG to find a wave to spawn
        int rand = Random.Range(0, potentialWaves.Count);

        //Looping through chosen wave
        StartCoroutine(SpawnWave(potentialWaves[rand]));
        potentialWaves.Clear();
        GameObject.FindGameObjectWithTag("Finish").GetComponent<GameController>().StageChange(level, stage);
        stage += 1;
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
