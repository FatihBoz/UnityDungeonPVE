using Unity.Netcode;
using UnityEngine;

public class Archer : RangedCharacter
{
    [Header("** Prefabs **")]
    public GameObject ArrowPrefab;

    [Header("** Shoot Points **")]
    public Transform ArrowShootPoint;

    public override void OnAttackEnd() => throw new System.NotImplementedException();
    public override void OnAttackStart() => throw new System.NotImplementedException();


    //Allow non-owners to call this method
    [ServerRpc]
    public void ShootArrowServerRpc()
    {
        GameObject arrow = Instantiate(ArrowPrefab, ArrowShootPoint.position, transform.rotation);

        NetworkObject arrowNetworkObject = arrow.GetComponent<NetworkObject>();
        arrowNetworkObject.Spawn(true);

    }

    //Basic attack arrow shooting animation event
    public void Shoot()
    {
        if (IsOwner)
        {
            ShootArrowServerRpc();
        }   
    }


    public override void OnBasicAttackCasted()
    {
        if (!isCasting && NetworkObject.IsOwner)
        {
            CharacterAnimation.Instance.SetTrigger(AnimationKey.BASIC_ATTACK);
        }
    }

    public override void OnPrimarySkillCasted()
    {
        primarySkill?.CastPrimarySkill();
    }

    public override void OnSecondarySkillCasted()
    {
        secondarySkill?.CastSecondarySkill();
    }


}