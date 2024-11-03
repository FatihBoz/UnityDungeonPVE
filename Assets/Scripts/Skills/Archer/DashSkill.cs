using System;
using Unity.Netcode;
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
    [SerializeField] private float dodgeTime = .3f;

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
        if (canUse && NetworkObject.IsOwner)
        {
            StartDodge();
            StartCoroutine(Cooldown());
        }
    }


    private Vector3 GetDodgeDirection()
    {
        Vector3 moveDirection = archer.MoveDirection;
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector3.zero) ? new Vector3(moveDirection.x, 0, moveDirection.y) : transform.forward;
    }

    private void StartDodge()
    {

        dodgeDirection = GetDodgeDirection();
        BeginDodgeMovement();
        InstantiateBombServerRpc(BombShootPoint.position, Vector3.up * 3f);
    }

    [ServerRpc]
    private void InstantiateBombServerRpc(Vector3 position, Vector3 force)
    {
        GameObject bomb = Instantiate(BombPrefab, position, Quaternion.identity);

        NetworkObject bombNetworkObject = bomb.GetComponent<NetworkObject>();
        bombNetworkObject.Spawn(true);

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


        CharacterAnimation.Instance.SetBool(AnimationKey.DODGE, isDodging);
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

        CharacterAnimation.Instance.SetBool(AnimationKey.DODGE, isDodging);
    }

    protected override void UpgradeSkill()
    {
        throw new NotImplementedException();
    }
}
