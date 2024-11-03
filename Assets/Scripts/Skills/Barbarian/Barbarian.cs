using System.Collections;
using UnityEngine;

public class Barbarian : MeleeCharacter  //Barbarian-specific implementation
{
    public override void OnBasicAttackCasted()
    {
        if(!isCasting && NetworkObject.IsOwner)
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
