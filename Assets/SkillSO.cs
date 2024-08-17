using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill System/SkillSO")]
public class SkillSO : ScriptableObject
{
    [SerializeField] private Buff buff;

    [Header("Skill Attributes")]
    [SerializeField] private SkillType type;
    [SerializeField] private float baseCooldown;
    [SerializeField] private float baseDamage;

    public Buff Buff() => buff;
    public SkillType Type() => type; 
    public float BaseCooldown() => baseCooldown;
    public float BaseDamage() => baseDamage;    
}


public enum SkillType
{
    PrimarySkill,
    SecondarySkill
}