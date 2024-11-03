using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private float damage = 1f;
    private float knockbackForce = 1f;


    private void OnTriggerStay(Collider other) //todo:Bunun için bir zaman ayarla. Her yarým saniyede bir hasar versin, gibi.
    {
        if (other.TryGetComponent<IEnemyCombat>(out var enemy))
        {
            enemy.TakeDamage(damage);

            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            enemy.KnockBack(knockbackDirection, knockbackForce * 5f);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetKnockbackForce(float knockbackForce)
    {
        this.knockbackForce = knockbackForce;
    }
}
