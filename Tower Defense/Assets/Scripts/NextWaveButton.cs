using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveButton : MonoBehaviour
{
    public EnemySpawner es;

    public void onClick()
    {
        if (es.spawnFinished())
        {
            es.findWave(es.level, es.stage);
        }
    }
}
