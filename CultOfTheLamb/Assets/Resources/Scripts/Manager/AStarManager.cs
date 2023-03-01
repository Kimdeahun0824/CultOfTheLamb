using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AStarManager : SingletonBase<AStarManager>
{
    Queue<PathRequest> pathRequestsQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    PathFinding pathFinding;

    public AGrid aGrid = default;

    bool isProcessingPath;

    public new void Awake()
    {
        base.Awake();
        aGrid = GetComponent<AGrid>();
        pathFinding = GetComponent<PathFinding>();
    }

    public void RequestPath(Vector3 pathStart, Vector3 pathEnd, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        pathRequestsQueue.Enqueue(newRequest);
        TryProcessNext();
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
    public void TryProcessNext()
    {
        if (!isProcessingPath && 0 < pathRequestsQueue.Count)
        {
            currentPathRequest = pathRequestsQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }
}

public class PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public UnityAction<Vector3[], bool> callback;

    public PathRequest(Vector3 nStart, Vector3 nEnd, UnityAction<Vector3[], bool> nCallback)
    {
        pathStart = nStart;
        pathEnd = nEnd;
        callback = nCallback;
    }
}
