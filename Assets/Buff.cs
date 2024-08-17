using UnityEngine;

[System.Serializable]
public class Buff
{
    [SerializeField] private float buffDuration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damageReduction;
    [SerializeField] private float attackSpeed;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public float DamageReduction
    {
        get => damageReduction;
        set => damageReduction = value;
    }

    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    public float Duration
    {
        get => buffDuration;
        set => buffDuration = value;
    }
}