using System.Collections;
using UnityEngine;

public class Knight : MeleeCharacter //Knight-specific implementation
{

    public override void OnBasicAttackCasted()
    {
        if (!isCasting)
        {
            anim.PlayAnimation(AnimationKey.BASIC_ATTACK);
        }
    }

    public override void OnPrimarySkillCasted()
    {
        if (skills[SkillType.PrimarySkill].CanUse && !isCasting)
        {
            isCasting = true;
            anim.SetBool(AnimationKey.PRIMARY_SKILL, true);
            StartCoroutine(Buff(skills[SkillType.PrimarySkill].Buff));
            StartCoroutine(skills[SkillType.PrimarySkill].Cooldown());
        }
    }

    public override void OnSecondarySkillCasted()
    {
        anim.PlayAnimation(AnimationKey.SECONDARY_SKILL);
    }

    public override IEnumerator Buff(Buff b)
    {
        characterAttributes.ApplyBuff(b);
        yield return new WaitForSeconds(b.Duration);
        characterAttributes.RemoveBuff(b);
        //Finish the animation
        anim.SetBool(AnimationKey.PRIMARY_SKILL, false);
        //Finish skill casting
        isCasting = false;
    }
}
