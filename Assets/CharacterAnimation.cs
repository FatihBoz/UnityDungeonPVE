using Unity.Netcode;
using UnityEngine;

public class CharacterAnimation : NetworkBehaviour //!CAN BE CHANGED.
{
    public static CharacterAnimation Instance;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Instance = this;    
    }


    public void SetTrigger(string parameterName)
    {
        animator.SetTrigger(parameterName);
    }

    public void SetBool(string param, bool value)
    {
        animator.SetBool(param, value);
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