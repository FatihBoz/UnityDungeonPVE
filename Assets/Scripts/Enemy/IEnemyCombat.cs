using UnityEngine;

public interface IEnemyCombat
{
    bool IsKnockedBack { get; }

    void Attack();

    public void TakeDamage(float damage);

    public void KnockBack(Vector3 hitPoint, float forceMagnitude);


}
