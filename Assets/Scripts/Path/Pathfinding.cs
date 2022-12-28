using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class Pathfinding : MonoBehaviour
{
    private class Node {
        public Node parent;
        public bool isRoot = false;
        public bool isClosed = false;
        public Vector2Int position, origin, destination;
        public int gCost = 999999;
        public int hCost = 999999;

        public int fCost = 999999;
        public Node(Vector2Int pos)//Root Node
        {
            isRoot = true;
            gCost = 0;
            position.x = pos.x;
            position.y = pos.y;
        }
        public Node(Node p, Vector2Int pos, Vector2Int start, Vector2Int end)
        {
            parent = p;
            position = pos;
            origin = start;
            destination = end;
        }
        public void initialCost(){
             gCost = 1 + parent.gCost;//Mathf.Abs(position.x - origin.x) + Mathf.Abs(position.y - origin.y);
             hCost = Mathf.Abs(position.x - destination.x) + Mathf.Abs(position.y - destination.y);
             fCost = gCost + hCost;
        }
        public void updateCost(Node newParent){
            int newg = 1 + newParent.gCost;
            if (newg < gCost)
            {
                gCost = newg;
                fCost = gCost + hCost;
                parent = newParent;
            }
            else
            {
                return;
            }
        }
    }
    Node[] openlist = new Node[MAX_MAPSIZE * MAX_MAPSIZE];
    //Node[] closelist = new Node[MAX_MAPSIZE * MAX_MAPSIZE];
    void Start()
    {// Start is called before the first frame update

    }
    private bool checkIfPassable(Vector2Int v, MapManager map)
    {
        return map.isPassable(v.x, v.y);
    }
    private int checkIfExist(Node[] list, Vector2Int v, int size)
    {
        for (int i = 0; i < size; i++)
        {
            if(list[i].position == v)
            {
                return i;
            }
        }
        return -1;
    }
    private int findLeastCost(Node[] list, int size){
        int min = int.MaxValue, minIndex = 0;
        for(int i = 0; i < size; i++){
            if (openlist[i].isClosed == true)
                continue;
            if(openlist[i].fCost < min)
            {
                min = openlist[i].fCost;
                minIndex = i;
            }else if(openlist[i].fCost == min)
            {
                if (openlist[i].hCost < openlist[minIndex].hCost)
                {
                    minIndex = i;
                }
            }
        }
        return minIndex;
    }
    private Node traceFirstNode(Node n)
    {
        while (n.parent.isRoot != true)
        {
            n = n.parent;
        }
        return n;
    }
    public Vector3 findDirection(Vector2Int origin, MapManager map, Vector2Int des)
    {
        int opensize = 0;
        Vector3 v = new Vector3(0.0f, 0.0f, 0.0f);
        if (origin == des)
            return v;
        Vector2Int[] neighbour = {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        Node root = new Node(origin);
        while (opensize < 10000)//int.MaxValue)
        {
            int selectedIndex = 0;
            //Select a node to expand
            if (opensize < 1){   //Select root
                openlist[opensize++] = new Node(root, root.position + Vector2Int.up, origin, des);
                openlist[opensize++] = new Node(root, root.position + Vector2Int.down, origin, des);
                openlist[opensize++] = new Node(root, root.position + Vector2Int.left, origin, des);
                openlist[opensize++] = new Node(root, root.position + Vector2Int.right, origin, des);
                for(int i = 0; i < 4; i++)
                {
                    openlist[i].initialCost();
                    if (openlist[i].hCost == 0) //Found
                    {
                        Node dirNode = traceFirstNode(openlist[i]);
                        Vector3 pos; pos.x = -dirNode.position.x; pos.z = -dirNode.position.y; pos.y = 0.0f;
                        Vector3 originPos; originPos.x = -origin.x; originPos.z = -origin.y; originPos.y = 0.0f;
                        return pos - originPos;
                    }
                    //Debug.Log("Point:"+openlist[i].position+"fCost:"+openlist[i].fCost);
                }
                continue;
            }
            else{   //Select a node in openlist with least fCost>h
                selectedIndex = findLeastCost(openlist,opensize);
            }
            //Expand and Calculate cost
            //Update cost if needed
            Vector2Int selectedPos = openlist[selectedIndex].position;
            for (int i = 0; i < 4; i++)
            {
                if (checkIfPassable(selectedPos, map) != true)
                    continue;
                int existIndex = -1;
                existIndex = checkIfExist(openlist, selectedPos + neighbour[i], opensize);
                if(existIndex != -1 && openlist[existIndex].isClosed == false) //existed
                {
                    openlist[existIndex].updateCost(openlist[selectedIndex]);
                }
                else if (existIndex != -1 && openlist[existIndex].isClosed == true)
                {//closed
                    continue;
                }
                else // not existed
                {
                    
                    openlist[opensize++] = new Node(openlist[selectedIndex], selectedPos+neighbour[i], origin, des);
                    openlist[opensize-1].initialCost();
                    //Debug.Log("opensize:" + opensize + "selectedIndex:" + selectedIndex + "hCost:" + openlist[opensize-1].hCost);
                    //Check if finished
                    if (openlist[opensize - 1].hCost == 0) //Found
                    {
                        Node dirNode = traceFirstNode(openlist[opensize - 1]);
                        Vector3 pos; pos.x = -dirNode.position.x; pos.z = -dirNode.position.y; pos.y = 0.0f;
                        Vector3 originPos; originPos.x = -origin.x; originPos.z = -origin.y; originPos.y = 0.0f;
                        return pos-originPos;
                    }
                }
            }
            //Close selected Node
            openlist[selectedIndex].isClosed = true;
        }
        return v;
    }
}
