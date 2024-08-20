using System.Collections;
using UnityEngine;

public class Barbarian : MeleeCharacter  //Barbarian-specific implementation
{
    public override void OnBasicAttackCasted()
    {
        if(!isCasting)
        {
            anim.PlayAnimation(AnimationKey.BASIC_ATTACK);
        }
        
    }

    public override void OnPrimarySkillCasted()
    {
        primarySkill?.CastPrimarySkill();
    }

    public override void OnSecondarySkillCasted()
    {
        secondarySkill?.CastSecondarySkill();
    }


}
