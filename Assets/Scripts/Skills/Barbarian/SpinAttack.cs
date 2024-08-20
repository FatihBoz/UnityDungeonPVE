using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : Skill, IPrimarySkill
{
    private Barbarian barbarian;

    private void Awake()
    {
        barbarian = GetComponent<Barbarian>();    
    }

    public void CastPrimarySkill()
    {
        if (canUse && !barbarian.isCasting)
        {
            barbarian.anim.PlayAnimation(AnimationKey.PRIMARY_SKILL);
            StartCoroutine(Cooldown());
        }
    }
}
