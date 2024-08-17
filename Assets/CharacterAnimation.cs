using UnityEngine;

public class CharacterAnimation : MonoBehaviour //!CAN BE CHANGED.
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string parameterName)
    {
        if (animator != null)
        {
            animator.SetTrigger(parameterName);
        }
    }


    public void SetBool(string parameterName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameterName, value);
        }
    }

    public void SetAnimationSpeed(float  speed)
    {
        animator.speed = speed;
    }
}

public static class AnimationKey
{
    public const string BASIC_ATTACK = "BasicAttack";
    public const string PRIMARY_SKILL = "PrimarySkill";
    public const string SECONDARY_SKILL = "SecondarySkill";
    public const string RUNNING = "isRunning";
    public const string DEFEND = "Defend";
    public const string DODGE = "isDodging";
    public const string DEATH = "Death";
    public const string GETHIT = "GetHit";
}