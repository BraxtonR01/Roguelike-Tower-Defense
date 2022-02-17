using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    private EnemySpawner es;

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
    public void setES(EnemySpawner e)
    {
        es = e;
    }
    public void onClick()
    {
        es.findWave(es.level, es.stage);
    }
}
