using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Character : NetworkBehaviour , ICharacter //General character implementation
{

    protected IPrimarySkill primarySkill;
    protected ISecondarySkill secondarySkill;

    public CharacterMovement movement { get; private set; }
    public CharacterAnimation anim { get; private set; }    
    public CharacterAttributes characterAttributes { get; private set; }    
    public bool isCasting { get; private set; } 

    protected virtual void Awake()
    {
        anim = GetComponent<CharacterAnimation>();
        movement = GetComponent<CharacterMovement>();
        characterAttributes = GetComponent<CharacterAttributes>();

        primarySkill = GetComponent<IPrimarySkill>();
        secondarySkill = GetComponent<ISecondarySkill>();
    }

    public void SetCasting(bool isCasting)
    {
        this.isCasting = isCasting;
    }

    #region Animation Events
    protected virtual void OnAnimationStart()
    {
        SetCasting(true);
    }

    protected virtual void OnAnimationEnd()
    {
        SetCasting(false);
    }
    #endregion Animation Events

    public abstract void OnBasicAttackCasted();

    public abstract void OnPrimarySkillCasted();

    public abstract void OnSecondarySkillCasted();

    public abstract void OnAttackStart();

    public abstract void OnAttackEnd();
}
