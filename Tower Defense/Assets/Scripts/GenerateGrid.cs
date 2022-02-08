using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    //Grid tracking, and its height and width
    public int height;
    public int width;

    //Nodes that turrets can be placed on, path that enemies follow, enemy start, and enemy end
    public GameObject nodeObject;
    public GameObject pathObject;
    public GameObject startObj;
    public GameObject endObj;

    //Start of enemy spawn and enemy end
    private int[] start;
    private int[] finish;
    public float space;

    //Total positions that can be moved from each square
    private List<int[]> positions = new List<int[]>();

    //Max and min path lengths
    public int min;
    public int max;

    //List of all unavailable positions
    private List<int[]> usedPos = new List<int[]>();

    //List of current path
    private List<int[]> currentPath = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        //Assignment of all private vars

        start = new int[2];
        start[0] = 0;
        start[1] = height - 1;

        finish = new int[2];
        finish[0] = width - 1;
        finish[1] = 0;

        positions.Add(new int[] { -1, 0 });
        positions.Add(new int[] { 1, 0 });
        positions.Add(new int[] { 0, 1 });
        positions.Add(new int[] { 0, -1 });

        usedPos.Add(start);
        currentPath.Add(start);

        AStar a = new AStar();
        findPath(start, finish, min, max, 0);
        currentPath.AddRange(a.finishPath(width, height, currentPath[currentPath.Count-1], finish, usedPos));

        createMap(currentPath, space);
        
    }

    //Creates visual map on screen including path and node positions
    public void createMap(List<int[]> path, float spacing = 1.2f)
    {
        //Transforms and Objects
        Quaternion rotation = new Quaternion(0, 0, 0, 1);
        GameObject holder = gameObject;
        GameObject node;
        
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y<height; y++)
            {
                //Setting proper positioning
                Vector3 newTrans = new Vector3(holder.transform.position.x + x * spacing, holder.transform.position.y + y * spacing, 0);
                Vector3 seTrans = new Vector3(holder.transform.position.x + x * spacing, holder.transform.position.y + y * spacing, -.5f);

                bool found = false;

                foreach (int[] pos in path)
                {
                    if (x == pos[0] && y == pos[1])
                    {
                        found = true;
                    }
                }
                //Instantiate node or path depending on if used in path list or not
                if (found)
                {
                    node = Instantiate(pathObject, newTrans, rotation);
                }
                else
                {
                    node = Instantiate(nodeObject, newTrans, rotation);
                }
                node.transform.SetParent(holder.transform);
                if (x == start[0] && y == start[1])
                {
                    Instantiate(startObj, seTrans, rotation);
                }
                else if (x == finish[0] && y == finish[1])
                {
                    Instantiate(endObj, seTrans, rotation);
                }
            }
        }
    }

    //"Randomly" generated path to a random point from the start point on the grid that is the min length
    public List<int[]> findPath(int[] currPos, int[] finish, int minLength, int maxLength, int pathLength)
    {
        if (pathLength < minLength)
        {
            //Array of potential positions
            List<int[]> potential = new List<int[]>();

            //Loop through each position (4)
            for (int x = 0; x < 4; x++)
            {
                bool found = false;

                //New position tracker
                int[] newPos = new int[2];

                //Finding new positions
                newPos[0] = currPos[0] + positions[x][0];
                newPos[1] = currPos[1] + positions[x][1];
                
                //Checking if within correct ranges
                if (newPos[0] < width && newPos[0] >= 0 && newPos[1] < height && newPos[1] >= 0)
                {
                    foreach (int[] item in usedPos)
                    {
                        if(currPos[0] + positions[x][0] == item[0] && currPos[1] + positions[x][1] == item[1])
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        potential.Add(new int[] { currPos[0] + positions[x][0], currPos[1] + positions[x][1] });
                    }
                    else { continue; }
                }
                else { continue; }
            }

            //Finding num of potential positions
            int len = potential.Count;

            //RNG
            int rand = Random.Range(0, len);

            //Adding position to current path and used path
            currentPath.Add(potential[rand]);
            usedPos.Add(potential[rand]);

            //Find other points around prior position to prevent overlap
            findSurrounding(currPos);

            //Increment path length
            pathLength++;

            //Clearing potential positions
            potential.Clear();

            //Recursively return new current position
            return findPath(currentPath[currentPath.Count - 1], finish, minLength, maxLength, pathLength);
        }
        return currentPath;
    }

    //Find all surrounding points arround a position
    public void findSurrounding(int[] pos)
    {
        for (int x = 0; x < 4; x++)
        {
            bool found = false;

            //Finding if used already
            foreach (int[] item in usedPos)
            {
                if (pos[0] + positions[x][0] == item[0] && pos[1] + positions[x][1] == item[1])
                {
                    found = true;
                }
            }
            if (!found)
            {
                usedPos.Add(new int[] { pos[0] + positions[x][0], pos[1] + positions[x][1] });
            }
            else { continue; }

        }
    }

}

