using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{

    public override void OnAttackEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackStart()
    {
        throw new System.NotImplementedException();
    }

    public override void OnBasicAttackCasted()
    {
        CharacterAnimation.Instance.SetTrigger(AnimationKey.BASIC_ATTACK);
    }

    public override void OnPrimarySkillCasted()
    {
        primarySkill?.CastPrimarySkill();
    }

    public override void OnSecondarySkillCasted()
    {
        throw new System.NotImplementedException();
    }
    
}
