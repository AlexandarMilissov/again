using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public int AttackDamage;
    public int AttackRange;
    public abstract void Attack(Unit unit);
}
