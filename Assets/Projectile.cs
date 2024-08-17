using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float lifeTime = 0.75f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float knockbackForce = 5f;
    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (moveSpeed > 0)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        }

        Destroy(this.gameObject,lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);

        #region AOE Damage
        if (explosionRadius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider c in colliders)
            {
                OnEnemyHit(c);
            }
            
        }
        #endregion AOE Damage

        #region SÝngle Target Damage
        else if (other.CompareTag("Enemy")) //!Single target damage
        {
            OnEnemyHit(other);
        }
        #endregion SÝngle Target Damage

        Destroy(this.gameObject);
    }

    void OnEnemyHit(Collider e)
    {
        if(e.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.KnockBack(transform.position, knockbackForce);
            enemy.TakeDamage(damage);
        }
    }

}
