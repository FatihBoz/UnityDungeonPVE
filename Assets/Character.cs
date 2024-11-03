using Unity.Netcode;
using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(InputReceiver))]
public abstract class Character : NetworkBehaviour , ICharacter //General character implementation
{
    public static Action<Character> OnCharacterSpawn; 

    protected IPrimarySkill primarySkill;

    protected ISecondarySkill secondarySkill;

    public CharacterAttributes characterAttributes { get; private set; }    

    public InputReceiver Input {  get; private set; }   

    public bool isCasting { get; private set; }

    public Vector2 MoveDirection { get; private set; }


    [SerializeField] private float rotationSpeed;

    [SerializeField] private CinemachineVirtualCamera vcam;

    [SerializeField] private Camera mainCam;


    private bool isRunning;
    private bool canMove = true;
    private Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterAttributes = GetComponent<CharacterAttributes>();
        Input = GetComponent<InputReceiver>();


        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //Skill
        primarySkill = GetComponent<IPrimarySkill>();
        secondarySkill = GetComponent<ISecondarySkill>();
    }

    private void Start()
    {
        OnCharacterSpawn?.Invoke(this);

        vcam.Follow = transform;

    }


    public void SetCasting(bool isCasting)
    {
        this.isCasting = isCasting;
    }


    #region MOVEMENT

    public void CanMove(bool value)
    {
        //canMove = value;
    }


    void Move()
    {
        //if (canMove)
        //{
            
        //}

        float moveSpeed = characterAttributes.MoveSpeed;
        MoveDirection = Input.GetMoveDirection();
        rb.velocity = new Vector3(MoveDirection.x * moveSpeed, 0, MoveDirection.y * moveSpeed);

    }


    void Rotate()
    {

        Vector2 mousePosition = Input.GetMousePosition();
        Ray ray = mainCam.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            Vector3 direction = targetPosition - transform.position;
            // Ignore the Y component to keep the rotation constrained to the Y-axis
            direction.y = 0;
            // Calculate the rotation needed to look at the target direction
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
    void Animate()
    {

        if (MoveDirection != Vector2.zero)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
            
        CharacterAnimation.Instance.SetBool(AnimationKey.RUNNING, isRunning);
    }


    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
    }

    #endregion MOVEMENT



    #region Animation Events
    protected virtual void OnAnimationStart()
    {
        SetCasting(true);
    }

    protected virtual void OnAnimationEnd()
    {
        SetCasting(false);
    }
    #endregion Animation Events




    #region Abstract methods

    public abstract void OnBasicAttackCasted();

    public abstract void OnPrimarySkillCasted();

    public abstract void OnSecondarySkillCasted();

    public abstract void OnAttackStart();

    public abstract void OnAttackEnd();

    #endregion Abstract methods



}
