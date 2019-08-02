using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Armor
{
    none = -1,
    light = 0,
    medium = 1,
    heavy = 2
}

public abstract class Unit : MonoBehaviour
{
    public Tile Tile;
    public Weapon Weapon;
    public Mount Mount;
    public Armor Armor;
    public int HP;
    protected int baseSpeed;
    public int Speed
    {
        get
        {
            if(Mount == null)
            {
                return baseSpeed - (int)Armor;
            }
            else
            {
                return (int)Mount.speed - (int)Armor;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(Armor == Armor.none)
        {
            HP -= (int)(1.3 * damage);
        }
        else
        {
            HP -= (int)(damage - damage*((int)(Armor)/5));
        }

        if(HP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        this.Tile.RemoveUnit();
        Destroy(this.gameObject);
    }
    public void Attack(Unit unit)
    {
        Weapon.Attack(unit);
    }
}
