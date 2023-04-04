using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinding
{
    public class Node
    {
        public Vector2Int position;
        public Node parent;
        public int gCost;
        public int hCost;

        public int FCost
        {
            get { return gCost + hCost; }
        }

        public Node(Vector2Int pos, Node parent, int gCost, int hCost)
        {
            this.position = pos;
            this.parent = parent;
            this.gCost = gCost;
            this.hCost = hCost;
        }
    }

    private Grid grid;

    public AStarPathfinding(Grid grid)
    {
        this.grid = grid;
    }

    public List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int targetPos)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        Node startNode = new Node(startPos, null, 0, GetManhattanDistance(startPos, targetPos));
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.position == targetPos)
            {
                return RetracePath(startNode, currentNode);
            }

            foreach (Vector2Int neighborPos in GetNeighborPositions(currentNode.position))
            {
                if (!grid.IsTileWalkable(neighborPos) || closedSet.Any(n => n.position == neighborPos))
                {
                    continue;
                }


                int newMovementCostToNeighbor = currentNode.gCost + GetManhattanDistance(currentNode.position, neighborPos);
                Node neighbor = new Node(neighborPos, currentNode, newMovementCostToNeighbor, GetManhattanDistance(neighborPos, targetPos));

                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    private List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }
    public class Grid
    {
        public Vector2Int gridSize;
        public TileType[,] tiles;

        public Grid(Vector2Int gridSize)
        {
            this.gridSize = gridSize;
            tiles = new TileType[gridSize.x, gridSize.y];
        }

        public bool IsTileWalkable(Vector2Int position)
        {
            // Return true if the tile at the given position is walkable, false otherwise
            return tiles[position.x, position.y] == TileType.Walkable;
        }

        public bool IsTileWithinBounds(Vector2Int position)
        {
            return position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y;
        }
    }

    public enum TileType
    {
        Walkable,
        Obstacle
    }

    private List<Vector2Int> GetNeighborPositions(Vector2Int position)
    {
        List<Vector2Int> neighborPositions = new List<Vector2Int>();

        // Add all adjacent grid cells as potential neighbors
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                Vector2Int neighborPos = new Vector2Int(position.x + x, position.y + y);
                if (grid.IsTileWithinBounds(neighborPos))
                {
                    neighborPositions.Add(neighborPos);
                }
            }
        }

        return neighborPositions;
    }

    private int GetManhattanDistance(Vector2Int posA, Vector2Int posB)
    {
        int distanceX = Mathf.Abs(posA.x - posB.x);
        int distanceY = Mathf.Abs(posA.y - posB.y);

        return distanceX + distanceY;
    }
}
