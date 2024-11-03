using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : NetworkBehaviour
{
    public PlayerControl PlayerControl;
    private ICharacter skill;

    private void Awake()
    {
        PlayerControl = new PlayerControl();
        PlayerControl.Player.Enable();

        skill = gameObject.GetComponent<ICharacter>();
    }

    private void Start()
    {
        PlayerControl.Player.BasicAttack.performed += OnBasicAttackCasted;
        PlayerControl.Player.PrimarySkill.performed += OnPrimarySkillCasted;
        PlayerControl.Player.SecondarySkill.performed += OnSecondarySkillCasted;
    }

    public Vector2 GetMoveDirection()
    {
        if (!IsOwner)
        {
            return Vector2.zero;
        }

        Vector2 moveDirection = PlayerControl.Player.Move.ReadValue<Vector2>();

        return moveDirection.normalized;   
    }

    public Vector2 GetMousePosition()
    {
        if (!IsOwner)
        {
            return Vector2.zero;
        }

        Vector2 mousePos = PlayerControl.Player.MousePosition.ReadValue<Vector2>();

        return mousePos;
    }

    private void OnBasicAttackCasted(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        skill?.OnBasicAttackCasted();
    }

    private void OnPrimarySkillCasted(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        skill?.OnPrimarySkillCasted();
    }

    private void OnSecondarySkillCasted(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        skill?.OnSecondarySkillCasted();
    }

    public override void OnDestroy()
    {
        PlayerControl.Player.Disable();
    }

    
}
