using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int posX;
    public int posZ;
    public Unit unit = null;
    public Side owner;
    public List<Node> nodes = new List<Node>();

    public Tile pathFindParent;
    public float pathFindCostToHere = 0;

    public void AddUnit(Unit Unit)
    {
        unit = Unit;
        unit.gameObject.name = "Unit";
        unit.gameObject.transform.position = this.transform.position;
        unit.Tile = this;
    }
    public void RemoveUnit()
    {
        this.unit = null;
    }
}
