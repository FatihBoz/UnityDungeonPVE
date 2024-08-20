using UnityEngine;

public class Archer : RangedCharacter
{
    [Header("** Prefabs **")]
    public GameObject ArrowPrefab;

    [Header("** Shoot Points **")]
    public Transform ArrowShootPoint;

    protected override void Awake()
    {
        base.Awake();
        primarySkill = GetComponent<IPrimarySkill>();
        secondarySkill = GetComponent<ISecondarySkill>();
    }

    public override void OnAttackEnd() => throw new System.NotImplementedException();
    public override void OnAttackStart() => throw new System.NotImplementedException();

    //Basic attack arrow shooting animation event
    public void Shoot() => Instantiate(ArrowPrefab, ArrowShootPoint.position, transform.rotation);

    public override void OnBasicAttackCasted()
    {
        if (!isCasting)
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