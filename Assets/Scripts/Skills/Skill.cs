using System.Collections;
using Unity.Netcode;
using UnityEngine;

public abstract class Skill : NetworkBehaviour
{
    [Header("*** SKILL ATTRIBUTES ***")]
    [SerializeField]private Buff buff;
    [SerializeField]private float baseCooldown;
    [SerializeField]private float baseDamage;


    #region Private Skill Attributes
    private float remainingCooldown;
    protected bool canUse = true;
    #endregion Private Skill Attributes

    private float tempCooldownHolder;


    public IEnumerator Cooldown()
    {
        canUse = false;
        remainingCooldown = baseCooldown;
        float elapsedTime = 0f;
        while (elapsedTime < baseCooldown)
        {
            elapsedTime += Time.deltaTime;
            remainingCooldown = baseCooldown - elapsedTime;
            yield return null; // wait until the next frame
        }

        remainingCooldown = 0f;
        canUse = true;
    }

    public void ReduceCooldownByPercentage(float percentage)
    {
        tempCooldownHolder = baseCooldown;
        baseCooldown *= (1 - percentage);
    }

    public void RestoreCooldown()
    {
        baseCooldown = tempCooldownHolder;
    }

    public Buff GetBuff() => buff;

    protected abstract void UpgradeSkill();
}