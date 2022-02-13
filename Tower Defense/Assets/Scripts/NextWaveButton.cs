using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    public EnemySpawner es;

    void Update()
    {
        if (es.spawnFinished())
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void onClick()
    {
        es.findWave(es.level, es.stage);
    }
}
