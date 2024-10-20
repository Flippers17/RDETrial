using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeQueue
{

    private PathfindingNode[] _nodes = new PathfindingNode[1024];
    
    private int _heapSize = 0;

    public void InsertNode(PathfindingNode node)
    {
        if(_heapSize + 1 >= _nodes.Length)
        {
            _nodes[_nodes.Length - 1] = node;
        }
        else
        {
            _nodes[_heapSize + 1] = node;
            _heapSize++;
        }
        

        HeapifyBottomToTop(_heapSize);
    }

    private void RemoveTopElement()
    {
        _nodes[1] = _nodes[_heapSize];
        _heapSize--;

        HeapifyTopToBottom(1);

    }

    public PathfindingNode PopSmallestF()
    {
        PathfindingNode node = _nodes[1];
        RemoveTopElement();
        return node;
    }

    public bool HasAvailableNodes()
    {
        return _heapSize >= 1;
    }

    private void HeapifyBottomToTop(int index)
    {
        if(index <= 1)
            return;

        int parent = index / 2;

        if (_nodes[index].FCost < _nodes[parent].FCost)
        {
            PathfindingNode temp = _nodes[parent];
            _nodes[parent] = _nodes[index];
            _nodes[index] = temp;

            HeapifyBottomToTop(parent);
        }
    }

    private void HeapifyTopToBottom(int index)
    {
        int left = index * 2;
        int right = (index * 2) + 1;
        int smallestChild = 0;

        if (left > _heapSize)
            return;
        else if (left == _heapSize && _nodes[left].FCost < _nodes[index].FCost)
        {
            PathfindingNode temp = _nodes[index];
            _nodes[index] = _nodes[left];
            _nodes[left] = temp;

            HeapifyTopToBottom(left);
            return;
        }

        if (_nodes[left].FCost < _nodes[right].FCost)
            smallestChild = left;
        else
            smallestChild = right;

        if (_nodes[smallestChild].FCost < _nodes[index].FCost)
        {
            PathfindingNode temp = _nodes[index];
            _nodes[index] = _nodes[smallestChild];
            _nodes[smallestChild] = temp;

            HeapifyTopToBottom(smallestChild);
            return;
        }
    }
}
