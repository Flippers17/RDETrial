using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private PathfindingGrid _grid;

    public PathfindingGrid Grid { get => _grid; }

    [SerializeField]
    private int _maxSearches = 1000;

    private NodeQueue _availableNodes = new NodeQueue();


    public Vector2[] GetPath(Vector2 startPos, Vector2 endPos)
    {
        _availableNodes = new NodeQueue();
        //_grid.ResetGraph();
        List<Vector2> path = new List<Vector2>();
        List<Vector2Int> visitedNodes = new List<Vector2Int>();

        Vector2Int s = _grid.GetNodeFromWorldPos(startPos);
        Vector2Int e = _grid.GetNodeFromWorldPos(endPos);

        if (_grid.IsOutsideOfGrid(s) || _grid.IsOutsideOfGrid(e))
        {
            return new Vector2[] { startPos };
        }





        PathfindingNode startNode = new PathfindingNode(s, 0, CalculateHeuristic(s, e), new Vector2Int(-1, -1));
        _grid.pathfindingGraph[s.x, s.y] = startNode;

        PathfindingNode currentNode = startNode;
        visitedNodes.Add(s);




        for(int i = 0; i < _maxSearches; i++)
        {
            //Seems to work
            List<PathfindingNode> currentNeighbours = _grid.GetNeighbours(currentNode.gridPosition);

            for(int j = 0; j < currentNeighbours.Count; j++)
            {
                int g = currentNode.gCost + 1;
                int h = CalculateHeuristic(currentNeighbours[j].gridPosition, e);

                PathfindingNode updatedNode = new PathfindingNode(currentNeighbours[j].gridPosition, g, h, currentNode.gridPosition);

                if (BetterPathFound(currentNeighbours[j], updatedNode))
                {
                    _availableNodes.InsertNode(updatedNode);
                    _grid.pathfindingGraph[updatedNode.gridPosition.x, updatedNode.gridPosition.y] = updatedNode;
                    visitedNodes.Add(updatedNode.gridPosition);
                }
            }

            if(_availableNodes.HasAvailableNodes())
                currentNode = _availableNodes.PopSmallestF();
            else
                return new Vector2[] { startPos };
            //path.Add(_grid.GetWorldPosFromGridPos(currentNode.gridPosition));

            if(currentNode.gridPosition == e)
            {
                break;
            }
        }






        Vector2Int current = e;
        for(int i = 0; i < _maxSearches; i++)
        {
            if (current.x < 0)
                break;

            path.Add(_grid.GetWorldPosFromGridPos(current));
            //Vector2Int temp = current;
            current = _grid.pathfindingGraph[current.x, current.y].parentPosition;
            //_grid.ResetNode(current);
        }

        foreach(Vector2Int n in visitedNodes)
        {
            _grid.ResetNode(n);
        }


        Vector2[] finalPath = new Vector2[path.Count];
        for(int i = path.Count - 1; i >= 0; i--)
        {
            finalPath[path.Count -1 - i] = path[i];
        }

        return finalPath;
    }


    private bool BetterPathFound(PathfindingNode current, PathfindingNode updated)
    {
        return (!current.visited || current.FCost > updated.FCost);
    }

    public int CalculateHeuristic(Vector2Int pos, Vector2Int endPos)
    {
        return Mathf.Abs(pos.x - endPos.x) + Mathf.Abs(pos.y - endPos.y);
    }
}



