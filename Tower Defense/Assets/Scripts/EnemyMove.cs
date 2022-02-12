using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //Target Position
    private Transform target;

    //List of waypoints
    private GameObject[] waypoints;

    //Enemy Speed
    public float speed;

    //Current waypoint number
    private int position;

    //Called at start of script
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        position = 1;
        target = findNextWaypoint(position).transform;
        position++;
    }

    // Update is called once per frame
    void Update()
    {
        //Setting step size
        float step = speed * Time.deltaTime;

        //Setting position to move towards
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if(Vector3.Distance(transform.position, target.position) < .1f)
        {
            target = findNextWaypoint(position).transform;
            position++;
        }
    }

    private GameObject findNextWaypoint(int num)
    {
        bool destroyed = true;
        foreach(GameObject way in waypoints)
        {
            Waypoint comp = way.GetComponent<Waypoint>();
            if (num == waypoints.Length && destroyed)
            {
                destroyed = false;
                GameController gc = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameController>();
                gc.LivesLost(gameObject.GetComponent<Enemy>().damage);
                Destroy(gameObject);
            }
            if(comp.position == num)
            {
                return way;
            }
        }
        return waypoints[0];
    }
}
