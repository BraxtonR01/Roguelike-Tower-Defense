using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;
public class Wave
{
    //Level contains 15 stages
    public int level;
    public int stage;

    public float waitTime;

    public List<GameObject> allEnemies;
    public List<GameObject> waveEnemies;

    //Incremental ID
    static private int nextID;
    public int id;

    //Blank constructor for use of setting up enemy list
    public Wave(List<GameObject> allE) {
        allEnemies = allE;
    }

    public Wave(int lev, int sta, List<GameObject> e, float wait)
    {
        level = lev;
        stage = sta;
        waveEnemies = e;
        id = Interlocked.Increment(ref nextID);
        waitTime = wait;
    }

    public List<Wave> SetUpEnemyList()
    {
        GameObject baseEnemy = null;
        GameObject fastEnemy = null;
        GameObject tankEnemy = null;

        //Looping through enemies
        foreach (GameObject e in allEnemies)
        {
            switch (e.name)
            {
                case "Base Enemy":
                    baseEnemy = e;
                    break;
                case "Fast Enemy":
                    fastEnemy = e;
                    break;
                case "Tank Enemy":
                    tankEnemy = e;
                    break;
            }
        }

        //Initializing List
        List<Wave> spawnList = new List<Wave>();

        //Adding Waves
        //Round 1 Stage 1
        spawnList.Add(new Wave(1, 1, new List<GameObject> { baseEnemy, baseEnemy, baseEnemy, baseEnemy, baseEnemy }, .5f));
        spawnList.Add(new Wave(1, 1, new List<GameObject> { baseEnemy, baseEnemy, fastEnemy, fastEnemy}, 1f));
        spawnList.Add(new Wave(1, 1, new List<GameObject> { tankEnemy, baseEnemy, fastEnemy }, 1f));

        //Returning List
        return spawnList;
    }
}
