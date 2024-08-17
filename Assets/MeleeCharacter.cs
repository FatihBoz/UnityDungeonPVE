
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeCharacter : Character //Melee character-specific implementation
{
    protected Weapon weapon;

    protected override void Awake()
    {
        base.Awake();
        weapon = GetComponentInChildren<Weapon>();
    }



    public override void OnAttackEnd()
    {
        weapon.OnAttackEnd();
    }

    public override void OnAttackStart()
    {
        weapon.OnAttackStart();
    }

    public abstract IEnumerator Buff(Buff b);
}
