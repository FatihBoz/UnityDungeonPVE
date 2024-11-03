using UnityEngine;

public class EnemyMovement : Enemy
{
    private float timeBetweenPathUpdates = 0.5f;

    private float lastPathUpdateTime;

    private GameObject[] players;

    private IEnemyCombat enemyCombat;

    protected override void Awake()
    {
        base.Awake();
        enemyCombat = GetComponent<IEnemyCombat>();
    }


    private void FixedUpdate()
    {
        if (enemyCombat.IsKnockedBack)
        {
            return;
        }

        players = GameObject.FindGameObjectsWithTag("Player"); //todo: BURANIN DEÐÝÞMESÝ GEREKÝYOR.

        Transform closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            RotateTowardsPlayer(closestPlayer);

            float distanceToPlayer = Vector3.Distance(transform.position, closestPlayer.position);

            if (distanceToPlayer <= enemy.AttackRange)
            {
                enemyCombat.Attack();
                return;
            }

            //Set enemies' destination in every certain amount of time instead of every frame.
            if (Time.time - lastPathUpdateTime >= timeBetweenPathUpdates)
            {
                lastPathUpdateTime = Time.time;
                agent.SetDestination(closestPlayer.position);
            }
        }
    }



    private void RotateTowardsPlayer(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }



    private Transform FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                closestPlayer = player.transform;
            }
        }

        return closestPlayer;
    }
}
