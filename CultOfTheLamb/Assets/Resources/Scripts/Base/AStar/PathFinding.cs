using UnityEngine;
using System.Collections.Generic;


public class PathFinding : MonoBehaviour
{
    AGrid grid;

    public Transform startObject;
    public Transform targetObject;

    private void Awake()
    {
        grid = GameObject.Find("AStarManager").GetComponent<AGrid>();

    }

    private void Update()
    {
        FindPath(startObject.position, targetObject.position);

    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        AStarNode startNode = grid.GetNodeFromWorldPoint(startPos);
        AStarNode targetNode = grid.GetNodeFromWorldPoint(targetPos);

        List<AStarNode> openList = new List<AStarNode>();
        HashSet<AStarNode> closedList = new HashSet<AStarNode>();
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            AStarNode currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (AStarNode n in grid.GetNeighborNode(currentNode))
            {
                if (!n.isWalkAvailable || closedList.Contains(n))
                {
                    continue;
                }

                int newCurrentToNeighborNodeCost = currentNode.gCost + GetDistanceCost(currentNode, n);
                if (newCurrentToNeighborNodeCost < n.gCost || !openList.Contains(n))
                {
                    n.gCost = newCurrentToNeighborNodeCost;
                    n.hCost = GetDistanceCost(n, targetNode);
                    n.parentNode = currentNode;

                    if (!openList.Contains(n))
                    {
                        openList.Add(n);
                    }
                }
            }
        }

    }

    void RetracePath(AStarNode startNode, AStarNode endNode)
    {
        List<AStarNode> path = new List<AStarNode>();
        AStarNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();
        grid.path = path;
    }

    int GetDistanceCost(AStarNode nodeA, AStarNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }
}