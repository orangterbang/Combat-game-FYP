using Unity.VisualScripting;
using UnityEngine;

//Note for this class
//1. Control player movement based on input
//2. Play animation

public class PlayerMovementManager : CharacterMovementManager
{
    PlayerManager player;

    [Header("Movement Setting")]
    Vector3 moveDirection;
    Vector3 lastMoveDirection;
    Quaternion targetRotation;
    [SerializeField]float moveSpeed = 3f;
    [SerializeField]float rotationSpeed = 3f;
    [SerializeField]float gravityValue = -9.81f;
    [SerializeField]float verticalVelocity = 0f;

    [Header("Dodge Setting")]
    Vector3 dodgeDirection;
    Vector3 dodgeVelocity;
    [SerializeField]float dodgeSpeed = 2f;
    bool isdodging = false;


    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }
    
    void Start()
    {
        lastMoveDirection = Vector3.right;
    }

    public void HandleAllMovement()
    {
        //Ground movement(i.e left-right, dodge)
        //Aerial movement(i.e falling)
        HandleGroundMovement();

        if (PlayerInput.Instance.dodgeInput)
        {
            HandleDodgeMovement();
        }
    }

    //MOVEMENT
    private void HandleGroundMovement()
    {
        if (!player.canMove)
        {
            return;
        }

        CalculateVelocity();
        player.characterController.Move(GetMovementVector() * Time.deltaTime);
        PlayMovementAnim();
        MoveDirectionRotate();
    }

    private void PlayMovementAnim()
    {
        if(PlayerInput.Instance.MovementInputValue != 0)
        {
            player.playerAnimatorManager.PlayMovementAnimator(true);
        }
        else
        {
            player.playerAnimatorManager.PlayMovementAnimator(false);
        }
    }

    private void CalculateVelocity()
    {
        float horizontalMovement = PlayerInput.Instance.MovementInputValue;
        moveDirection = new Vector3(horizontalMovement, 0, 0);

        if(player.characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravityValue * Time.deltaTime;
        }
    }

    private Vector3 GetMovementVector()
    {
        Vector3 horizontalVelocity = moveDirection * moveSpeed;

        return horizontalVelocity + new Vector3(0f, verticalVelocity, 0f);
    }

    private void MoveDirectionRotate()
    {
        if (!player.canRotate)
        {
            return;
        }
        if(moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection.normalized;
        }

        if(lastMoveDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(lastMoveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
    }

    //DODGE
    private void HandleDodgeMovement()
    {
        if(player.isPerformingAction)
        {
            return;
        }

        if(PlayerInput.Instance.MovementInputValue != 0)
        {
            //Perform dodge/roll according to player direction
            dodgeDirection = lastMoveDirection.normalized;

            player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward", true, 2, true);
        }
        else
        {
            //Perform Dodge behind
            dodgeDirection = Vector3.Scale(lastMoveDirection, Vector3.left);
            player.playerAnimatorManager.PlayTargetActionAnimation("Dodge_Back", true, 2, true);
        }
        
        dodgeVelocity = dodgeDirection * dodgeSpeed;
        isdodging = true;
    }

    void OnAnimatorMove()
    {//&& PlayerInput.Instance.dodgeInput
        if(player.isPerformingAction && player.animator.applyRootMotion)
        {
            player.characterController.Move(dodgeVelocity * Time.deltaTime);

            isdodging = false;
        }
    }
}
