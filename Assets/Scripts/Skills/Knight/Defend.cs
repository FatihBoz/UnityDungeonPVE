using System.Collections;
using UnityEngine;

public class Defend : Skill,IPrimarySkill
{
    private Knight knight;
    private Rigidbody rb;

    private void Awake()
    {
        knight = GetComponent<Knight>();
        rb = GetComponent<Rigidbody>();  
    }

    public void CastPrimarySkill()
    {
        if (canUse && !knight.isCasting)
        {
            CharacterAnimation.Instance.SetTrigger(AnimationKey.BASIC_ATTACK);
            StartCoroutine(Buff(GetBuff()));
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Buff(Buff b)
    {
        //Environment cannot affect in buff time
        rb.isKinematic = true;
        //start skill casting
        knight.SetCasting(true);
        //make character cannot move in buff time
        knight.CanMove(false);

        knight.characterAttributes.ApplyBuff(b);

        yield return new WaitForSeconds(b.Duration);

        knight.characterAttributes.RemoveBuff(b);
        //Finish the animation
        CharacterAnimation.Instance.SetBool(AnimationKey.PRIMARY_SKILL, false);
        //Finish skill casting
        knight.SetCasting(false);
        //Make character can move
        knight.CanMove(true);
        //Environment can affect character now
        rb.isKinematic = false;
    }

    protected override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }
}
