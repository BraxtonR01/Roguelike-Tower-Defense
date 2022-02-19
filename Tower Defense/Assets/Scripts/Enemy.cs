using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy health, damage to player, and money reward for killing
    public float health;
    public int damage;
    public int killReward;

    private GameController gc;

    void Start()
    {
        gc = GameObject.Find("Node Holder").GetComponent<GameController>();        
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            gc.AddMoney(killReward);
            Destroy(gameObject);
        }
    }
}
