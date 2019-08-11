using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Tile startTile;
    public float cost;
    public Tile endTile;

    public Node(Tile StartTile, float Cost, Tile EndTile)
    {
        this.startTile = StartTile;
        this.cost = Cost;
        this.endTile = EndTile;

        StartTile.nodes.Add(this);
    }
}
