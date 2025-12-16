using Unity.VisualScripting;
using UnityEngine;
//clean some codes
//add animations both in code and any animation necessary
public enum MovementState
{
    Normal,
    Move,
    Jump,
    Dodge
}

public class PlayerMovement : MonoBehaviour
{
    public MovementState state{get; private set;}
    private Rigidbody rb;
    private Animator animator;
    private float inputValue;
    public float playerMovementSpeed;

    private Jump playerJump;
    public float playerJumpVelocity;
    public float minJumpThreshold;

    private Attack playerAttack;
    private Parry playerParry;
    
    private Dodge playerDodge;
    public float playerDodgeSpeed;
    public float playerDodgeSlowdownValue;
    private float dodgeDirection;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();

        playerJump = gameObject.GetComponent<Jump>();
        playerAttack = gameObject.GetComponent<Attack>();
        playerParry = gameObject.GetComponent<Parry>();
        playerDodge = gameObject.GetComponent<Dodge>();

        state = MovementState.Normal;
    }

    
    void Update()
    {
        inputValue = Input.GetAxisRaw("Horizontal");

        switch (state)
        {
            case MovementState.Normal:
                Idle();
                HandleJump();
                HandleAttack();
                HandleParry();
                HandleDodge();
                break;
            
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case MovementState.Move:
                HandleMovement();
                HandleJump();
                HandleAttack();
                HandleParry();
                HandleDodge();
                break;
            case MovementState.Jump:
                HandleJumpMovement();
                break;
            case MovementState.Dodge:
                HandleDodgeMovement();
                break;
            
        }
    }

    void Idle()
    {

        animator.SetBool("Run", false);

        if(inputValue != 0)
        {
            state = MovementState.Move;
        }
    }

    void HandleMovement()
    {
        animator.SetBool("Run", true);
        float inputDirection = inputValue;

        Vector3 moveDirection = new Vector3(inputDirection, 0).normalized;
        Vector3 currentVelocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(moveDirection.x * playerMovementSpeed, currentVelocity.y, currentVelocity.z);

        if(inputValue == 0)
        {
            state = MovementState.Normal;
        }
    }

    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerJump.jumpVelocity = playerJumpVelocity;
            playerJump.jumpPosition = new Vector3(inputValue, 1).normalized;
            state = MovementState.Jump;
        }
    }
    void HandleJumpMovement()
    {
        playerJump.ExecuteAction();

        if(playerJump.jumpVelocity < minJumpThreshold)
        {
            state = MovementState.Normal;
        }
    }
    void HandleAttack()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerAttack.ExecuteAction();
        }
    }

    void HandleParry()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerParry.ExecuteAction();
        }
    }

    void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            dodgeDirection = Input.GetAxis("Horizontal");

            if(dodgeDirection == 0)
            {
                dodgeDirection = -1f;
            }

            playerDodge.moveDirection = new Vector3(dodgeDirection, 0).normalized;
            playerDodge.dodgeSpeed = playerDodgeSpeed;

            state = MovementState.Dodge;
        }
    }
    void HandleDodgeMovement()
    {        
        playerDodge.ExecuteAction();
       
        if(playerDodge.dodgeSpeed < playerDodgeSlowdownValue)
        {
            state = MovementState.Normal;
        }
    }
}
