using System.Collections;
using UnityEngine;

public class Skill
{
    #region Skill Attributes
    private Buff buff;
    private SkillType type; 
    private float baseCooldown;
    private float baseDamage;
    private float remainingCooldown;
    protected bool canUse;
    #endregion Skill Attributes

    private float tempCooldownHolder;

    public Skill(SkillSO s)
    {
        buff = s.Buff();
        type = s.Type();
        baseCooldown = s.BaseCooldown();
        baseDamage = s.BaseDamage();
        remainingCooldown = baseCooldown;
        canUse = true;
    }

    public IEnumerator Cooldown()
    {
        canUse = false;
        remainingCooldown = baseCooldown;
        float elapsedTime = 0f;
        while (elapsedTime < baseCooldown)
        {
            elapsedTime += Time.deltaTime;
            remainingCooldown = baseCooldown - elapsedTime;
            yield return null; // Pauses until the next frame
        }

        remainingCooldown = 0f;
        canUse = true;
    }

    public void ReduceCooldownByPercentage(float percentage)
    {
        tempCooldownHolder = baseCooldown;
        baseCooldown *= (1 - percentage);
    }

    public void RestoreCooldown()
    {
        baseCooldown = tempCooldownHolder;
    }

    public float BaseCooldown
    {
        get => baseCooldown;
        set => baseCooldown = value;
    }

    public float BaseDamage
    {
        get => baseDamage;
        set => baseDamage = value;
    }

    public bool CanUse
    {
        get => canUse;
        set => canUse = value;
    }

    public float RemainingCooldown
    {
        get => remainingCooldown;
        set => remainingCooldown = value;
    }

    public SkillType Type
    {
        get => type;
        set => type = value;
    }

    public Buff Buff
    {
        get => buff;
        set => buff = value;
    }


}