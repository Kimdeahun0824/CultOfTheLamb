using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private AGrid grid;

    private void Awake()
    {
        grid = GameObject.Find("AStarManager").GetComponent<AGrid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        Debug.Log($"StartFindPath");
        StartCoroutine(FindPath(startPos, targetPos));
    }

    private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        AStarNode startNode = grid.GetNodeFromWorldPoint(startPos);
        AStarNode targetNode = grid.GetNodeFromWorldPoint(targetPos);
        AStarNode endNode = default;
        if (startNode.isWalkAvailable && targetNode.isWalkAvailable)
        {
            List<AStarNode> openList = new List<AStarNode>();
            HashSet<AStarNode> closedList = new HashSet<AStarNode>();
            openList.Add(startNode);

            while (0 < openList.Count)
            {
                AStarNode currentNode = openList[0];

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost <= currentNode.fCost && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    endNode = currentNode;
                    break;
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
        yield return null;

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, endNode);
        }

        AStarManager.Instance.FinishedProcessingPath(waypoints, pathSuccess);

    }

    private Vector3[] RetracePath(AStarNode startNode, AStarNode endNode)
    {
        List<AStarNode> path = new List<AStarNode>();
        AStarNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    private Vector3[] SimplifyPath(List<AStarNode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = default;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 dircetionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (dircetionNew != directionOld)
            {
                waypoints.Add(path[i].pos);
            }
            directionOld = dircetionNew;
        }
        return waypoints.ToArray();
    }

    private int GetDistanceCost(AStarNode nodeA, AStarNode nodeB)
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