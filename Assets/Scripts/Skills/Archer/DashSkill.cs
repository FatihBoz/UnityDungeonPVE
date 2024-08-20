using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DashSkill : Skill , IPrimarySkill
{
    [Header("*** PREFABS ***")]
    public GameObject BombPrefab;

    [Header("** Shoot Points **")]
    public Transform BombShootPoint;

    [Header("** Torque Values **")]
    public float minTorque = -5f;
    public float maxTorque = 5f;

    [Header("** Dodge Values **")]
    [SerializeField] private float dodgeDistance = 10f;
    [SerializeField] private float dodgeTime = .4f;

    private bool isDodging;
    private float dodgeStartTime;
    private Vector3 dodgeDirection;
    private Archer archer;

    private void Awake()
    {
        archer = GetComponent<Archer>();
    }

    public void CastPrimarySkill()
    {
        print(canUse);
        if (canUse)
        {
            StartDodge();
            StartCoroutine(Cooldown());
        }
    }


    private Vector3 GetDodgeDirection()
    {
        Vector3 moveDirection = archer.movement.MoveDirection;
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector3.zero) ? new Vector3(moveDirection.x, 0, moveDirection.y) : transform.forward;
    }

    private void StartDodge()
    {
        dodgeDirection = GetDodgeDirection();
        BeginDodgeMovement();
        InstantiateBomb(BombShootPoint.position, Vector3.up * 3f);
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

    private void BeginDodgeMovement()
    {
        isDodging = true;
        dodgeStartTime = Time.time;
        archer.anim.SetBool(AnimationKey.DODGE, isDodging);
    }


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
        archer.anim.SetBool(AnimationKey.DODGE, isDodging);
    }


}
