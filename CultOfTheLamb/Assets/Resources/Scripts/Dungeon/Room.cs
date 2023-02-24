using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public List<Transform> worldTriggerZone = default;
    public List<Transform> worldSpwanZone = default;
    public List<Transform> worldSizeCheck = default;
    public Vector2 worldSize = default;

    public void OnEnable()
    {
        float minX, minY, maxX, maxY;

        minX = worldSizeCheck[0].position.x;
        minY = worldSizeCheck[0].position.z;

        maxX = worldSizeCheck[1].position.x;
        maxY = worldSizeCheck[1].position.z;

        float width, height;

        if (minX < 0) minX *= -1;
        if (minY < 0) minY *= -1;

        width = minX + maxX;
        height = minY + maxY;

        worldSize = new Vector2(width, height);

        AStarManager.Instance.aGrid.gridWorldSize = worldSize;
        GameManager.Instance.SetWorldSize();
        AStarManager.Instance.aGrid.CreateGrid();
    }
}
