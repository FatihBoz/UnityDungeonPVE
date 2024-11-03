using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class BombardmentSkill : Skill, ISecondarySkill
{
    [Header("*** PREFABS ***")]
    public GameObject BombPrefab;

    [Header("** Torque Values **")]
    public float minTorque = -5f;
    public float maxTorque = 5f;

    private Archer archer;

    private void Awake()
    {
        archer = GetComponent<Archer>();
    }

    public void CastSecondarySkill()
    {
        if (!archer.isCasting && canUse && NetworkObject.IsOwner)
        {
            CharacterAnimation.Instance.SetTrigger(AnimationKey.SECONDARY_SKILL);
            StartCoroutine(DropBombs());
            StartCoroutine(Cooldown());
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

            InstantiateBombServerRpc(randomLocation, Vector3.down * 5f);

            bombCounter++;
            yield return new WaitForSeconds(.25f);
        }
    }

    [ServerRpc]
    private void InstantiateBombServerRpc(Vector3 position, Vector3 force)
    {
        GameObject bomb = Instantiate(BombPrefab, position, Quaternion.identity);
        NetworkObject netBomb = bomb.GetComponent<NetworkObject>();

        netBomb.Spawn(true);

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

    protected override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }
}
