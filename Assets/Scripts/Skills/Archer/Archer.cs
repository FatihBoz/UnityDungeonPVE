using UnityEngine;

public class Archer : RangedCharacter
{
    [Header("** Prefabs **")]
    public GameObject ArrowPrefab;

    [Header("** Shoot Points **")]
    public Transform ArrowShootPoint;

    public override void OnAttackEnd() => throw new System.NotImplementedException();
    public override void OnAttackStart() => throw new System.NotImplementedException();


    public void ShootArrow()
    {
        Instantiate(ArrowPrefab, ArrowShootPoint.position, transform.rotation);
    }

    //Basic attack arrow shooting animation event
    public void Shoot()
    {
        ShootArrow();
    }


    public override void OnBasicAttackCasted()
    {
        if (!isCasting)
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