using System.Collections;
using UnityEngine;

public class Archer : RangedCharacter
{
    [Header("** Prefabs **")]
    public GameObject ArrowPrefab;
    public GameObject BombPrefab;

    [Header("** Shoot Points **")]
    public Transform ArrowShootPoint;
    public Transform BombShootPoint;

    [Header("** Torque Values **")]
    public float minTorque = -5f;
    public float maxTorque = 5f;

    [Header("** Dodge Values **")]
    [SerializeField] private float dodgeDistance = 10f;
    [SerializeField] private float dodgeTime = .4f;

    #region Private Dodge Variables
    private Vector3 dodgeDirection;
    private bool isDodging;
    private float dodgeStartTime;
    #endregion

    public override void OnAttackEnd() => throw new System.NotImplementedException();
    public override void OnAttackStart() => throw new System.NotImplementedException();

    //Basic attack arrow shooting animation event
    public void Shoot() => Instantiate(ArrowPrefab, ArrowShootPoint.position, transform.rotation);

    public override void OnBasicAttackCasted()
    {
        if (!isCasting)
        {
            anim.PlayAnimation(AnimationKey.BASIC_ATTACK);
        }
    }

    #region Primary Skill
    public override void OnPrimarySkillCasted()
    {
        if (skills[SkillType.PrimarySkill].CanUse)
        {
            StartDodge();
            StartCoroutine(skills[SkillType.PrimarySkill].Cooldown());
        }
    }

    private void StartDodge()
    {
        dodgeDirection = GetDodgeDirection();
        BeginDodgeMovement();
        InstantiateBomb(BombShootPoint.position, Vector3.up * 3f);
    }

    private Vector3 GetDodgeDirection()
    {
        //Vector3 moveDirection = inputReceiver.GetMoveDirection();
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector3.zero) ? new Vector3(moveDirection.x, 0, moveDirection.y) : transform.forward;
    }

    private void BeginDodgeMovement()
    {
        isDodging = true;
        dodgeStartTime = Time.time;
        anim.SetBool(AnimationKey.DODGE, isDodging);
    }

    private void InstantiateBomb(Vector3 position, Vector3 force)
    {
        GameObject bomb = Instantiate(BombPrefab, position, Quaternion.identity);
        if (bomb.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(GetRandomTorque(), ForceMode.Impulse);
        }
    }

    private Vector3 GetRandomTorque()
    {
        return new Vector3(
            Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque)
        );
    }
    #endregion

    #region Secondary Skill
    public override void OnSecondarySkillCasted()
    {
        if (!isCasting && skills[SkillType.SecondarySkill].CanUse)
        {
            anim.PlayAnimation(AnimationKey.SECONDARY_SKILL);
            StartCoroutine(DropBombs());
            StartCoroutine(skills[SkillType.SecondarySkill].Cooldown());
        }
    }

    private IEnumerator DropBombs()
    {
        int bombCounter = 0;
        const float radius = 10f;
        const int bombCount = 20;

        while (bombCounter < bombCount)
        {
            Vector3 randomLocation = GetRandomLocation(radius);
            InstantiateBomb(randomLocation, Vector3.down * 5f);
            bombCounter++;
            yield return new WaitForSeconds(.25f);
        }
    }

    private Vector3 GetRandomLocation(float radius)
    {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(0f, radius);

        //!Mathf.Cos and Mathf.Sin functions are used to convert this angle and distance
        //!into x and z coordinates.
        float x = transform.position.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = transform.position.z + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, 5f, z);
    }
    #endregion

    private void FixedUpdate()
    {
        if (isDodging)
        {
            PerformDodge();
        }
    }

    private void PerformDodge()
    {
        float elapsedTime = Time.time - dodgeStartTime;
        if (elapsedTime > dodgeTime)
        {
            EndDodge();
        }
        else
        {
            transform.position += dodgeDirection * Time.fixedDeltaTime / dodgeTime * dodgeDistance;
        }
    }

    private void EndDodge()
    {
        isDodging = false;
        anim.SetBool(AnimationKey.DODGE, isDodging);
    }
}