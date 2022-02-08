using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private int[,] grid;

    public List<int[]> finishPath(int width, int height, int[] start, int[] finish, List<int[]> used)
    {
        grid = new int[width, height];

        //Set up grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0;
            }
        }

        //Setting used terrain as unwalkable
        foreach (int[] spot in used)
        {
            if (spot[0] >= 0 && spot[0] < width && spot[1] >= 0 && spot[1] < height)
            {
                grid[spot[0], spot[1]] = 1;
            }
        }

        //Initialize start and end points
        Node start_node = new Node(null, start);
        Node end_node = new Node(null, finish);

        //Initialize open and closed lists
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        //Add start node to list
        openList.Add(start_node);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            //Find best node available in open list
            foreach (Node n in openList)
            {
                if (n.f < currentNode.f)
                {
                    currentNode = n;
                }
                else if (n.f == currentNode.f)
                {
                    if (n.g < currentNode.g)
                    {
                        currentNode = n;
                    }
                }
            }

            //Move best node from open to closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //Check if finish found
            if (currentNode.position[0] == finish[0] && currentNode.position[1] == finish[1])
            {
                List<int[]> path = new List<int[]>();

                //Trace through parents until none left
                while (currentNode != null)
                {
                    path.Add(currentNode.position);
                    currentNode = currentNode.parent;
                }

                //Return reversed path
                path.Reverse();
                path.RemoveAt(0);
                return path;
            }

            //Track children nodes
            List<Node> children = new List<Node>();

            //List of all available changes
            List<int[]> positions = new List<int[]>();
            positions.Add(new int[] { -1, 0 });
            positions.Add(new int[] { 1, 0 });
            positions.Add(new int[] { 0, 1 });
            positions.Add(new int[] { 0, -1 });

            //Checking all adjacent squares
            foreach (int[] pos in positions)
            {
                //Find new node position
                int[] newPos = new int[] { currentNode.position[0] + pos[0], currentNode.position[1] + pos[1] };

                //Check valid position
                if (newPos[0] >= width || newPos[0] < 0 || newPos[1] >= height || newPos[1] < 0)
                {
                    continue;
                }

                //Check if unwalkable
                if(grid[newPos[0], newPos[1]] == 1){
                    continue;
                }
                //Create new node with parent of current node
                Node newNode = new Node(currentNode, newPos);

                //Append to children
                children.Add(newNode);
            }
            //Iterating through new children nodes
            foreach (Node child in children)
            {
                bool found = false;

                //Checking closed list for node
                foreach (Node close in closedList)
                {
                    if (close.position[0] == child.position[0] && close.position[1] == child.position[1])
                    {
                        found = true;
                    }
                }
                if (found) { continue; }

                //Checking open list for node
                foreach (Node open in openList)
                {
                    if (open.position[0] == child.position[0] && open.position[1] == child.position[1])
                    {
                        found = true;
                    }
                }
                if (found) { continue; }

                child.g = currentNode.g + 10;
                child.h = (Mathf.Abs(child.position[0] - end_node.position[0]) + Mathf.Abs(child.position[1] - end_node.position[1])) * 10;
                child.f = child.g + child.h;

                openList.Add(child);
            }
        }
        return null;
    }
}

//Node Class
public class Node
{
    public int[] position;
    public Node parent;

    //g = distance from start
    public double g;
    //f = g+h
    public double f;
    //h = heuristic
    public double h;

    public Node(Node parent = null, int[] position = null)
    {
        this.g = 0;
        this.f = 0;
        this.h = 0;
        this.parent = parent;
        this.position = position;
    }

}