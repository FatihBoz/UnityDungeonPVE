using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    [SerializeField] private GameObject healingCirclePrefab;
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
        anim.PlayAnimation(AnimationKey.BASIC_ATTACK);
    }

    public override void OnPrimarySkillCasted()
    {
        anim.PlayAnimation(AnimationKey.PRIMARY_SKILL);
    }

    public override void OnSecondarySkillCasted()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
