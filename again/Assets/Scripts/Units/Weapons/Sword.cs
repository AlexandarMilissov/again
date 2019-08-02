using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Sword()
    {
        this.AttackDamage = 20;
        this.AttackRange = 1;
    }

    public override void Attack(Unit unit)
    {
        unit.TakeDamage(AttackDamage);
    }
}
