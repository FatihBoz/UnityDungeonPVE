using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float additionalAttackSpeedPercentage;
    [SerializeField] private float baseMoveSpeed;

    private float attackSpeed = 1f;

    private float damageReduction = 0f;

    private float currentMoveSpeed;


    #region GETTERS
    public float MoveSpeed => currentMoveSpeed;
    public float AttackSpeed => attackSpeed;
    public float DamageReduction => damageReduction;

    #endregion GETTERS


    private void Awake()
    {
        currentMoveSpeed = baseMoveSpeed;
    }

    private void Start()
    {
        ChangeAttackSpeedBy(additionalAttackSpeedPercentage);
    }

    public void ApplyBuff(Buff buff)
    {
        ChangeDamageReductionBy(buff.DamageReduction);
        ChangeMovementSpeedBy(buff.MoveSpeed);
        ChangeAttackSpeedBy(buff.AttackSpeed);
    }

    public void RemoveBuff(Buff buff)
    {
        ChangeAttackSpeedBy(-buff.AttackSpeed);
        ChangeDamageReductionBy(-buff.DamageReduction);
        ChangeMovementSpeedBy(-buff.MoveSpeed);
    }
    public void ChangeAttackSpeedBy(float AS)
    {
        attackSpeed += AS;
        CharacterAnimation.Instance.SetAnimationSpeed(attackSpeed);
    }

    public void ChangeDamageReductionBy(float dr)
    {
        damageReduction += dr;
        damageReduction = Mathf.Clamp01(damageReduction);
    }

    public void ChangeMovementSpeedBy(float moveSpeedPercentage)
    {
        currentMoveSpeed += baseMoveSpeed * moveSpeedPercentage;
    }

    


}
