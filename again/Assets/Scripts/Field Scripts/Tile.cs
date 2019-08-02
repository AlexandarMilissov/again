using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int posX;
    private int posZ;
    public Unit unit = null;

    public int PosX
    {
        get
        {
            return posX;
        }
    }
    public int PosZ
    {
        get
        {
            return posZ;
        }
    }

    public Tile(int X, int Z)
    {
        posX = X;
        posZ = Z;
    }

    public void AddUnit(Unit unit)
    {
        this.unit = unit;
    }
    public void RemoveUnit()
    {
        this.unit = null;
    }

}
