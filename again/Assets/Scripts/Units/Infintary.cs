using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infintary : Unit
{
    public Infintary(Tile tile)
    {
        this.Tile = tile;
        this.Armor = Armor.light ;
        this.Mount = null;
        this.Weapon = new Sword();
        this.HP = 100;
        this.baseSpeed = 2;
    }
}
