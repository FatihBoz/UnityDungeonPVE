using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour , ICharacter //General character implementation
{

    [SerializeField] private List<SkillSO> skillSO;

    protected Dictionary<SkillType, Skill> skills = new();

    protected CharacterAnimation anim;
    protected CharacterMovement movement;
    protected CharacterAttributes characterAttributes;
    protected bool isCasting;

    protected virtual void Awake()
    {
        anim = GetComponent<CharacterAnimation>();
        movement = GetComponent<CharacterMovement>();
        characterAttributes = GetComponent<CharacterAttributes>();
    }

    private void Start()
    {
        foreach (var skill in skillSO)
        {
            skills[skill.Type()] = new Skill(skill);
        }  
    }

    #region Animation Events
    protected virtual void OnAnimationStart()
    {
        isCasting = true;
    }

    protected virtual void OnAnimationEnd()
    {
        isCasting = false;
    }
    #endregion Animation Events

    public abstract void OnBasicAttackCasted();

    public abstract void OnPrimarySkillCasted();

    public abstract void OnSecondarySkillCasted();

    public abstract void OnAttackStart();

    public abstract void OnAttackEnd();
}
