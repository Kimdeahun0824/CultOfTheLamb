using UnityEngine;
using System.Collections.Generic;
public class AGrid : MonoBehaviour
{
    public LayerMask unwalkableMask = default;
    public Vector2 gridWorldSize = default;
    public float nodeRadius;
    AStarNode[,] grid = default;

    float nodeDiameter = default;
    int gridSizeX = default;
    int gridSizeY = default;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public void CreateGrid()
    {
        grid = new AStarNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        Vector3 worldPoint;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new AStarNode(walkable, worldPoint, x, y);
            }
        }
    }



    // 입력 노드에 이웃 노드(8방면)을 찾는 함수
    public List<AStarNode> GetNeighborNode(AStarNode node)
    {
        List<AStarNode> neighborNodes = new List<AStarNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (0 <= checkX && checkX < gridSizeX && 0 <= checkY && checkY < gridSizeY)
                {
                    neighborNodes.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighborNodes;
    }

    // 유니티의 WorldPosition으로 부터 그리드상의 노드를 찾는 함수
    public AStarNode GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (var node in grid)
            {
                Gizmos.color = (node.isWalkAvailable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.pos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}