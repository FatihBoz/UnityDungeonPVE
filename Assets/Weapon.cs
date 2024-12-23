using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float forceMagnitude = 1f;
    public float DamageAmount = 5f;
    public GameObject hitEffectPrefab;
    bool canDealDamage;

    public void OnAttackStart()
    {
        canDealDamage = true;
    }

    public void OnAttackEnd()
    {
        canDealDamage = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage)
        {
            if(other.TryGetComponent<IEnemyCombat>(out var enemy))
            {
                enemy.TakeDamage(DamageAmount);

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
                enemy.KnockBack(hitPoint, forceMagnitude);
            }
        }
    }
}
