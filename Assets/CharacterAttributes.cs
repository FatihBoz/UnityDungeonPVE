using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public float DamageReduction { get; private set; }
    public float AttackSpeed { get; private set; }


    [SerializeField] private float BaseMoveSpeed;
    private float currentMoveSpeed;
    private CharacterAnimation anim;

    private void Awake()
    {
        anim = GetComponent<CharacterAnimation>();
    }

    private void Start()
    {
        currentMoveSpeed = BaseMoveSpeed;
        DamageReduction = 0;
        AttackSpeed = 1;
    }

    public void ApplyBuff(Buff buff)
    {
        IncreaseDamageReduction(buff.DamageReduction);
        IncreaseMovementSpeedByPercentage(buff.MoveSpeed);
        IncreaseAttackSpeed(buff.AttackSpeed);
    }

    public void RemoveBuff(Buff buff)
    {
        DecreaseAttackSpeed(buff.AttackSpeed);
        DecreaseDamageReduction(buff.DamageReduction);
        DecreaseMovementSpeedByPercentage(buff.MoveSpeed);
    }

    public void IncreaseAttackSpeed(float percentage)
    {
        AttackSpeed += percentage;
        anim.SetAnimationSpeed(AttackSpeed);
    }

    public void DecreaseAttackSpeed(float percentage)
    {
        AttackSpeed -= percentage;
        anim.SetAnimationSpeed(AttackSpeed);
    }

    public void IncreaseDamageReduction(float dr)
    {
        DamageReduction += dr;
        print(DamageReduction);
    }

    public void DecreaseDamageReduction(float dr)
    {
        DamageReduction -= dr;
    }

    public void IncreaseMovementSpeedByPercentage(float percentage)
    {
        if (Mathf.Abs(percentage) > 1)
            return;

        currentMoveSpeed += BaseMoveSpeed * percentage;
    }

    public void DecreaseMovementSpeedByPercentage(float percentage)
    {
        if (Mathf.Abs(percentage) > 1)
            return;

        currentMoveSpeed -= BaseMoveSpeed * percentage;
    }


    public float MoveSpeed() => currentMoveSpeed;

}
