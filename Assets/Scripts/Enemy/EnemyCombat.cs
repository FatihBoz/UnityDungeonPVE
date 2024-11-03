using System.Collections;
using UnityEngine;

public class EnemyCombat : Enemy, IEnemyCombat
{
    [SerializeField] private float waitingTimeAfterKnockBack = .5f;

    private float hp;
    private bool canDealDamage;
    private bool isKnockedBack;

    private void Start()
    {
        hp = enemy.Hp;
    }

    public void Attack()
    {
        Vector3 attackDirection = transform.forward;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemy.AttackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            anim.SetTrigger("Attack");

            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;

            float angleToTarget = Vector3.Angle(attackDirection, directionToTarget);

            //if player is in the angle
            if (angleToTarget <= enemy.AttackAngle / 2 && canDealDamage)
            {

                if (hitCollider.TryGetComponent<CharacterAttributes>(out var ca))
                {
                    print("player damage aldý");
                }
            }
        }
    }


    public void TakeDamage(float damage)
    {
        anim.Play(AnimationKey.GETHIT);
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(GetComponent<Collider>());
            anim.SetBool(AnimationKey.DEATH, true);
            Destroy(this.gameObject, 1f);
        }
    }



    private IEnumerator KnockbackRoutine(Vector3 direction, float forceMagnitude)
    {
        float tempSpeedHolder = agent.speed;
        isKnockedBack = true;
        agent.speed = 0;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction * forceMagnitude;

        // Ensure we reset the position to the exact end point
        while (elapsedTime < enemy.KnockBackDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / enemy.KnockBackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitingTimeAfterKnockBack);
        agent.speed = tempSpeedHolder;
        isKnockedBack = false;
    }

    public void KnockBack(Vector3 hitPoint, float forceMagnitude)
    {
        if (!isKnockedBack)
        {
            Vector3 forceDirection = transform.position - hitPoint;
            forceDirection.y = 0f;
            //?rb.AddForce(forceDirection.normalized, ForceMode.Impulse);
            StartCoroutine(KnockbackRoutine(forceDirection, forceMagnitude));
        }
    }


    public bool IsKnockedBack => isKnockedBack;

    public void OnAttackStart() => canDealDamage = true;

    public void OnAttackEnd() => canDealDamage = false;

}
