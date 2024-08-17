using System.Collections;
using UnityEngine;

public class Barbarian : MeleeCharacter  //Barbarian-specific implementation
{
    #region VFX
    [SerializeField] private GameObject buffPrefab;
    private GameObject buffEffect;
    #endregion


    private void FixedUpdate()
    {
        if (buffEffect != null)
        {
            buffEffect.transform.position = transform.position;
        }
    }

    public override void OnBasicAttackCasted()
    {
        if(!isCasting)
        {
            anim.PlayAnimation(AnimationKey.BASIC_ATTACK);
        }
        
    }

    public override void OnPrimarySkillCasted()
    {
        if (skills[SkillType.PrimarySkill].CanUse && !isCasting)
        {
            anim.PlayAnimation(AnimationKey.PRIMARY_SKILL);
            StartCoroutine(skills[SkillType.PrimarySkill].Cooldown());
        }
    }

    #region Secondary Skill
    public override void OnSecondarySkillCasted()
    {
        if (skills[SkillType.SecondarySkill].CanUse)
        {
            buffEffect = Instantiate(buffPrefab, transform.position, Quaternion.identity);
            //floats used as percentages. 1.0 -> %100
            skills[SkillType.PrimarySkill].ReduceCooldownByPercentage(1);
            //Apply Buff for a certain amount of time
            StartCoroutine(Buff(skills[SkillType.SecondarySkill].Buff));
            //Start to count cooldown
            StartCoroutine(skills[SkillType.SecondarySkill].Cooldown()); 
        }
    }
    
    public override IEnumerator Buff(Buff b)
    {
        characterAttributes.ApplyBuff(b);
        yield return new WaitForSeconds(b.Duration);
        characterAttributes.RemoveBuff(b);
        //Set primary skill's cd to its base value
        skills[SkillType.PrimarySkill].RestoreCooldown();
    }

    #endregion Secondary Skill
}
