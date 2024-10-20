using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PathfindingGrid : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _gridSize = new Vector2Int(10, 10);
    public PathfindingNode[,] pathfindingGraph = new PathfindingNode[0,0];
    public bool[,] blockedNodes = new bool[0,0];

    private Matrix4x4 _worldToGrid;


    [SerializeField]
    private float cellSize = 1f;

    [SerializeField]
    private LayerMask obstacleLayers;

    [SerializeField]
    private bool _debug;

    
    private void Awake()
    {
        _worldToGrid = Matrix4x4.identity;
        _worldToGrid.SetTRS(-((Vector2)transform.position - _gridSize / 2), Quaternion.identity, new Vector3(1/cellSize, 1/cellSize, 1));

        pathfindingGraph = new PathfindingNode[_gridSize.x, _gridSize.y];
        blockedNodes = new bool[_gridSize.x, _gridSize.y];

        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                pathfindingGraph[i, j] = new PathfindingNode(pos, 0, 0, new Vector2Int(-1, -1));
                blockedNodes[i, j] = Physics2D.OverlapBox(_worldToGrid.inverse.MultiplyPoint(new Vector3(i, j)), new Vector2(cellSize, cellSize), 0, obstacleLayers);
            }
        }

    }


    public void ResetGraph()
    {
        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                pathfindingGraph[i, j] = new PathfindingNode(pos, 0, 0, new Vector2Int(-1, -1));
            }
        }
    }

    public void ResetNode(Vector2Int node)
    {
        pathfindingGraph[node.x, node.y] = new PathfindingNode(node, 0, 0, new Vector2Int(-1, -1));
    }


    public bool IsBlocked(Vector2Int pos)
    {
        if(IsOutsideOfGrid(pos))
            return false;

        return blockedNodes[pos.x, pos.y];
    }

    public bool IsOutsideOfGrid(Vector2Int pos)
    {
        return pos.x < 0 || pos.x >= _gridSize.x || pos.y < 0 || pos.y >= _gridSize.y;
    }

    public Vector2Int GetNodeFromWorldPos(Vector2 pos)
    {
        Vector2 c = (Vector2)_worldToGrid.MultiplyPoint(pos);
        return new Vector2Int(Mathf.RoundToInt(c.x), Mathf.RoundToInt(c.y));
    }

    public Vector2 GetWorldPosFromGridPos(Vector2Int pos)
    {
        return _worldToGrid.inverse.MultiplyPoint((Vector2)pos);
    }


    public List<PathfindingNode> GetNeighbours(Vector2Int nodePos)
    {
        List<PathfindingNode> neighbours = new List<PathfindingNode>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                    continue;
                Vector2Int newPos = new Vector2Int(nodePos.x + x, nodePos.y + y);

                if(!IsOutsideOfGrid(newPos) && !blockedNodes[newPos.x, newPos.y])
                    neighbours.Add(pathfindingGraph[newPos.x, newPos.y]);
            }
        }

        return neighbours;
    }

    private void OnDrawGizmosSelected()
    {
        if(!_debug) return;

        for(int i = 0; i < pathfindingGraph.GetLength(0); i++)
        {
            for(int j = 0; j < pathfindingGraph.GetLength(1); j++)
            {
                if (blockedNodes[i, j])
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.white;

                Gizmos.DrawWireCube(_worldToGrid.inverse.MultiplyPoint(new Vector3(i, j)), new Vector2(cellSize, cellSize));
            }
        }
    }
}


public struct PathfindingNode
{
    public int gCost;
    public int hCost;

    public Vector2Int gridPosition;
    public Vector2Int parentPosition;


    public int FCost
    {
        get => gCost + hCost;
    }

    public bool visited => FCost > 0;


    public PathfindingNode(Vector2Int gridPos, int G, int H, Vector2Int parentPos)
    {
        this.gridPosition = gridPos;
        this.parentPosition = parentPos;

        gCost = G;
        hCost = H;
        //FCost = G + H;


    }
}