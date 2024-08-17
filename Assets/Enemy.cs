using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public float hp = 100f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float timeBetweenPathUpdates = 0.5f;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private GameObject[] players;
    private float lastPathUpdateTime;

    private float knockbackDuration = 0.5f;
    private bool isKnockedBack;
    private bool canDealDamage;

    private Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (isKnockedBack)
        {
            return;
        }

        Transform closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            RotateTowardsPlayer(closestPlayer);

            float distanceToPlayer = Vector3.Distance(transform.position, closestPlayer.position);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
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

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player"))
        {

        }
    }


    private Transform FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach(var player in players)
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


    public void TakeDamage(float damage)
    {
        anim.Play(AnimationKey.GETHIT);
        hp -= damage;

         if (hp <= 0)
        {
            anim.SetBool(AnimationKey.DEATH,true);
            Destroy(this.gameObject,1f);
        }
    }

    private IEnumerator KnockbackRoutine(Vector3 direction,float forceMagnitude)
    {
        float tempSpeedHolder = agent.speed;
        isKnockedBack = true;
        agent.speed = 0;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction * forceMagnitude;

        // Ensure we reset the position to the exact end point
        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        agent.speed = tempSpeedHolder;
        isKnockedBack = false;
    }

    public void KnockBack(Vector3 hitPoint, float forceMagnitude)
    {
        Vector3 forceDirection = transform.position - hitPoint;
        forceDirection.y = 0f;
        //?rb.AddForce(forceDirection.normalized, ForceMode.Impulse);
        StartCoroutine(KnockbackRoutine(forceDirection,forceMagnitude));
    }


    public void OnAttackStart()
    {
        canDealDamage = true;
    }

    public void OnAttackEnd()
    {
        canDealDamage = false;
    }

}
