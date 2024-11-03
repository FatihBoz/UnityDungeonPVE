using System.Collections;
using UnityEngine;

public class Knight : MeleeCharacter //Knight-specific implementation
{
    //todo: KNIGHT NEEDS A SECONDARY SKILL
    public override void OnBasicAttackCasted()
    {
        if (!isCasting && NetworkObject.IsOwner)
        {
            CharacterAnimation.Instance.SetTrigger(AnimationKey.BASIC_ATTACK);
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
