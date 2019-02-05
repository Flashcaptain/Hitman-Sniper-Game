﻿﻿﻿﻿﻿﻿﻿using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool IsWalkable;
    public int WeightToStartNode;
    public int GridX;
    public int GridY;
    public int WeightToEndNode;
    public Node Parent;
    public Vector3 WorldPosition;

    public int HeapIndex { get; set; }

    public int TotalCost
    {
        get { return WeightToStartNode + WeightToEndNode; }
    }

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        IsWalkable = walkable;
        WorldPosition = worldPos;
        GridX = gridX;
        GridY = gridY;
    }

    public int CompareTo(Node nodeToCompare)
    {
        var compare = TotalCost.CompareTo(nodeToCompare.TotalCost);
        if (compare == 0)
        {
            compare = WeightToEndNode.CompareTo(nodeToCompare.WeightToEndNode);
        }
        return -compare;
    }

    public int GetDistance(Node target)
    {
        var distanceX = Mathf.Abs(GridX - target.GridX);
        var distanceY = Mathf.Abs(GridY - target.GridY);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   