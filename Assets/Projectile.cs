using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] private ProjectileSO projectile;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (projectile.MoveSpeed > 0)
        {
            rb.AddForce(transform.forward * projectile.MoveSpeed, ForceMode.Impulse);
        }

        Destroy(this.gameObject,projectile.LifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        #region AOE Damage
        if (projectile.ExplosionRadius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, projectile.ExplosionRadius);

            foreach (Collider c in colliders)
            {
                OnEnemyHit(c);
            }
            
        }
        #endregion AOE Damage

        #region Single Target Damage
        else
        {
            OnEnemyHit(other);
        }
        #endregion Single Target Damage

        DespawnAndDestroy();
    }

    private void DespawnAndDestroy() //todo: A non-rpc method despawn an object. is it okay?
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();

        if (networkObject != null && networkObject.IsSpawned)
        {
            SpawnHitEffectServerRpc();

            networkObject.Despawn(true);
        }

        Destroy(gameObject);
    }

    [ServerRpc]
    private void SpawnHitEffectServerRpc()
    {
        
        // Instantiate hit effect on the server
        GameObject projectile1 = Instantiate(projectile.HitEffect, transform.position, Quaternion.identity);

        // Get the NetworkObject component and spawn it on the network
        if (projectile1.TryGetComponent<NetworkObject>(out var obj))
        {
            obj.Spawn(true);
        }
    }

    void OnEnemyHit(Collider e)
    {
        if (e.TryGetComponent<IEnemyCombat>(out var enemy))
        {
            enemy.KnockBack(transform.position, projectile.KnockbackForce);
            enemy.TakeDamage(projectile.Damage);   
        }
    }

}
