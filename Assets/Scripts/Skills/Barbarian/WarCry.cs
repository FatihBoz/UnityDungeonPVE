using System.Collections;
using UnityEngine;

public class WarCry : Skill,ISecondarySkill
{
    private Barbarian barbarian;
    private IPrimarySkill primarySkill;

    #region VFX
    [SerializeField] private GameObject buffPrefab;
    private GameObject buffEffect;
    #endregion



    private void Awake()
    {
        barbarian = GetComponent<Barbarian>();
        primarySkill = GetComponent<IPrimarySkill>();
    }

    private void FixedUpdate()
    {
        if (buffEffect != null)
        {
            buffEffect.transform.position = transform.position;
        }
    }

    private IEnumerator Buff()
    {
        barbarian.characterAttributes.ApplyBuff(GetBuff());
        yield return new WaitForSeconds(GetBuff().Duration);
        barbarian.characterAttributes.RemoveBuff(GetBuff());
        //Set primary skill cd to its default value
        //!primarySkill.RestoreCooldown();
    }


    public void CastSecondarySkill()
    {
        if (canUse)
        {
            buffEffect = Instantiate(buffPrefab, transform.position, Quaternion.identity);
            //floats used as percentages. 1.0 -> %100
            //!primarySkill.ReduceCooldownByPercentage(1);
            //Apply Buff for a certain amount of time
            StartCoroutine(Buff());
            //Start to count cooldown
            StartCoroutine(Cooldown());
        }

    }

    protected override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }
}
