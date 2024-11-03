using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile",menuName = "Projectile")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] private GameObject hitEffect;

    [SerializeField] private float lifeTime = 0.75f;

    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float damage = 5f;

    [SerializeField] private float explosionRadius = 5f;

    [SerializeField] private float knockbackForce = 5f;


    public GameObject HitEffect { get => hitEffect; set => hitEffect = value; }

    public float LifeTime { get => lifeTime; set => lifeTime = value; }

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public float Damage { get => damage; set => damage = value; }

    public float ExplosionRadius { get => explosionRadius; set => explosionRadius = value; }

    public float KnockbackForce { get => knockbackForce; set => knockbackForce = value; }
}
