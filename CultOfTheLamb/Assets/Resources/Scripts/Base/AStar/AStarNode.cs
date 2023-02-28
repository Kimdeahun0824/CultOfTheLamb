using UnityEngine;

public class AStarNode
{
    public bool isWalkAvailable = default;
    public Vector3 pos = default;
    public int gridX = default;
    public int gridY = default;
    public int gCost = default;
    public int hCost = default;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public AStarNode parentNode = default;
    public AStarNode(bool isWalkAvailable_, Vector3 pos_, int gridX_, int gridY_)
    {
        this.isWalkAvailable = isWalkAvailable_;
        this.pos = pos_;
        this.gridX = gridX_;
        this.gridY = gridY_;
    }
}
