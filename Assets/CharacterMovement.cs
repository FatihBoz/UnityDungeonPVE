using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class CharacterMovement : MonoBehaviour
{
    public Vector2 MoveDirection {  get; private set; } 

    #region [SerializeField]
    [SerializeField] private float rotationSpeed;
    #endregion

    #region private
    private bool canMove;
    private Rigidbody rb;
    private Camera mainCamera;
    private InputReceiver inputReceiver;
    private CharacterAnimation characterAnimation;
    private CharacterAttributes characterAttributes;
    #endregion



     private void Awake()
    {
        characterAttributes = GetComponent<CharacterAttributes>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        characterAnimation = GetComponent<CharacterAnimation>();
        rb = GetComponent<Rigidbody>();
        inputReceiver = GetComponent<InputReceiver>();
        canMove = true;
    }

    public void CanMove(bool value)
    {
        canMove = value;
    }


    void Move()
    {
        if (canMove)
        {
            float moveSpeed = characterAttributes.MoveSpeed();
            MoveDirection = inputReceiver.GetMoveDirection();
            rb.velocity = new Vector3(MoveDirection.x * moveSpeed, 0, MoveDirection.y * moveSpeed);
        }

    }


    void Rotate()
    {

        Vector2 mousePosition = inputReceiver.GetMousePosition();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // Calculate the direction from the character to the target position
            Vector3 direction = targetPosition - transform.position;

            // Ignore the Y component to keep the rotation constrained to the Y-axis
            direction.y = 0;

            // Calculate the rotation needed to look at the target direction
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the character
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    
    void Animate()
    {
        if (MoveDirection != Vector2.zero)
        {
            characterAnimation.SetBool(AnimationKey.RUNNING, true);
            return;
        }
        characterAnimation.SetBool(AnimationKey.RUNNING, false);
    }


    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
    }


}
