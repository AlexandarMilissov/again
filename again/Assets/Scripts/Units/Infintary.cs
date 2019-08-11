using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Infintary : Unit
{
    public Infintary(Tile tile, Side side)
    {
        this.Tile = tile;
        this.Side = side;
        this.Armor = Armor.light ;
        this.Mount = null;
        this.Weapon = new Sword();
        this.HP = 100;
        this.baseSpeed = 2;
    }
}
