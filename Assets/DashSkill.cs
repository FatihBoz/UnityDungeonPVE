using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    private Character character;
    private bool isDodging;
    private float dodgeStartTime;
    public float dodgeDuration;
    public DashSkill(SkillSO s, Character c) : base(s)
    {
        character = c;
    }

    public void ActivateSkill()
    {
        if (canUse)
        {

        }
    }
    

}
