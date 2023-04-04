using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    public Grid grid;
    public AStarPathfinding pathfinder;

    void Start()
    {
        Vector2Int startPos = new Vector2Int(0, 0);
        Vector2Int targetPos = new Vector2Int(10, 10);
        List<Vector2Int> path = pathfinder.FindPath(startPos, targetPos);

        // Do something with the path
    }
}
