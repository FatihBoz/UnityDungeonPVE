using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float hp;

    [SerializeField] private float attackRange = 5.0f;

    [SerializeField] private float attackAngle = 180f;

    [SerializeField] private float damage = 10.0f;
    //how much time needed to move after knocked back
    [SerializeField] private float knockBackDuration = 0.5f;

    

    public float Hp { get => hp; set => hp = value; }

    public float AttackRange { get => attackRange; set => attackRange = value; }

    public float AttackAngle { get => attackAngle; set => attackAngle = value; }

    public float Damage { get => damage; set => damage = value; }

    public float KnockBackDuration { get => knockBackDuration; set => knockBackDuration = value; }
}
